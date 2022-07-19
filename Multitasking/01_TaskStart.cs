namespace Multitasking;

internal class _01_TaskStart
{
	static void Main(string[] args)
	{
		Task t = new Task(Run); //Genau wie bei Threads (vor .NET 4.0)
		t.Start();

		Task t2 = Task.Factory.StartNew(Run2); //Direkt starten (ab .NET 4.0)

		Task t3 = Task.Run(Run3); //Genau das gleiche wie Factory.StartNew (ab .NET 4.5)

		//t.Wait(); //Äquivalent zu t.Join in Threads (warten bis der Task fertig ist)

		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Main {i}");

		Console.ReadKey(); //Hier auf Tasks warten, werden beim Programmende nicht wie Threads weiterlaufen
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Task 1 {i}");
	}

	static void Run2()
	{
		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Task 2 {i}");
	}

	static void Run3()
	{
		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Task 3 {i}");
	}
}
