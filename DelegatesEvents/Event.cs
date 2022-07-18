namespace DelegatesEvents;

internal class Event
{
	static event EventHandler TestEvent; //Event definieren wie beim Delegate

	static event EventHandler<TestEventArgs> ArgsEvent;

	static void Main(string[] args)
	{
		TestEvent += Event_TestEvent; //Extra Methode anhängen (ohne new)
		TestEvent += (sender, args) => Console.WriteLine("Anonymes Event"); //Anonymes Event wie bei Action/Func/Predicate
		TestEvent(null, EventArgs.Empty); //Aufruf (kein sender da in static Methode)

		ArgsEvent += Event_ArgsEvent;
		ArgsEvent(null, new TestEventArgs("ArgsEvent aufgerufen"));
	}

	private static void Event_ArgsEvent(object sender, TestEventArgs e) //TestEventArgs als Parameter wegen Generic oben
	{
		Console.WriteLine(e.Status);
	}

	private static void Event_TestEvent(object sender, EventArgs e)
	{
		Console.WriteLine("Test");
	}
}

public class TestEventArgs : EventArgs //Eigene EventArgs definieren
{
	public string Status { get; set; }

	public TestEventArgs(string status) => this.Status = status;
}