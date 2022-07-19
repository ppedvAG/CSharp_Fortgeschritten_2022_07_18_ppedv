using System.Diagnostics;

namespace AsyncAwait;

internal class Program
{
	static async Task Main(string[] args)
	{
		//Stopwatch sw = Stopwatch.StartNew(); //Sequentiell, ineffizient
		//Toast();
		//GeschirrHerrichten();
		//KaffeeZubereiten();
		//sw.Stop();
		//Console.WriteLine(sw.ElapsedMilliseconds); //8s

		//Stopwatch sw2 = Stopwatch.StartNew();
		//ToastAsync();
		//GeschirrHerrichtenAsync();
		//KaffeeZubereitenAsync();
		//sw2.Stop();
		//Console.WriteLine(sw2.ElapsedMilliseconds); //9ms, Main Thread läuft weiter
		//Console.ReadKey();

		Stopwatch sw2 = Stopwatch.StartNew();
		Task<Toast> toast = ToastTaskAsync();
		Task<Tasse> tasse = GeschirrTaskAsync();
		//Tasse t = await tasse; //warte hier bis die Tasse verfügbar ist
		Task<Kaffee> kaffee = KaffeeTaskAsync(await tasse); //hier auch möglich zu awaiten
		Toast t = await toast; //Warte auf den Toast Task
		Kaffee k = await kaffee;
		sw2.Stop();
		Console.WriteLine(sw2.ElapsedMilliseconds);
	}

	static void Toast()
	{
		Thread.Sleep(4000);
		Console.WriteLine("Toast fertig");
	}

	static void GeschirrHerrichten()
	{
		Thread.Sleep(2000);
		Console.WriteLine("Geschirr hergerrichtet");
	}

	static void KaffeeZubereiten()
	{
		Thread.Sleep(2000);
		Console.WriteLine("Kaffee zubereitet");
	}

	static async void ToastAsync()
	{
		await Task.Delay(4000); //Ersatz für Thread.Sleep()
		Console.WriteLine("Toast fertig");
	}

	static async void GeschirrHerrichtenAsync()
	{
		await Task.Delay(2000);
		Console.WriteLine("Geschirr hergerrichtet");
	}

	static async void KaffeeZubereitenAsync()
	{
		await Task.Delay(2000);
		Console.WriteLine("Kaffee zubereitet");
	}

	static async Task<Toast> ToastTaskAsync()
	{
		await Task.Delay(6000);
		Console.WriteLine("Toast fertig");
		return new Toast();
	}

	static async Task<Tasse> GeschirrTaskAsync()
	{
		await Task.Delay(2000);
		Console.WriteLine("Geschirr hergerrichtet");
		return new Tasse();
	}

	static async Task<Kaffee> KaffeeTaskAsync(Tasse t)
	{
		await Task.Delay(2000);
		Console.WriteLine("Kaffee zubereitet");
		return new Kaffee();
	}
}

class Toast { }

class Tasse { }

class Kaffee { }