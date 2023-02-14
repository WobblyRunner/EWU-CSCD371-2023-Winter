using System.Xml.Linq;

namespace GenericsHomework;

public class Node<TValue> where TValue : notnull
{
	public TValue Value { get; set; }

	public Node<TValue> Next { get; private set; }

	public Node(TValue value)
	{
		Value = value;
		Next = this;
	}

	private Node(TValue value, Node<TValue> next)
	{
		Value = value;		
		Next = next;
	}

	public override string? ToString()
		=> Value?.ToString();

	public Node<TValue> Append(TValue value)
	{
		// Navigate to the end of the collection
		Node<TValue> last;
		for (last = this; last.Next != this; last = last.Next)
		{
			if (last.Value.Equals(value))
				throw new ArgumentException($"Cannot append duplicate value `{value}` to list.");
		}
		if (last.Value.Equals(value))
			throw new ArgumentException($"Cannot append duplicate value `{value}` to list.");

		var appendNode = new Node<TValue>(value, next: this);
		last.Next = appendNode;

		return appendNode;
	}

	public Node<TValue>[] Append(params TValue[] values)
	{
		Node<TValue>[] added = new Node<TValue>[values.Length];
		for (int i = 0; i < values.Length; i++)
		{
			added[i] = Append(values[i]);
		}
		return added;
	}

	public void Clear()
	{
		/* In a circular linked list, we can practically treat any node as the root node as the collection is inherently rootless.
		 *	In this case, each node can effectively be treated an encapsulation of a container for the entire collection.
		 *	This means that when we Clear() the collection in this API, developers will expect that the entire collection becomes destroyed and each node orphanized.
		 *	Otherwise, you are not clearing the list. You are simply removing a node, or item, from the list. */

		Node<TValue> current = this;
		while (current.Next != this)
		{
			(current, current.Next) = (current.Next, current);
		}
		current.Next = current;
	}

	public bool Exists(TValue value)
	{
		for (Node<TValue> current = this; current.Next != this; current = current.Next)
		{
			if (current.Value.Equals(value))
				return true;
		}
		return false;
	}
}