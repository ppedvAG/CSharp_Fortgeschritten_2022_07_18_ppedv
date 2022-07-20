using System.Reflection;

namespace Reflection;

internal class Program
{
	static void Main(string[] args)
	{
		Program p = new Program();
		//über p.GetType() Informationen über den Aufbau des Objekts erhalten

		//typeof(Program) Ohne Objekt Informationen erhalten

		typeof(Program).GetMethod("Test").Invoke(p, null); //Methode indirekt aufrufen

		typeof(Program).GetMethod("Test2").Invoke(p, new[] { "Print me" }); //Methode indirekt mit Parameter aufrufen

		object o = Activator.CreateInstance(typeof(Program)); //Program Objekt erstellen

		o.GetType().GetMethod("Test").Invoke(o, null); //Methode über generisches Objekt aufrufen

		object o2 = Activator.CreateInstance("Reflection", "Reflection.Program"); //Object erstellen ohne Type (nur mit strings)

		//o2.GetType().GetField("_wrappedObject", BindingFlags.NonPublic).GetType().GetMethod("Test").Invoke(o2, null); //Methode über generisches Objekt aufrufen

		Assembly a = Assembly.GetExecutingAssembly(); //Derzeitiges Assembly (Projekt oder Programm)
		List<TypeInfo> types = a.DefinedTypes.ToList(); //Alle Typen vom derzeitigen Projekt

		string path = @"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2022_07_18\DelegatesEvents\bin\Debug\net6.0\DelegatesEvents.dll";

		Assembly loaded = Assembly.LoadFrom(path); //Dll laden

		Type compType = loaded.DefinedTypes.First(e => e.Name == "PrimeComponent"); //Finde die PrimeComponent

		object component = Activator.CreateInstance(compType); //Objekt erstellen aus extrahiertem Type

		//EventHandler anhängen
		component.GetType().GetEvent("Prime").AddEventHandler(component, (int i) => Console.WriteLine($"Primzahl: {i}")); //int muss hier angegeben werden, da nicht über Reflection klar ist um welchen Typen es sich bei der Action handelt
		component.GetType().GetEvent("NotPrime").AddEventHandler(component, (int i, int t) => Console.WriteLine($"Keine Primzahl: {i}, teilbar durch {t}"));
		component.GetType().GetEvent("Prime100").AddEventHandler(component, (int i) => Console.WriteLine($"Hundertste Primzahl: {i}"));

		component.GetType().GetMethod("StartProcess").Invoke(component, null);
	}

	public void Test()
	{
		Console.WriteLine("Test wurde aufgerufen");
	}

	public void Test2(string s)
	{
		Console.WriteLine(s);
	}
}