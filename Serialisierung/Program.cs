using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;
using static System.Text.Json.JsonElement;

namespace Serialisierung;

internal class Program
{
	static void Main(string[] args)
	{
		//Json();

		//NewtonsoftJson();

		//Xml();

		//CSV();

		//Binary();
	}

	public static void Json()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Environment.GetFolderPath: Speziellen statischen Pfad holen

		string folderPath = Path.Combine(desktop, "Serialisierung"); //Path.Combine: beliebig viele Pfade kombinieren

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		string filePath = Path.Combine(folderPath, "Test.txt");

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		JsonSerializerOptions options = new() { WriteIndented = true }; //Einstellungen beim Serialisieren

		using StreamWriter sw = new(filePath) { AutoFlush = true };
		string json = System.Text.Json.JsonSerializer.Serialize(fahrzeuge, options); //Objekt serialisieren, WICHTIG: Alle Felder müssen Properties sein
		sw.WriteLine(json);
		sw.Close();

		using StreamReader sr = new(filePath);
		List<Fahrzeug> readFahrzeuge = System.Text.Json.JsonSerializer.Deserialize<List<Fahrzeug>>(sr.BaseStream); //Fahrzeuge von Json String zu Liste konvertieren

		JsonDocument doc = JsonDocument.Parse(json); //Json String zu JsonDocument umwandeln um damit zu arbeiten
		JsonElement root = doc.RootElement; //Oberstes Element (Baumstruktur)
		ArrayEnumerator ae = root.EnumerateArray(); //Alle Children holen
		foreach (JsonElement e in ae)
		{
			Console.WriteLine(e.GetProperty("MaxGeschwindigkeit").GetInt32()); //.GetProperty(Name) -> .Get<Typ>()

			Fahrzeug fzg = e.Deserialize<Fahrzeug>(); //Einzelnes Fahrzeug erstellen
			Console.WriteLine(fzg.MaxGeschwindigkeit);
		}
	}

	public static void NewtonsoftJson()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Environment.GetFolderPath: Speziellen statischen Pfad holen

		string folderPath = Path.Combine(desktop, "Serialisierung"); //Path.Combine: beliebig viele Pfade kombinieren

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		string filePath = Path.Combine(folderPath, "Test.txt");

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		JsonSerializerSettings setting = new() { Formatting = Newtonsoft.Json.Formatting.Indented, TypeNameHandling = TypeNameHandling.Objects };

		string json = JsonConvert.SerializeObject(fahrzeuge, Newtonsoft.Json.Formatting.Indented);
		File.WriteAllText(filePath, json);

		string readJson = File.ReadAllText(filePath);
		List<Fahrzeug> parsedJson = JsonConvert.DeserializeObject<List<Fahrzeug>>(readJson);

		JToken jt = JToken.Parse(readJson);
		foreach (JToken fzg in jt) //Children durchgehen
		{
			Console.WriteLine(fzg["MaxGeschwindigkeit"].Value<int>()); //Auf Felder in Children zugreifen mit [], Value<T> zum casten

			Fahrzeug f = JsonConvert.DeserializeObject<Fahrzeug>(fzg.ToString()); //JToken Deserialisieren über ToString()
			Console.WriteLine(f.MaxGeschwindigkeit);
		}
	}

	public static void Xml()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Environment.GetFolderPath: Speziellen statischen Pfad holen

		string folderPath = Path.Combine(desktop, "Serialisierung"); //Path.Combine: beliebig viele Pfade kombinieren

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		string filePath = Path.Combine(folderPath, "Test.txt");

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		XmlSerializer xmlS = new XmlSerializer(fahrzeuge.GetType()); //In Klammer den Typ angeben (hier List<Fahrzeug> statt Fahrzeug), typeof(List<Fahrzeug>)
		using FileStream fs = new FileStream(filePath, FileMode.Create); //FileStream mit Create -> Überschreiben
		xmlS.Serialize(fs, fahrzeuge);

		using FileStream readStream = new FileStream(filePath, FileMode.Open); //Diesmal Open weil lesen
		List<Fahrzeug> readXml = xmlS.Deserialize(readStream) as List<Fahrzeug>; //hier muss gecastet werden, keine Methode mit integriertem Generic

		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load(readStream); //XML einlesen als XmlDocument

		List<XmlNode> nodes = xmlDoc.ChildNodes[1].OfType<XmlNode>().ToList(); //[1] um Deklaration oben im Dokument zu überspringen
		foreach (XmlNode node in nodes)
		{
			Console.WriteLine(node.ChildNodes.OfType<XmlNode>().First(e => e.Name == "MaxGeschwindigkeit").InnerText); //Auf Elemente zugreifen mit ChildNodes -> First(Name), InnerText auf den Wert
		}
	}

	public static void CSV()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Environment.GetFolderPath: Speziellen statischen Pfad holen

		string folderPath = Path.Combine(desktop, "Serialisierung"); //Path.Combine: beliebig viele Pfade kombinieren

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		string filePath = Path.Combine(folderPath, "Test.txt");

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		File.WriteAllText(filePath, fahrzeuge.Aggregate(string.Empty, (agg, fzg) => agg + $"{fzg.ID};{fzg.MaxGeschwindigkeit};{fzg.Marke}\n"));

		TextFieldParser tfp = new TextFieldParser(filePath);
		tfp.SetDelimiters(";");

		string[] headers = tfp.ReadFields();

		List<Fahrzeug> fzgs = new();
		while (!tfp.EndOfData)
		{
			Fahrzeug f = new();
			string[] fields = tfp.ReadFields();
			f.ID = int.Parse(fields[0]);
			f.MaxGeschwindigkeit = int.Parse(fields[1]);
			f.Marke = (FahrzeugMarke) int.Parse(fields[2]);
			fzgs.Add(f);
		}
	}

	public static void Binary()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Environment.GetFolderPath: Speziellen statischen Pfad holen

		string folderPath = Path.Combine(desktop, "Serialisierung"); //Path.Combine: beliebig viele Pfade kombinieren

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		string filePath = Path.Combine(folderPath, "Test.txt");

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		BinaryFormatter formatter = new BinaryFormatter();
		using FileStream fs = new FileStream(filePath, FileMode.Create);
		formatter.Serialize(fs, fahrzeuge); //Wie bei XML

		using FileStream readStream = new FileStream(filePath, FileMode.Open);
		List<Fahrzeug> readFahrzeug = formatter.Deserialize(readStream) as List<Fahrzeug>;
	}
}

[Serializable] //Notwendig für Binary
public class Fahrzeug
{
	[XmlIgnore] //Feld beim Serialisieren ignorieren
	[XmlAttribute] //Attribute statt Element
	public int ID { get; set; }

	[JsonPropertyName("MaxV")] //Name vom Feld im Json angeben
	[JsonProperty("MaxV")] //Newtonsoft Json Equivalent zu JsonPropertyName
	public int MaxGeschwindigkeit { get; set; }

	[System.Text.Json.Serialization.JsonIgnore] //Feld beim Serialisieren ignorieren
	[Newtonsoft.Json.JsonIgnore]
	public FahrzeugMarke Marke { get; set; }

	public Fahrzeug(int id, int v, FahrzeugMarke fm)
	{
		ID = id;
		MaxGeschwindigkeit = v;
		Marke = fm;
	}

	public Fahrzeug() { }
}

public enum FahrzeugMarke { Audi, BMW, VW }