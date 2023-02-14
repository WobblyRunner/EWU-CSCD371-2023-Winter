namespace GenericsHomework.Tests
{
	[TestClass]
	public class CircleLinkedListTests
	{
		[TestInitialize]
		public void TestInitialize()
		{
		}

		[TestMethod]
		public void Node_ToString_ReturnsValueToString()
		{
			const int value = 1337;
			Node<int> root = new(value);
			string rootToString;

			rootToString = root.ToString();

			Assert.IsNotNull(rootToString);
			Assert.AreEqual<string>(value.ToString(), rootToString);
		}

		[TestMethod]
		public void Node_Next_ReturnsSelfWhenEmpty()
		{
			Node<int> root = new(0), next;

			next = root.Next;
			
			Assert.AreEqual<Node<int>>(root, next);
		}

		[TestMethod]
		public void Node_Next_ReturnsNextWhenNotEmpty()
		{
			Node<int> root = new(0);
			const int value = 25;
			Node<int> next;

			root.Append(25);
			next = root.Next;

			Assert.IsNotNull(next);
			Assert.AreNotEqual<Node<int>>(root, next);
		}

		[TestMethod]
		public void Node_Append_ReturnsAddedNode()
		{
			Node<int> root = new(0);
			const int value = 25;
			Node<int> appended, next;

			appended = root.Append(value);
			next = root.Next;

			Assert.AreEqual<Node<int>>(appended, next, "References are not the same");
			Assert.AreEqual<int>(next.Value, value, "Values are not the same");
		}

		[TestMethod]
		public void Node_Append_ThrowsArgumentExceptionOnDuplicate()
		{
			Node<int> root = new(0);
			const int value = 42;

			root.Append(value);

			Assert.ThrowsException<ArgumentException>(() => root.Append(value));
		}

		[TestMethod]
		public void Node_Clear_NextReferenceIsLost()
		{
			Node<int> root = new(0);
			const int value = 25;
			Node<int> next, nextAfterClear;

			root.Append(value);
			next = root.Next;
			root.Clear();
			nextAfterClear = root.Next;

			Assert.AreNotEqual<Node<int>>(next, nextAfterClear);
			Assert.AreEqual<Node<int>>(root, nextAfterClear);
			Assert.IsNotNull(nextAfterClear);
		}

		[TestMethod]
		public void Node_Exists_ReturnsTrueOnExists()
		{
			Node<int> root = new(0);
			ReadOnlySpan<int> values = stackalloc int[3] { 1, 2, 3 };
			bool exists = false;

			root.Append(values);
			exists = root.Exists(values[0]);

			Assert.IsTrue(exists);
		}

		public void Node_Exists_ReturnsFalseOnNotExists()
		{
			Node<int> root = new(0);
			ReadOnlySpan<int> values = stackalloc int[3] { 4, 5, 6 };
			bool exists = true;
			const int notExistingValue = 42;

			root.Append(values);
			exists = root.Exists(notExistingValue);

			Assert.IsFalse(exists);
		}
	}
}