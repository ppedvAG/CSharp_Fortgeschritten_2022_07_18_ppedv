namespace Multitasking;

internal class _04_TaskExceptions
{
	static void Main(string[] args)
	{
		try
		{
			Task t1, t2, t3, t4;

			t1 = Task.Run(Exception1);
			t2 = Task.Run(Exception2);
			t3 = Task.Run(Exception3);
			t4 = Task.Run(KeineException);

			Task.WaitAll(t1, t2, t3, t4); //Auf Tasks warten um Exceptions zu sehen

			if (t1.IsFaulted) { } //Wenn eine unhandled Exception aufgetreten ist

			if (t2.IsCanceled) { } //Canceled mit Token

			if (t3.IsCompleted) { } //Wenn Task zu Ende ist (erfolgreich oder nicht)

			if (t4.IsCompletedSuccessfully) { } //Wenn Task erfolgreich
		}
		catch (AggregateException ex) //Sammelt alle Exceptions
		{
			foreach (Exception e in ex.InnerExceptions) //Alle Exception die aufgetreten
				Console.WriteLine(e.Message);
		}
	}

	static void Exception1()
	{
		Thread.Sleep(1000);
		throw new DivideByZeroException();
	}

	static void Exception2()
	{
		Thread.Sleep(2000);
		throw new StackOverflowException();
	}

	static void Exception3()
	{
		Thread.Sleep(3000);
		throw new OutOfMemoryException();
	}

	static void KeineException()
	{
		Console.WriteLine("Alles OK");
	}
}
