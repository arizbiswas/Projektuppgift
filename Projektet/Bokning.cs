// Klass som representerar en bokning i systemet
public class Bokning
{
    // Namnet på kunden som gjort bokningen
    public string KundNamn { get; set; }

    // Kundens bils registreringsnummer
    public string RegistreringsNummer { get; set; }

    // Datumet för bokningen (ex. 2025-05-10)
    public DateTime Datum { get; set; }

    // Tiden för bokningen (ex. 10:00)
    public string Tid { get; set; }

    // Den tjänst som kunden har valt (t.ex. "Däckbyte")
    public string Tjänst { get; set; }

    // Konstruktor som används när en ny bokning skapas
    public Bokning(string kundNamn, string registreringsNummer, DateTime datum, string tid, string tjänst)
    {
        KundNamn = kundNamn;                         // Sätter kundens namn
        RegistreringsNummer = registreringsNummer;   // Sätter registreringsnumret
        Datum = datum;                               // Sätter bokningsdatumet
        Tid = tid;                                   // Sätter bokningstiden
        Tjänst = tjänst;                             // Sätter tjänsten som valts
    }
}
