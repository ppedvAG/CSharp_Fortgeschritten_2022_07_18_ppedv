using System.Xml.Serialization;

namespace Benchmark;

[Serializable]
public class Fahrzeug
{
	[XmlAttribute]
	public int ID;

	[XmlAttribute]
	public int MaxGeschwindigkeit;

	[XmlAttribute]
	public FahrzeugMarke Marke;

	public List<Sitzplatz> Sitze;

	public Fahrzeug(int id, int v, FahrzeugMarke fm)
	{
		ID = id;
		MaxGeschwindigkeit = v;
		Marke = fm;
		Sitze = new();

		//Anzahl Sitzplätze anhand der Geschwindigkeit (6: max 150km/h, 5 bis 250km/h, 4 ab 250km/h)
		int sitze = v <= 150 ? 6 : v <= 250 ? 5 : 4;

		//Sitzplätze erstellen
		for (int i = 0; i < sitze; i++)
			Sitze.Add(new Sitzplatz());

		//Sitzplätze semi-zufällig belegen damit die Übung zwischen Teilnehmern gleiche Ergebnisse liefert
		//Geschwindigkeit modulo Anzahl Sitzplätze besetzen
		for (int i = 0; i < v % (sitze + 1); i++)
			Sitze[i].IstBesetzt = true;
	}

	public Fahrzeug() { }
}

[Serializable]
public class Sitzplatz
{
	[XmlAttribute]
	public bool IstBesetzt;

	public Sitzplatz() { }
}

public enum FahrzeugMarke
{
	Audi, BMW, VW
}