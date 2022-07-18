namespace Generics;

internal class Constraints
{
	static void Main(string[] args)
	{
		DataStore4<TestClass> ds;
	}

	public class DataStore1<T> where T : class { } //Referenztyp erzwingen

	public class DataStore2<T> where T : struct { } //Wertetyp erzwingen

	public class DataStore3<T> where T : Constraints { } //Vererbungshierarchie erzwingen

	public class DataStore4<T> where T : new() { } //Nur Typen die einen Default Konstruktor haben

	public class DataStore5<T> where T : Enum { } //Nur Enums

	public class DataStore6<T> where T : Delegate { } //Nur Delegates (Action, Predicate, Func, EventHandler, ...)

	public class DataStore7<T> where T : unmanaged { } //Bestimmte Typen (z.B.: int, bool, double, ...) https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/unmanaged-types

	public class DataStore8<T>
		where T : class, new() //Mehrere Constraints (Ein Referenztyp mit zusätzlich Default Konstruktor)
	{ }

	public class DataStore9<T1, T2> //Mehrere Constraints auf mehrere Generics machen
		where T1 : class
		where T2 : struct
	{ }
}

public class TestClass
{
	public TestClass(int i) { } //Eigener Konstruktor überschreibt default Konstruktor

	public TestClass() { } //Default Konstruktor wiederherstellen
}