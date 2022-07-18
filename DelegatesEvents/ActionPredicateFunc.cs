namespace DelegatesEvents;

internal class ActionPredicateFunc
{
	static void Main(string[] args)
	{
		Action<int, int> action = Addiere; //Action: Methode mit void und bis zu 16 Parametern, kein Rückgabewert
		action += Subtrahiere; //Weitere Methode anhängen
		action(3, 5);
		action?.Invoke(4, 6); //Aufruf mit Null-Check

		DoSomething(3, 6, Addiere);
		DoSomething(4, 1, Subtrahiere); //Unterschiedliche Methoden als Parameter

		Predicate<int> predicate = CheckForZero; //Predicate: Methode mit bool als return Wert und genau einem Parameter
		predicate += CheckForOne; //Methode anhängen
		Console.WriteLine(predicate(4));
		bool? b = predicate?.Invoke(4); //Aufruf mit Null-Check, Zuweisung auf nullable bool

		DoSomething(5, CheckForZero);
		DoSomething(1, CheckForOne); //Funktion mit unterschiedlichen Predicates

		Func<int, int, double> func = Multipliziere; //Func: Methode mit Rückgabewert, letzter Generic ist der Rückgabewert
		func += Dividiere;
		double? ergebnis = func?.Invoke(3, 4); //Ausführen

		DoSomething(3, 6, Multipliziere);
		DoSomething(3, 6, Dividiere);

		func += delegate (int x, int y) //Anonyme Methode
		{
			return x * y;
		};

		func += (x, y) => { return x * y; };
		func += (x, y) => x * y; //kürzeste Form

		DoSomething(4, 5, (z1, z2) => { Console.WriteLine(z1 + z2); }); //Action implizieren
		DoSomething(3, x => x != 0); //Predicate, keine Klammern
		DoSomething(4, 5, (z1, z2) => { return z1 + z2; }); //Func implizieren mit return
	}

	#region Actions
	static void Addiere(int x, int y) => Console.WriteLine(x + y);

	static void Subtrahiere(int x, int y) => Console.WriteLine(x - y);

	static void DoSomething(int x, int y, Action<int, int> action) => action?.Invoke(x, y); //Methode mit Action als Parameter, Verhalten der Methode anpassen
	#endregion

	#region Predicate
	static bool CheckForZero(int x) => x == 0;

	static bool CheckForOne(int x) => x == 1;

	static void DoSomething(int x, Predicate<int> pred) => pred?.Invoke(x);
	#endregion

	#region Func
	static double Multipliziere(int x, int y) => x * y;

	static double Dividiere(int x, int y) => (double) x / y;

	static void DoSomething(int x, int y, Func<int, int, double> func) => func?.Invoke(x, y);
	#endregion
}