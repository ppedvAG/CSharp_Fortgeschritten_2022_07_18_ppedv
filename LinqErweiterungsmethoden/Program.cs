using LinqErweiterungsmethoden2;
using System.Text;

namespace LinqErweiterungsmethoden;

internal class Program
{
	static void Main(string[] args)
	{
		#region Einfaches Linq
		//Erstellt eine Liste von Start mit einer bestimmten Anzahl Elementen
		//(5, 20) -> Start bei 5, 20 Elemente -> 5-24
		List<int> ints = Enumerable.Range(0, 20).ToList();

		Console.WriteLine(ints.Average());
		Console.WriteLine(ints.Min());
		Console.WriteLine(ints.Max());

		Console.WriteLine(ints.Sum());

		Console.WriteLine(ints.First()); //Erstes Element der Liste, Exception wenn Liste leer
		Console.WriteLine(ints.FirstOrDefault()); //null wenn Liste leer

		Console.WriteLine(ints.Last()); //Erstes Element der Liste, Exception wenn Liste leer
		Console.WriteLine(ints.LastOrDefault()); //null wenn Liste leer

		Console.WriteLine(ints.Single(e => e == 2)); //Einziges Element mit Bedingung, Exception wenn leer oder mehr als ein Element
		Console.WriteLine(ints.SingleOrDefault(e => e == 2)); //Einziges Element mit Bedingung, null wenn leer und Exception wenn mehr als ein Element
		#endregion

		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new Fahrzeug(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		#region Vergleich Linq Schreibweisen
		//Alle BMWs mit Foreach
		List<Fahrzeug> bmwsForEach = new();
		foreach (Fahrzeug f in fahrzeuge)
			if (f.Marke == FahrzeugMarke.BMW)
				bmwsForEach.Add(f);

		//Standard-Linq: SQL-ähnliche Schreibweise (alt)
		List<Fahrzeug> bmws = (from fzg in fahrzeuge
							   where fzg.Marke == FahrzeugMarke.BMW
							   select fzg).ToList();

		//Methodenkette
		List<Fahrzeug> bmwsNeu = fahrzeuge.Where(fzg => fzg.Marke == FahrzeugMarke.BMW).ToList();
		#endregion

		IEnumerable<Fahrzeug[]> chunk = fahrzeuge.Chunk(5); //Liste auf 5er Teile aufteilen, Rest im letzten Teil (5, 5, 2)

		fahrzeuge.Reverse(); //List Methode (wird angewandt)

		IEnumerable<Fahrzeug> reversed = fahrzeuge.Reverse<Fahrzeug>(); //Linq-Funktion (wird nicht angewandt)



		List<Fahrzeug> concat = new()
		{
			new Fahrzeug(324, FahrzeugMarke.Audi),
			new Fahrzeug(338, FahrzeugMarke.BMW),
			new Fahrzeug(291, FahrzeugMarke.VW)
		};

		IEnumerable<Fahrzeug> concatted = fahrzeuge.Concat(concat); //Neue Liste

		fahrzeuge = fahrzeuge.Concat(concat).ToList(); //Concat auf originale Liste anwenden



		fahrzeuge.Append(concat[0]); //Fahrzeug am Ende anhängen (neue Liste)
		fahrzeuge.Prepend(concat[0]); //Fahrzeug am Anfang anhängen (neue Liste)

		concatted.Except(concat); //Originale Liste minus zweite Liste
		concatted.Intersect(concat); //Schnittmenge von beiden Listen (Elemente die in beiden Listen enthalten sind)

		fahrzeuge.MaxBy(f => f.MaxGeschwindigkeit); //Max mit Selector (Min geht auch)
		fahrzeuge.DistinctBy(f => f.Marke); //Distinct mit Selector

		IEnumerable<(int First, Fahrzeug Second)> zip = Enumerable.Range(0, fahrzeuge.Count).Zip(fahrzeuge); //Originale Liste mit neuer Liste zu einem Tupel kombinieren

		Dictionary<int, Fahrzeug> zipDict = zip.ToDictionary(e => e.First, e => e.Second); //Liste von Tupeln zu Dictionary konvertieren

		zipDict.Where((e, i) => e.Key == i); //Where mit Schleifenvariable

		List<List<Fahrzeug>> selectMany = new();
		selectMany.Add(fahrzeuge);
		selectMany.Add(concat);
		List<Fahrzeug> flatten = selectMany.SelectMany(e => e).ToList(); //Listen zu einer Liste kombinieren

		IEnumerable<IGrouping<FahrzeugMarke, Fahrzeug>> grouped = fahrzeuge.GroupBy(e => e.Marke); //Liste auf Gruppen aufteilen

		List<Fahrzeug> einzelneGruppe = grouped.First(e => e.Key == FahrzeugMarke.VW).ToList(); //Einzelne Gruppe extrahieren

		Dictionary<FahrzeugMarke, List<Fahrzeug>> groupDict = grouped.ToDictionary(e => e.Key, e => e.ToList()); //GroupBy auf ein Dictionary mappen

		Console.WriteLine(fahrzeuge.Aggregate(string.Empty, (agg, fzg) => agg + $"Das Fahrzeug hat die Marke {fzg.Marke} und kann maximal {fzg.MaxGeschwindigkeit} fahren.\n")); //Funktion auf jedes Element der Liste anwenden

		Console.WriteLine(fahrzeuge
			.Aggregate(new StringBuilder(), (agg, fzg) => agg.AppendLine($"Das Fahrzeug hat die Marke {fzg.Marke} und kann maximal {fzg.MaxGeschwindigkeit} fahren."))
			.ToString());

		Console.WriteLine(fahrzeuge.Aggregate(0, (double agg, Fahrzeug fzg) => agg + Math.Pow(fzg.MaxGeschwindigkeit, 2)));

		#region Erweiterungsmethoden
		int x = 397529;
		x.Quersumme();
		Console.WriteLine(35729439.Quersumme());

		List<Fahrzeug> shuffled = fahrzeuge.Shuffle().ToList(); //Neue Liste
		#endregion
	}
}

public class Fahrzeug
{
	public int MaxGeschwindigkeit;

	public FahrzeugMarke Marke;

	public Fahrzeug(int v, FahrzeugMarke fm)
	{
		MaxGeschwindigkeit = v;
		Marke = fm;
	}
}

public enum FahrzeugMarke
{
	Audi, BMW, VW
}