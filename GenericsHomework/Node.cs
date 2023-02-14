using System.Collections;

namespace GenericsHomework;

public class Node<TValue> : ICollection<TValue>, IReadOnlyNode<TValue>
	where TValue : notnull
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
		Node<TValue> last = this;
		do
		{
			if (last.Value.Equals(value))
				throw new InvalidOperationException($"Cannot append duplicate value `{value}` to list.");
			last = last.Next;
		}
		while (last != this);

		var appendNode = new Node<TValue>(value, next: Next);
		Next = appendNode;

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
		Node<TValue> current = this;
		do
		{
			if (current.Value.Equals(value))
				return true;
			current = current.Next;
		}
		while (current != this);

		return false;
	}

	IReadOnlyNode<TValue> IReadOnlyNode<TValue>.Next { get => Next; }

	#region Implements ICollection<TValue>
	public void Add(TValue item)
		=> Append(item);

	public bool Contains(TValue item)
		=> Exists(item);

	public void CopyTo(TValue[] array, int arrayIndex)
	{
		int currentIndex = arrayIndex;
		Node<TValue> current = this;
		do
		{
			array[currentIndex++] = current.Value;
			current = current.Next;
		}
		while (current != this);
	}

	public bool Remove(TValue item)
	{
		if (Value.Equals(item) && Next == this)
		{
			throw new InvalidOperationException($"Cannot remove item from a list if it would remove the only value contained in the list.");
		}

		Node<TValue> current = this;
		do
		{
			Node<TValue> next = current.Next;
			if (next.Value.Equals(item))
			{
				current.Next = next.Next;
				next.Next = next;
				return true;
			}
			current = next;
		}
		while (current != this);
		return false;
	}

	public IEnumerator<TValue> GetEnumerator()
	{
		Node<TValue> current = this;
		do
		{
			yield return current.Value;
			current = current.Next;
		}
		while (current != this);
	}

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();

	public int Count
	{
		get
		{
			int count = 1;
			Node<TValue> current = this;
			while (current.Next != this)
			{
				count++;
				current = current.Next;
			}
			return count;
		}
	}

	public bool IsReadOnly => false;
	#endregion
}