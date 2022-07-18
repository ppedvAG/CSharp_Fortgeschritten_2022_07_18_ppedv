using System.Collections;

namespace Generics;

internal class Program
{
	static void Main(string[] args)
	{
		List<string> list = new List<string>(); //Generic: T wird nach unten übernommen (hier T = string)
		list.Add("Max"); //T wird durch string ersetzt: Add(T) -> Add(string)

		Dictionary<string, int> dictionary = new Dictionary<string, int>(); //Klasse mit 2 Generics: TKey (string), TValue (int)
		dictionary.Add("Max", 1); //string als TKey, int als TValue: Add(TKey, TValue) -> Add(string, int)

		foreach (KeyValuePair<string, int> kv in dictionary) { } //Dictionary iterieren
	}
}

public class DataStore<T> //Beim Erstellen eines Objekts muss ein Generic angegeben werden
	: IProgress<T>, //T bei Vererbung übergeben
	  IEnumerable<int>
{
	private T[] data = new T[10]; //Array vom Typ T

	public List<T> Data => data.ToList(); //T nach unten in ein weiteres Generic übergeben

	public void Add(int index, T item) //Generic in Methodenparameter
	{
		data[index] = item;
	}

	public T GetIndex(int index) //T als Return Wert
	{
		if (index < 0 || index >= data.Length)
			return default(T); //default(T): Standardwert von T (int: 0, string: null, bool: false, Program: null)
		return data[index];
	}

	public void PrintType<MyType>() //Generic in Methode
	{
		Console.WriteLine(typeof(MyType));
		Console.WriteLine(nameof(MyType)); //MyType als string: "MyType"
		Console.WriteLine(default(MyType));
	}

	public void Report(T value) //T wird von oben weitergegeben
	{
		throw new NotImplementedException();
	}

	IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();

	public IEnumerator<int> GetEnumerator() //Oben bei Vererbung fixen Typ übergeben (hier int)
	{
		throw new NotImplementedException();
	}
}

public class DataStore2<T> : DataStore<T> //Klassen mit T vererben: braucht wieder T beim Klassennamen
{

}