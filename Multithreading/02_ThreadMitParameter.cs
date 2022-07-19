namespace Multithreading;

internal class _02_ThreadMitParameter
{
	static void Main(string[] args)
	{
		ParameterizedThreadStart pt = new ParameterizedThreadStart(ThreadMitParam); //Funktionszeiger
		Thread t = new Thread(pt); //PTS übergeben
		t.Start(200); //Bei Start Parameter übergeben

		for (int i = 0; i < 20; i++)
			Console.WriteLine($"Main Thread: {i}/20");
	}

	static void ThreadMitParam(object o) //Nur object möglich, Methode muss void sein
	{
		if (o is int x)
		{
			for (int i = 0; i < x; i++)
				Console.WriteLine($"Side Thread: {i}/{x}");
		}
	}
}
