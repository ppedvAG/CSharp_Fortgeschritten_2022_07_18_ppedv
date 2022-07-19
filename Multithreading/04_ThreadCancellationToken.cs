namespace Multithreading;

internal class _04_ThreadCancellationToken
{
	static void Main(string[] args)
	{
		try
		{
			CancellationTokenSource cts = new CancellationTokenSource(); //Source erstellen
			CancellationToken token = cts.Token; //Token aus Source entnehmen

			ParameterizedThreadStart pt = new ParameterizedThreadStart(Run);
			Thread t = new Thread(pt);
			t.Start(token); //Token als Parameter übergeben

			Thread.Sleep(2000);
			cts.Cancel(); //Auf der Source die Cancellation anfordern
		}
		catch (OperationCanceledException)
		{
			//Funktioniert auch hier nicht
		}
	}

	static void Run(object o)
	{
		try
		{
			if (o is CancellationToken ct)
			{
				for (int i = 0; i < 100; i++)
				{
					if (ct.IsCancellationRequested)
						ct.ThrowIfCancellationRequested(); //OperationCanceledException werfen

					Console.WriteLine(i);
					Thread.Sleep(100);
				}
			}
		}
		catch (OperationCanceledException)
		{
			Console.WriteLine("Thread wurde mit Token beendet");
		}
	}
}
