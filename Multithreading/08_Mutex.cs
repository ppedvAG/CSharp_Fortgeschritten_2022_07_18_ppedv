namespace Multithreading;

internal class _08_Mutex
{
	static void Main(string[] args)
	{
		Mutex m;
		if (Mutex.TryOpenExisting("08", out m)) //Checken, ob Mutex belegt ist
		{
			Console.WriteLine("Applikation läuft bereits");
			Environment.Exit(0); //Applikation beenden
		}
		else
		{
			m = new Mutex(true, "08"); //Mutex belegen
		}

		m.ReleaseMutex(); //Mutex freigeben
	}
}
