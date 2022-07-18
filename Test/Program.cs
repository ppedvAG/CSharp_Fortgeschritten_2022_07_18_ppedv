namespace Test;

internal class Program
{
	static void Main(string[] args)
	{
		Node<SolarItem> node = new Node<Sun>();
	}
}

public interface Node<out T>
	where T : SolarItem
{
	//public T Self;

	//public Node<T> Parent;

	//public List<Node<T>> Children;
}

public class SolarItem { }

public class Sun : SolarItem { }

public class Planet : SolarItem { }

public class Moon : SolarItem { }