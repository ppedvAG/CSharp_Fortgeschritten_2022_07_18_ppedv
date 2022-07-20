using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace Benchmark;

[MemoryDiagnoser(false)] //Speicherbedarf messen (standardmäßig GC auch dabei, false um GEN auszuschalten)
[RankColumn] //Rangliste für Geschwindigkeit der Benchmarks
[IterationsColumn] //IterationCount - Outlier Count
//[Orderer(SummaryOrderPolicy.FastestToSlowest)]

//Exporter
[JsonExporter]
[XmlExporter]
[CsvExporter]
[AsciiDocExporter]
[RPlotExporter] //RPlot generieren (braucht entsprechende Software)
public class Benchmarks
{
	public List<Fahrzeug> Fahrzeuge;

	public string FolderPath;

	[Params(1000, 5000, 10000)]
	public int AnzFahrzeuge;

	//[Benchmark]
	[IterationCount(50)]
	public void StringPlus()
	{
		string s = "";
		for (int i = 0; i < 10000; i++)
			s += i;
	}

	//[Benchmark]
	[IterationCount(50)]
	public void StringBuilderTest()
	{
		StringBuilder sb = new();
		for (int i = 0; i < 100000; i++)
			sb.Append(i);
		sb.ToString();
	}

	[GlobalSetup]
	public void Setup()
	{
		Fahrzeuge = new();
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
		FolderPath = Path.Combine(desktop, "Benchmark");
		if (!Directory.Exists("Benchmark"))
			Directory.CreateDirectory(FolderPath);

		Random rnd = new Random();
		for (int i = 0; i < AnzFahrzeuge; i++)
			Fahrzeuge.Add(new Fahrzeug(i, rnd.Next(100, 500), (FahrzeugMarke) rnd.Next(0, 3)));
	}

	[GlobalCleanup]
	public void Cleanup()
	{
		Directory.Delete(FolderPath, true);
	}

	[Benchmark]
	[IterationCount(50)]
	public void SystemJson()
	{
		string path = Path.Combine(FolderPath, @"System.json");
		Stream s = new FileStream(path, FileMode.Create);
		System.Text.Json.JsonSerializerOptions options = new System.Text.Json.JsonSerializerOptions();
		options.IncludeFields = true;
		System.Text.Json.JsonSerializer.Serialize(s, Fahrzeuge, options);
		s.Dispose();
	}

	[Benchmark]
	[IterationCount(50)]
	public void NewtonsoftJson()
	{
		string path = Path.Combine(FolderPath, @"Newtonsoft.json");
		Newtonsoft.Json.JsonSerializer js = Newtonsoft.Json.JsonSerializer.Create();
		StreamWriter sw = new StreamWriter(path);
		js.Serialize(sw, Fahrzeuge);
		sw.Dispose();
	}

	[Benchmark]
	[IterationCount(50)]
	public void Xml()
	{
		string path = Path.Combine(FolderPath, @"Xml.xml");
		Stream s = new FileStream(path, FileMode.Create);
		XmlSerializer xml = new XmlSerializer(typeof(List<Fahrzeug>));
		xml.Serialize(s, Fahrzeuge);
		s.Dispose();
	}

	[Benchmark]
	[IterationCount(50)]
	public void Binary()
	{
		string path = Path.Combine(FolderPath, @"Binary.bin");
		Stream s = new FileStream(path, FileMode.Create);
		BinaryFormatter formatter = new BinaryFormatter();
		formatter.Serialize(s, Fahrzeuge);
		s.Dispose();
	}
}
