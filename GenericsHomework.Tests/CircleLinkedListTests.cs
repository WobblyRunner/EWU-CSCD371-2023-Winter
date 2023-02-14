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
			string? rootToString;

			rootToString = root.ToString();

			Assert.IsNotNull(rootToString, "ToString result is null");
			Assert.AreEqual<string>(value.ToString(), rootToString, "ToString result is not the same result as the node's value");
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
		public void Node_Append_ThrowsArgumentExceptionOnDuplicateNotRoot()
		{
			Node<int> root = new(0);
			const int value = 42;

			root.Append(value);

			Assert.ThrowsException<ArgumentException>(() => root.Append(value));
		}

		[TestMethod]
		public void Node_Append_ThrowsArgumentExceptionOnDuplicateRootValue()
		{
			const int value = 100;
			Node<int> root = new(value);

			Assert.ThrowsException<ArgumentException>(() => root.Append(value));
		}

		[TestMethod]
		public void Node_AppendRange_ReturnsAllAddedNodes()
		{
			Node<int> root = new(0);
			const int value1 = 5, value2 = 10, value3 = 25;
			int[] values = { value1, value2, value3 };
			Node<int>[] appended;
			int[] appendedValueCast;
			
			appended = root.Append(values);
			appendedValueCast = appended.Select(node => node.Value).ToArray();

			CollectionAssert.AreEquivalent(appendedValueCast, values);
		}

		[TestMethod]
		public void Node_AppendRange_ReturnsNodesInOrder()
		{
			Node<int> root = new(0);
			const int value1 = 5, value2 = 10, value3 = 25;
			int[] values = { value1, value2, value3 };
			Node<int>[] appended;
			
			appended = root.Append(values);

			Assert.AreEqual<int>(appended[0].Value, value1);
			Assert.AreEqual<int>(appended[1].Value, value2);
			Assert.AreEqual<int>(appended[2].Value, value3);
		}

		[TestMethod]
		public void Node_AppendRange_NoOperationOnEmpty()
		{
			Node<int> root = new(0);

			root.Append(Array.Empty<int>());

			Assert.AreEqual<Node<int>>(root, root.Next);
		}

		[TestMethod]
		public void Node_AppendRange_ThrowsArgumentExceptionOnDuplicate()
		{
			Node<int> root = new(0);
			const int value1 = 5, value2 = 5, value3 = 5;
			int[] values = { value1, value2, value3 };

			Assert.ThrowsException<ArgumentException>(() => root.Append(values));
		}

		[TestMethod]
		public void Node_Clear_NextReferenceIsLostForAll()
		{
			/* In a circular linked list, we can practically treat any node as the root node as the collection is inherently rootless.
			 *	In this case, each node can effectively be treated an encapsulation of a container for the entire list.
			 *	This means that when we Clear() the list, developers will expect that entire list to become destroyed and each node orphanized. */

			Node<int> root = new(0);
			Node<int> next1, next2, next3;

			root.Append(1, 2, 3);
			next1 = root.Next;
			next2 = root.Next.Next;
			next3 = root.Next.Next.Next;
			root.Clear();

			Assert.AreEqual<Node<int>>(root, root.Next);
			Assert.AreEqual<Node<int>>(next1, next1.Next);
			Assert.AreEqual<Node<int>>(next2, next2.Next);
			Assert.AreEqual<Node<int>>(next3, next3.Next);
		}

		[TestMethod]
		public void Node_Exists_ReturnsTrueOnExists()
		{
			Node<int> root = new(0);
			int[] values = { 1, 2, 3 };
			bool exists = false;

			root.Append(values);
			exists = root.Exists(values[0]);

			Assert.IsTrue(exists);
		}

		public void Node_Exists_ReturnsFalseOnNotExists()
		{
			Node<int> root = new(0);
			int[] values = { 4, 5, 6 };
			bool exists = true;
			const int notExistingValue = 42;

			root.Append(values);
			exists = root.Exists(notExistingValue);

			Assert.IsFalse(exists);
		}
	}
}