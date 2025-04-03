using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    // Lista som innehåller alla bokningar
    static List<Bokning> bokningar = new List<Bokning>();

    static void Main(string[] args)
    {
        bool kör = true; // Loopkontroll för att hålla igång menyn

        while (kör)
        {
            // Rensar skärmen och visar huvudmenyn
            Console.Clear();
            Console.WriteLine("=== DÄCKARN BOKNINGSSYSTEM ===\n");
            Console.WriteLine("[1] Lägg till bokning");
            Console.WriteLine("[2] Ta bort bokning");
            Console.WriteLine("[3] Ändra bokning");
            Console.WriteLine("[4] Sök lediga tider");
            Console.WriteLine("[5] Visa dagens bokningar");
            Console.WriteLine("[6] Sök bokning via kundnamn");
            Console.WriteLine("[7] Visa bokningar för specifik dag");
            Console.WriteLine("[8] Visa alla bokningar sorterade");
            Console.WriteLine("[9] Rensa alla bokningar");
            Console.WriteLine("[0] Avsluta");
            Console.Write("\nVälj ett alternativ: ");

            string val = Console.ReadLine(); // Läser in användarens val

            // Switch-sats för att anropa rätt metod baserat på användarens menyval
            switch (val)
            {
                case "1":
                    LäggTillBokning();
                    break;
                case "2":
                    TaBortBokning();
                    break;
                case "3":
                    ÄndraBokning();
                    break;
                case "4":
                    SökLedigaTider();
                    break;
                case "5":
                    VisaDagensBokningar();
                    break;
                case "6":
                    SökBokningViaKund();
                    break;
                case "7":
                    VisaBokningarFörSpecifikDag();
                    break;
                case "8":
                    VisaAllaBokningarSorterade();
                    break;
                case "9":
                    RensaAllaBokningar();
                    break;
                case "0":
                    kör = false; // Avslutar loopen
                    break;
                default:
                    Console.WriteLine("Felaktigt val. Tryck valfri knapp.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    // Lägger till en ny bokning i listan
    static void LäggTillBokning()
    {
        Console.Clear();
        Console.WriteLine("--- Lägg till ny bokning ---");

        // Inmatning av kundinformation
        Console.Write("Kundens namn: ");
        string namn = Console.ReadLine();

        Console.Write("Registreringsnummer: ");
        string regNummer = Console.ReadLine();

        Console.Write("Datum (åååå-mm-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime datum))
        {
            // Felhantering för ogiltigt datum
            Console.WriteLine("Felaktigt datumformat.");
            Console.ReadKey();
            return;
        }

        Console.Write("Tid (HH:mm): ");
        string tid = Console.ReadLine();

        // Kontroll för dubbelbokning med samma datum och tid
        if (bokningar.Any(b => b.Datum == datum && b.Tid == tid))                                     //Any()
        {
            Console.WriteLine("\n⚠️ Dubbelbokning! Det finns redan en bokning vid denna tid.");
            Console.ReadKey();
         
        }

        // Visar tillgängliga tjänster
        Console.WriteLine("\nVälj tjänst:");
        for (int i = 0; i < TjänstTyp.Tjänster.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {TjänstTyp.Tjänster[i]}");
        }

        // Användaren väljer tjänst med en siffra
        if (int.TryParse(Console.ReadLine(), out int tjänstVal) && tjänstVal >= 1 && tjänstVal <= TjänstTyp.Tjänster.Count)
        {
            string tjänst = TjänstTyp.Tjänster[tjänstVal - 1];
            bokningar.Add(new Bokning(namn, regNummer, datum, tid, tjänst)); // Skapar och lägger till bokningen
            Console.WriteLine("\n✅ Bokning genomförd!");
        }
        else
        {
            Console.WriteLine("Felaktigt val av tjänst.");
        }
        Console.ReadKey();
    }

    // Tar bort en bokning baserat på datum och tid
    static void TaBortBokning()
    {
        Console.Clear();
        Console.WriteLine("--- Ta bort bokning ---");
        Console.Write("Ange datum (åååå-mm-dd): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime datum))
        {
            Console.Write("Ange tid (HH:mm): ");
            string tid = Console.ReadLine();

            // Söker efter bokning med angivet datum och tid
            var bokning = bokningar.FirstOrDefault(b => b.Datum == datum && b.Tid == tid);
            if (bokning != null)
            {
                bokningar.Remove(bokning);
                Console.WriteLine("Bokningen borttagen.");
            }
            else
            {
                Console.WriteLine("Ingen bokning hittades.");
            }
        }
        else
        {
            Console.WriteLine("Felaktigt datum.");
        }
        Console.ReadKey();
    }

    // Ändrar en befintlig bokning
    static void ÄndraBokning()
    {
        Console.Clear();
        Console.WriteLine("--- Ändra bokning ---");
        Console.Write("Ange datum (åååå-mm-dd): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime datum))
        {
            Console.Write("Ange tid (HH:mm): ");
            string tid = Console.ReadLine();

            var bokning = bokningar.FirstOrDefault(b => b.Datum == datum && b.Tid == tid);                  //FirstOrDefault()
            if (bokning != null)
            {
                // Ändring av uppgifter
                Console.Write("Nytt kundnamn: ");
                bokning.KundNamn = Console.ReadLine();

                Console.Write("Nytt registreringsnummer: ");
                bokning.RegistreringsNummer = Console.ReadLine();

                Console.WriteLine("Välj ny tjänst:");
                for (int i = 0; i < TjänstTyp.Tjänster.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {TjänstTyp.Tjänster[i]}");
                }

                if (int.TryParse(Console.ReadLine(), out int tjänstVal) && tjänstVal >= 1 && tjänstVal <= TjänstTyp.Tjänster.Count)
                {
                    bokning.Tjänst = TjänstTyp.Tjänster[tjänstVal - 1];
                    Console.WriteLine("Bokningen uppdaterad.");
                }
                else
                {
                    Console.WriteLine("Felaktigt val.");
                }
            }
            else
            {
                Console.WriteLine("Ingen bokning hittades.");
            }
        }
        else
        {
            Console.WriteLine("Felaktigt datum.");
        }
        Console.ReadKey();
    }

    // Visar lediga tider i en vecka framåt
    static void SökLedigaTider()
    {
        Console.Clear();
        Console.WriteLine("--- Lediga tider de kommande 7 dagarna ---");
        DateTime idag = DateTime.Today;
        for (int i = 0; i < 7; i++)
        {
            DateTime datum = idag.AddDays(i);
            Console.WriteLine($"\nDatum: {datum:yyyy-MM-dd}");
            for (int h = 9; h <= 17; h++)
            {
                string tid = $"{h}:00";
                if (!bokningar.Any(b => b.Datum == datum && b.Tid == tid))
                {
                    Console.WriteLine($"  - {tid}");
                }
            }
        }
        Console.ReadKey();
    }

    // Visar bokningar för dagens datum
    static void VisaDagensBokningar()
    {
        Console.Clear();
        Console.WriteLine("--- Dagens bokningar ---\n");
        DateTime idag = DateTime.Today;
        var dagens = bokningar.Where(b => b.Datum == idag).ToList();                                 //Where()

        if (dagens.Count == 0)
        {
            Console.WriteLine("Inga bokningar idag.");
        }
        else
        {
            foreach (var b in dagens)
            {
                Console.WriteLine($"{b.Tid} - {b.KundNamn} ({b.RegistreringsNummer}) - {b.Tjänst}");
            }
        }
        Console.ReadKey();
    }

    // Söker bokningar med ett visst kundnamn
    static void SökBokningViaKund()
    {
        Console.Clear();
        Console.Write("Skriv kundens namn: ");
        string namn = Console.ReadLine();
        var hittad = bokningar.Where(b => b.KundNamn.ToLower().Contains(namn.ToLower())).ToList();

        if (hittad.Count == 0)
        {
            Console.WriteLine("Ingen bokning hittades.");
        }
        else
        {
            foreach (var b in hittad)
            {
                Console.WriteLine($"{b.Datum:yyyy-MM-dd} {b.Tid} - {b.KundNamn} ({b.RegistreringsNummer}) - {b.Tjänst}");
            }
        }
        Console.ReadKey();
    }

    // Visar bokningar för ett visst datum
    static void VisaBokningarFörSpecifikDag()
    {
        Console.Clear();
        Console.Write("Ange datum (åååå-mm-dd): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime datum))
        {
            var bokningarDag = bokningar.Where(b => b.Datum == datum).ToList();
            if (bokningarDag.Count == 0)
            {
                Console.WriteLine("Inga bokningar denna dag.");
            }
            else
            {
                foreach (var b in bokningarDag)
                {
                    Console.WriteLine($"{b.Tid} - {b.KundNamn} ({b.RegistreringsNummer}) - {b.Tjänst}");
                }
            }
        }
        else
        {
            Console.WriteLine("Felaktigt datum.");
        }
        Console.ReadKey();
    }

    // Visar alla bokningar i sorterad ordning
    static void VisaAllaBokningarSorterade()
    {
        Console.Clear();
        Console.WriteLine("--- Alla bokningar ---\n");
        var sorterade = bokningar.OrderBy(b => b.Datum).ThenBy(b => b.Tid).ToList();                                 //OrderBy() & ThenBy()
        foreach (var b in sorterade)
        {
            Console.WriteLine($"{b.Datum:yyyy-MM-dd} {b.Tid} - {b.KundNamn} ({b.RegistreringsNummer}) - {b.Tjänst}");
        }
        Console.ReadKey();
    }

    // Rensar hela bokningslistan efter bekräftelse
    static void RensaAllaBokningar()
    {
        Console.Clear();
        Console.Write("Är du säker på att du vill radera alla bokningar? (j/n): ");
        string svar = Console.ReadLine();
        if (svar.ToLower() == "j")
        {
            bokningar.Clear();
            Console.WriteLine("Alla bokningar raderade.");
        }
        else
        {
            Console.WriteLine("Avbrutet.");
        }
        Console.ReadKey();
    }
}
