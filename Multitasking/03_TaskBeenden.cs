namespace Multitasking;

internal class _03_TaskBeenden
{
	static void Main(string[] args)
	{
		CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
		CancellationToken ct = cancellationTokenSource.Token;

		Task t = new Task(Print, ct); //Hier Token direkt übergeben
		t.Start();

		Thread.Sleep(500);

		cancellationTokenSource.Cancel();

		Console.ReadKey();
	}

	static void Print(object token)
	{
		if (token is CancellationToken ct)
		{
			for (int i = 0; i < 100; i++)
			{
				if (ct.IsCancellationRequested)
					ct.ThrowIfCancellationRequested(); //Task wirft Exception ist aber nicht sichtbar

				Console.WriteLine($"Task {i}");
				Thread.Sleep(50);
			}
		}
	}
}
