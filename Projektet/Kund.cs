// Klass som representerar en kund i bokningssystemet
public class Kund
{
    // Kundens fullständiga namn
    public string Namn { get; set; }

    // Bilens registreringsnummer kopplat till kunden
    public string RegistreringsNummer { get; set; }

    // Konstruktor som skapar ett kundobjekt med namn och registreringsnummer
    public Kund(string namn, string registreringsNummer)
    {
        Namn = namn;
        RegistreringsNummer = registreringsNummer;
    }
}
