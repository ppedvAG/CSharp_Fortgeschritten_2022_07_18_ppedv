namespace Multitasking;

internal class _05_ContinueWith
{
	static void Main(string[] args)
	{
		Task<int> t = Task.Run(() =>
		{
			return 0;
		});

		t.ContinueWith(task => Folgetask(task.Result)); //Tasks verketten, weitermachen wenn originaler Task fertig
		t.ContinueWith(task => Fehlertask(), TaskContinuationOptions.OnlyOnFaulted); //Verzweigungen
		t.ContinueWith(task => Erfolgstask(), TaskContinuationOptions.OnlyOnRanToCompletion);

		Console.ReadKey();
	}

	static void Folgetask(int x)
	{
		Console.WriteLine(x);
	}

	static void Fehlertask()
	{
		Console.WriteLine("Fehler");
	}

	static void Erfolgstask()
	{
		Console.WriteLine("Erfolg");
	}
}
