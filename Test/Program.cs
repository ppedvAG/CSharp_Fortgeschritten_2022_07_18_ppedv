namespace Test
{
	internal class Program
	{
		static void Main(string[] args)
		{
			ITest t;
			//t.TestMethode();
		}
	}

	interface ITest
	{
		public static int Etwas => 20; //OK

		public void TestMethode()
		{
			//bad practice
		}
	}
}