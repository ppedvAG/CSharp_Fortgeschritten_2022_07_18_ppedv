namespace Multithreading;

internal class _05_ThreadMitReturn
{
	static string ReturnString = string.Empty;

	static void Main(string[] args)
	{
		Thread t = new Thread(new ParameterizedThreadStart(ToUpper));
		t.Start();
	}

	static void ToUpper(object o)
	{
		if (o is string s)
		{
			ReturnString = s.ToUpper();
		}
	}
}
