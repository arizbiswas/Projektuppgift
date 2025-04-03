// Klass som innehåller en lista med alla tjänster som kunden kan boka
public class TjänstTyp
{
    // Statisk lista med tjänster. Den är statisk eftersom tjänsterna inte ändras under programmets gång.
    public static List<string> Tjänster = new List<string>
    {
        "Hjulbyte (vinter/sommar)",             // Byte av vinter- eller sommardäck
        "Däckhotell inkl. hjulbyte",            // Förvaring av däck + byte
        "Hjulinställning",                      // Justering av hjulens vinkel
        "Däckbyte (nya däck)"                   // Byte till helt nya däck
    };
}
