using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace TaskTest
{
	public partial class MainWindow : Window
	{
		public MainWindow() => InitializeComponent();

		private async void SaveSplitJson(object sender, EventArgs e)
		{
			ButtonGrid.IsEnabled = false;
			
			using StreamReader sr = new StreamReader("history.city.list.min.json");
			JsonDocument jd = await JsonDocument.ParseAsync(sr.BaseStream);
			//StreamReader erstellen und BaseStream davon als Stream-Parameter hier benutzen
			//Json File einlesen mit await um UI-Freeze zu verhindern

			Directory.Delete("Lab", true); //Übung zurücksetzen
			Directory.CreateDirectory("Lab");

			Dictionary<string, List<JsonElement>> cities = new();
			foreach (JsonElement je in jd.RootElement.EnumerateArray()) //Json Datei iterieren
			{
				string countryCode = je.GetProperty("city").GetProperty("country").GetString(); //Auf den CountryCode zugreifen (AT, DE, IT, ...)
				if (!cities.ContainsKey(countryCode))
					cities.Add(countryCode, new());
				cities[countryCode].Add(je);
			}

			Dispatcher.Invoke(() => Progress.Maximum = cities.Count); //Maximum der ProgressBar setzen

			//await ForEachAsync um UI nicht freezen zu lassen
			await Parallel.ForEachAsync(cities, (kv, x) => //Json Files schreiben
			{
				File.WriteAllText(Path.Combine("Lab", $"{kv.Key}.json"), JsonListToJson(kv.Value)); //Methode weiter unten
				Dispatcher.Invoke(() => Output.Text += $"{kv.Key} in die Datei geschrieben\n"); //Logging
				Dispatcher.Invoke(() => Scroll.ScrollToEnd()); //Dispatcher.InvokeAsync: Side Threads/Tasks können nicht auf den UI Thread zugreifen, der Dispatcher ermöglicht einen Umweg
				Dispatcher.Invoke(() => Progress.Value++);
				return ValueTask.CompletedTask; //Hier einfach "leeres" Objekt zurückgeben
			});

			//Ohne Parallel, langsamer
			//foreach (KeyValuePair<string, List<JsonElement>> kv in cities) //Json Files schreiben
			//{
			//	await File.WriteAllTextAsync(Path.Combine("Lab", $"{kv.Key}.json"), JsonListToJson(kv.Value)); //Methode weiter unten
			//	await Dispatcher.InvokeAsync(() => Output.Text += $"{kv.Key} in die Datei geschrieben\n"); //Logging
			//	await Dispatcher.InvokeAsync(() => Scroll.ScrollToEnd()); //Dispatcher.InvokeAsync: Side Threads/Tasks können nicht auf den UI Thread zugreifen, der Dispatcher ermöglicht einen Umweg
			//	await Dispatcher.InvokeAsync(() => Progress.Value++);
			//}

			Progress.Value = 0;
			ButtonGrid.IsEnabled = true;
		}

		private async void LoadSplitJsonFiles(object sender, RoutedEventArgs e)
		{
			ButtonGrid.IsEnabled = false;

			ConcurrentDictionary<string, List<JsonElement>> jsons = new(); //Parallel-sicheres Dictionary
			List<string> countryCodes = Directory.GetFiles("Lab").Select(cc => Path.GetFileNameWithoutExtension(cc)).ToList();
			//Alle Dateinamen ohne Pfad und Erweiterung

			Dispatcher.Invoke(() => Progress.Maximum = countryCodes.Count); //Maximum der ProgressBar setzen

			//await ForEachAsync um UI nicht freezen zu lassen
			await Parallel.ForEachAsync(countryCodes, (code, ct) =>
			{
				string path = Path.Combine("Lab", $"{code}.json"); //Einzelnes File angreifen
				string file = File.ReadAllText(path); //File einlesen
				jsons.TryAdd(code, JsonDocument.Parse(file).RootElement.EnumerateArray().ToList()); //Element hinzufügen (hier mit TryAdd)
				Dispatcher.Invoke(() => Output.Text += $"{code} geladen\n");
				Dispatcher.Invoke(() => Scroll.ScrollToEnd());
				Dispatcher.Invoke(() => Progress.Value = jsons.Count); //ProgressBar aktualisieren
				return ValueTask.CompletedTask; //Hier einfach "leeres" Objekt zurückgeben
			});

			//Ohne Parallel, langsamer
			//foreach (string code in countryCodes)
			//{
			//	string path = Path.Combine("Lab", $"{code}.json"); //Einzelnes File angreifen
			//	string file = await File.ReadAllTextAsync(path); //File einlesen
			//	jsons.TryAdd(code, JsonDocument.Parse(file).RootElement.EnumerateArray().ToList()); //Element hinzufügen (hier mit TryAdd)
			//	await Dispatcher.InvokeAsync(() => Output.Text += $"{code} geladen\n");
			//	await Dispatcher.InvokeAsync(() => Scroll.ScrollToEnd());
			//	await Dispatcher.InvokeAsync(() => Progress.Value++); //ProgressBar aktualisieren
			//}

			Progress.Value = 0;
			ButtonGrid.IsEnabled = true;
		}

		//Es gibt keine Methode um aus einer Liste von JsonElements ein JsonArray zu generieren
		private string JsonListToJson(List<JsonElement> jsons)
		{
			return jsons.Aggregate(new StringBuilder("[\n"), (sb, je) =>
				sb.Append('\t')
				  .Append(je.GetRawText())
				  .Append(",\n"))
				  .ToString()
				  .TrimEnd(',', '\n') + "\n]";
		}
	}
}
