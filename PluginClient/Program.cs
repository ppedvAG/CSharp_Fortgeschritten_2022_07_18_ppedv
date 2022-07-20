using PluginBase;
using System.Reflection;

namespace PluginClient
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string path = @"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2022_07_18\TestPlugin\bin\Debug\net6.0\TestPlugin.dll";

			Assembly a = Assembly.LoadFrom(path);
			Type pluginType = a.DefinedTypes.First(e => e.GetInterface("ISpecificPlugin") != null); //Typ holen über ISpecificPlugin

			ISpecificPlugin plugin = Activator.CreateInstance(pluginType) as ISpecificPlugin; //Objekt vom Plugin erstellen

			plugin.Function1(); //Funktionen aufrufen
			Console.WriteLine(plugin.Method2(4, 4));
		}
	}
}