namespace SolarSystemSolution;

public class Node<T>
{
	private T _item;

	private Node<T> _parentNode = default!;

	private List<Node<T>> _children = new List<Node<T>>();


	public T Item
	{
		get { return _item; }
		set { _item = value; }
	}

	public List<Node<T>> Childrens { get => _children; set => _children = value; }
	public Node<T> ParentNode { get => _parentNode; set => _parentNode = value; }


	public Node(T item)
	{
		_item = item;
	}

	public Node(T item, Node<T> parentNode) : this(item)
	{
		_parentNode = parentNode;
	}

	public void SetParentNode(Node<T> parentNode)
	{
		ParentNode = parentNode;
	}

	public void SetParentNodeInChilds()
	{
		foreach (Node<T> child in _children)
			child.SetParentNode(this);
	}

	public void AddChild(T child)
	{
		Childrens.Add(new Node<T>(child, this));
	}

	public void AddChild(Node<T> child)
	{
		child.ParentNode = this;
		Childrens.Add(child);
	}

	public void RemoveChild(T child)
	{
		var node = Childrens.FirstOrDefault(e => e.Item.Equals(child));
		if (node != null)
			Childrens.Remove(node);
	}
}
