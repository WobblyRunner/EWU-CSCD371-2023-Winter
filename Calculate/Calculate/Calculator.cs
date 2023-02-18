using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace Calculate;

public class Calculator
{
	public delegate decimal BinaryOperation(decimal left, decimal right);

	public static BinaryOperation Add { get; set; } = (lhs, rhs) => lhs + rhs;
	public static BinaryOperation Subtract { get; set; } = (lhs, rhs) => lhs - rhs;
	public static BinaryOperation Multiply { get; set; } = (lhs, rhs) => lhs * rhs;
	public static BinaryOperation Divide { get;	set; } = (lhs, rhs) => lhs / rhs;

	public IReadOnlyDictionary<char, BinaryOperation> MathematicalOperations { get; }
	protected char[] Operators { get; }

	public Calculator()
	{
		MathematicalOperations = new Dictionary<char, BinaryOperation>()
		{
			{ '+', Add },
			{ '-', Subtract },
			{ '*', Multiply },
			{ '/', Divide },
		};
		Operators = MathematicalOperations.Keys.ToArray();
	}

	public bool TryCalculate(string expression, out decimal result)
	{
		int index = expression.LastIndexOfAny(Operators);
		if (index > -1)
		{
			if (decimal.TryParse(expression[(index+1)..], out decimal right)
				&& TryCalculate(expression[..index], out decimal left))
			{
				result = MathematicalOperations[expression[index]].Invoke(left, right);
				return true;
			}
			result = 0;
			return false;
		}
		return decimal.TryParse(expression, out result);
	}
}
