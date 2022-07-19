namespace Multithreading;

internal class _01_ThreadStarten
{
	static void Main(string[] args)
	{
		Thread t = new Thread(Run); //Funktionszeiger
		t.Start(); //Thread starten

		//Hier parallele Arbeit

		t.Join(); //Threads zusammenführen (wieder sequentiell)

		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Main Thread: {i}");
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Side Thread: {i}");
	}
}