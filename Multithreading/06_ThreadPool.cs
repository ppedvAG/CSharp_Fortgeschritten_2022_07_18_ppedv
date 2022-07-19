namespace Multithreading;

internal class _06_ThreadPool
{
	static void Main(string[] args)
	{
		Thread t = new Thread(() => Thread.Sleep(500)); //Vordergrundthread (hält Programm auf bis er fertig ist)
		t.Start();

		ThreadPool.QueueUserWorkItem(Methode1); //Hintergrundthread (wird abgebrochen wenn alle Vordergrundthreads zu Ende sind)
		ThreadPool.QueueUserWorkItem(Methode2);
		ThreadPool.QueueUserWorkItem(Methode3);
		Thread.Sleep(250);

		//Hintergrundthreads abbrechen
	}

	public static void Methode1(object o)
	{
		for (int i = 0; i < 100; i++)
		{
			Thread.Sleep(25);
			Console.WriteLine($"Methode1 {i}");
		}
	}

	public static void Methode2(object o)
	{
		for (int i = 0; i < 100; i++)
		{
			Thread.Sleep(25);
			Console.WriteLine($"Methode2 {i}");
		}
	}

	public static void Methode3(object o)
	{
		for (int i = 0; i < 100; i++)
		{
			Thread.Sleep(25);
			Console.WriteLine($"Methode3 {i}");
		}
	}
}
