using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace JsonSplit
{
	public partial class MainWindow : Window
	{
		public MainWindow() => InitializeComponent();

		/// <summary>
		/// Diese Methode soll die originale Json Datei laden, aufteilen und in die einzelnen Dateien speichern.
		/// Diese Methode ist mit dem linken Button (Split Json) in der UI verbunden
		/// </summary>
		private async void SaveSplitJson(object sender, EventArgs e)
		{
			
		}

		/// <summary>
		/// Diese Methode soll die aufgeteilten Json Dateien wieder einlesen und danach in einem Dictionary bereit stellen.
		/// Diese Methode ist mit dem rechten Button (Load Json) in der UI verbunden
		/// </summary>
		private async void LoadSplitJsonFiles(object sender, RoutedEventArgs e)
		{

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
