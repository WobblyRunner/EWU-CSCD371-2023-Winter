using System.Diagnostics.CodeAnalysis;
using System.Runtime.Intrinsics.Arm;
using System.Security.Principal;

namespace Calculate;

public class Calculator
{
	public delegate decimal BinaryOperation(decimal lhs, decimal rhs);

	public static BinaryOperation Add { get; set; } = (lhs, rhs) => lhs + rhs;
	public static BinaryOperation Subtract { get; set; } = (lhs, rhs) => lhs - rhs;
	public static BinaryOperation Multiply { get; set; } = (lhs, rhs) => lhs * rhs;
	public static BinaryOperation Divide { get; set; } = (lhs, rhs) => lhs / rhs;

	public IReadOnlyDictionary<char, BinaryOperation> MathematicalOperations { get; }

	public Calculator()
	{
		MathematicalOperations = new Dictionary<char, BinaryOperation>()
		{
			{ '+', Add },
			{ '-', Subtract },
			{ '*', Multiply },
			{ '/', Divide }
		};
	}

	private (Stack<decimal> Values, Stack<BinaryOperation> Operations) Parse(string toParse)
	{
		Stack<decimal> values = new();
		Stack<BinaryOperation> operations = new();

		char[] ops = MathematicalOperations.Keys.ToArray();
		int index = toParse.LastIndexOfAny(ops);
		do
		{
			index = toParse.LastIndexOfAny(ops);
			if (index > -1)
			{
				char op = toParse[index];
				operations.Push(MathematicalOperations[op]);
			}

			string operand = index == -1 ? toParse : toParse[(index+1)..];
			if (decimal.TryParse(operand, out decimal value))
				values.Push(value);

			if (index > -1)
				toParse = toParse[0..index];
		} while (index > -1);
		
		return (values, operations);
	}

	private decimal StackCalculate(Stack<decimal> values, Stack<BinaryOperation> operations)
	{
		if (values.Count == 1)
		{
			return values.Pop();
		}
		decimal lhs = values.Pop(), rhs = values.Pop();
		BinaryOperation operation = operations.Pop();
		values.Push(operation(lhs, rhs));
		return StackCalculate(values, operations);
	}

	public bool TryCalculate(string expression, out decimal result)
	{
		result = 0;

		var (values, operations) = Parse(expression);
		result = StackCalculate(values, operations);

		return true;
	}
}