namespace Multitasking;

internal class _02_TaskMitReturn
{
	static void Main(string[] args)
	{
		Task<int> t = Task.Run(SumI); //Task.Run nimmt automatisch Generic an
		//Console.WriteLine(t.Result); //.Result blockt den Main Thread (t.Wait())

		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine(i);
			Thread.Sleep(50);
		}

		Task<int> t2 = Task.Run(() => //Task mit Lambda Expression
		{ 
			int summe = 0;
			for (int i = 0; i < 100; i++)
			{
				summe += i;
				Thread.Sleep(50);
			}
			return summe;
		});

		Task.WaitAll(t, t2); //Warte bis alle Tasks fertig sind
		Task.WaitAny(t, t2); //Warte bis ein Task fertig ist (Rückgabewert: Index vom Task der fertig geworden ist, 0: t, 1: t2)
	}

	static int SumI()
	{
		int summe = 0;
		for (int i = 0; i < 100; i++)
		{
			summe += i;
			Thread.Sleep(50);
		}
		return summe;
	}
}
