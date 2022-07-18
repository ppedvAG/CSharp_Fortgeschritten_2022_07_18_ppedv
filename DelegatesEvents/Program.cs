namespace DelegatesEvents;

internal class Program
{
	public delegate void Vorstellungen(string name); //Definition vom Delegate, speichert Referenzen zu Methoden, können zur Laufzeit hinzugefügt oder weggenommen werden

	static void Main(string[] args)
	{
		Vorstellungen vorstellungen; //Variable
		vorstellungen = new Vorstellungen(VorstellungDE); //Delegate erstmal erstellen mit new
		vorstellungen += VorstellungEN; //weitere Methode anhängen ohne new
		vorstellungen -= VorstellungDE; //Methode abziehen
		vorstellungen -= VorstellungDE; //kein Fehler wenn die Methode nicht abgenommen werden kann
		vorstellungen -= VorstellungEN; //Delegate ab hier null

		vorstellungen("Max"); //Delegate ausführen, alle Methoden werden nacheinander aufgerufen

		if (vorstellungen != null)
			vorstellungen("Max"); //null-Check vor Aufruf

		vorstellungen?.Invoke("Max"); //Null-Check mit ? und Invoke

		foreach (Delegate dg in vorstellungen.GetInvocationList()) //Delegate iterieren
		{
			Console.WriteLine(dg.Method.Name);
		}

		vorstellungen = null; //Delegate komplett entleeren
	}

	public static void VorstellungDE(string name) => Console.WriteLine($"Hallo mein Name ist {name}");

	public static void VorstellungEN(string name) => Console.WriteLine($"Hello my name is {name}");
}