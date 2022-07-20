using System.Collections;

namespace Sonstiges;

internal class Program
{
	static void Main(string[] args)
	{
		Wagon w1 = new();
		Wagon w2 = new();
		if (w1 == w2)
		{
			//...
		}

		Zug z = new();
		z++;
		z++;
		z++;

		Zug z1 = new();
		z1++;
		z1++;

		z += w1;
		z += z1;

		foreach (Wagon w in z)
		{
			//...
		}

		Wagon rot = z["Rot"];

		var anon = z.Wagons.Select(e => new { Farb = e.Farbe, HC = e.GetHashCode() });
		anon.First(e => e.Farb == "Rot");

		System.Timers.Timer t = new System.Timers.Timer();
		t.Elapsed += (sender, e) => Console.WriteLine("Timer");
		t.Interval = 1000;
		t.Start();

		Console.ReadKey();
	}
}

public class Zug : IEnumerable<Wagon>
{
	public List<Wagon> Wagons = new();

	Dictionary<int, Wagon> WagonsIDs = new();

	public Wagon this[int pos]
	{
		get => Wagons[pos];
		set => Wagons[pos] = value;
	}

	public Wagon this[string farbe]
	{
		get => Wagons.First(e => e.Farbe == farbe);
	}

	public Wagon this[int pos, int sitze]
	{
		get => Wagons.Where(e => e.AnzSitze == sitze).Where(e => Wagons[pos] != null).First();
	}

	public static Zug operator ++(Zug z1)
	{
		z1.Wagons.Add(new Wagon());
		return z1;
	}

	public static Zug operator +(Zug z1, Wagon w1)
	{
		z1.Wagons.Add(w1);
		return z1;
	}

	public static Zug operator +(Zug z1, Zug z2)
	{
		z1.Wagons.AddRange(z2.Wagons);
		return z1;
	}

	public IEnumerator<Wagon> GetEnumerator()
	{
		return Wagons.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return WagonsIDs.Values.GetEnumerator();
	}
}

public class Wagon
{
	public int AnzSitze;

	public string Farbe;

	public static bool operator ==(Wagon w1, Wagon w2)
	{
		return w1.AnzSitze == w2.AnzSitze && w1.Farbe == w2.Farbe;
	}

	public static bool operator !=(Wagon w1, Wagon w2)
	{
		return !(w1 == w2);
	}
}