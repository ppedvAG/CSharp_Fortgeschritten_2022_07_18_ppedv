namespace DelegatesEvents;

internal class ComponentWithEvent
{
	static void Main(string[] args)
	{
		Component comp = new();
		comp.ValueChanged += (counter) => Console.WriteLine("Zähler: " + counter); //Action mit einem Parameter (counter). Wenn unten das Event aufgerufen wird, kommt der Wert hier hinaus
		comp.ProcessCompleted += () => Console.WriteLine("Fertig"); //Action ohne Parameter mit ()
		comp.StartProcess();
	}
}

public class Component
{
	public event Action ProcessCompleted; //Action, Predicate, Func hier möglich

	public event Action<int> ValueChanged;

	public void StartProcess()
	{
		for (int i = 0; i < 10; i++)
		{
			//Längerer Prozess
			ValueChanged?.Invoke(i); //Hier auf jeden Fall ?.Invoke benutzen
			Thread.Sleep(100);
		}
		ProcessCompleted?.Invoke();
	}
}