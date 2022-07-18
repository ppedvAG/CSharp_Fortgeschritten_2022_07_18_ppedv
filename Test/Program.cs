namespace Sprachfeatures; //File-Scoped Namespace: alle Member in diesem File haben den Namespace

internal class Program
{
	static void Main(string[] args)
	{
		ITest t;
		//t.TestMethode();

		Person p = new(1, "Test", null); //Target-Typed new

		//{ p.Vorgesetzter.Vorgesetzter.Vorgesetzter } == null; //Property Pattern mit weniger Klammern

		//Test("");
		//Test(null);
	}

	//static void Test(string s!!) //null-Check bei Parameterdeklaration, wirft ArgumentNullException wenn Parameter null
	//{ }
}

interface ITest
{
	public static int Etwas => 20; //OK

	public void TestMethode()
	{
		//bad practice
	}
}