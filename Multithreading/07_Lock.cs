namespace Multithreading;

internal class _07_Lock
{
	static int Zahl { get; set; } = 0;

	static object LockObject = new();

	static void Main(string[] args)
	{
		for (int i = 0; i < 500; i++)
		{
			new Thread(ZahlPlusPlus).Start();
		}
	}

	static void ZahlPlusPlus()
	{
		for (int i = 0; i < 100; i++)
		{
			lock (LockObject) //Zahl sperren damit nicht mehrere Threads gleichzeitig draufgreifen, wenn schon gelockt dann warten Threads auf Unlock
			{
				Zahl++;
				Console.WriteLine(Zahl);
			} //Lock öffnen

			Monitor.Enter(LockObject); //Monitor und Lock haben 1:1 den selben Code, genau die selbe Funktionsweise wie oben
			Zahl++;
			Console.WriteLine(Zahl);
			Monitor.Exit(LockObject); //Lock öffnen
		}
	}
}