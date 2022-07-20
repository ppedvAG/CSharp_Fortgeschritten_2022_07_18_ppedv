using PluginBase;

namespace TestPlugin
{
	internal class Program : ISpecificPlugin
	{
		public string Name { get => "TestPlugin"; }

		public string Description { get => "Das ist ein TestPlugin"; }

		public void Function1()
		{
			Console.WriteLine("Function1 wurde aufgerufen");
		}

		public int Method2(int x, int y)
		{
			return x + y;
		}
	}
}