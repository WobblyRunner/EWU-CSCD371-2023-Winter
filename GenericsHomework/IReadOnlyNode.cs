namespace GenericsHomework;

public interface IReadOnlyNode<TValue>
{
	TValue Value { get; }
	IReadOnlyNode<TValue> Next { get; }
	bool Exists(TValue value);
}
