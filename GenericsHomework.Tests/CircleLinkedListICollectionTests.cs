namespace GenericsHomework.Tests;

[TestClass]
public class CircleLinkedListICollectionTests
{
	[TestInitialize]
	public void TestInitialize()
	{
	}

	[TestMethod]
	public void Node_Count_IsActualCount()
	{
		Node<double> list = new Node<double>(15.2) { 20.0, 30.1234, 42.998 };
		int count = 0;

		count = list.Count;

		Assert.AreEqual(4, count);
	}

	[TestMethod]
	public void Node_Add_ContainsAddedValue()
	{
		Node<string> list = new Node<string>("Hello, ");
		const string toAdd = "world!";

		list.Add(toAdd);

		Assert.IsTrue(list.Contains(toAdd));
	}

	[TestMethod]
	public void Node_Remove_DoesNotContainRemovedValue()
	{
		const string toAdd = "world!";
		Node<string> list = new Node<string>("Hello, ") { toAdd };

		list.Remove(toAdd);
		
		Assert.IsFalse(list.Contains(toAdd));
	}

	[TestMethod]
	[ExpectedException(typeof(InvalidOperationException))]
	public void Node_Remove_ThrowsInvalidOperationExceptionIfSingleNodeList()
	{
		const char value = 'A';
		Node<char> list = new Node<char>(value);

		list.Remove(value);
	}

	[TestMethod]
	public void Node_CopyTo_ReturnsEquivalentArray()
	{
		int[] values = { 1, 2, 3, 5, 8, 13, 20, 40 };
		Node<int> list = new Node<int>(1) { 2, 3, 5, 8, 13, 20, 40 };
		int[] copied = new int[list.Count];

		list.CopyTo(copied, 0);

		CollectionAssert.AreEquivalent(values, copied);
	}

	[TestMethod]
	public void Node_GetEnumerator_ReturnsSummedValuesIteratively()
	{
		Node<int> list = new Node<int>(10) { 20, 50 };
		int actualSum = 0, expectedSum = 80;
		
		foreach (int value in list)
			actualSum += value;

		Assert.AreEqual<int>(expectedSum, actualSum);
	}
}
