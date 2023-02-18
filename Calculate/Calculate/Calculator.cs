using System.Diagnostics.CodeAnalysis;

namespace Calculate;

public class Calculator : ICalculator<int>
{
	public static Func<int, int, int> Add { get; set; } = (lhs, rhs) => lhs + rhs;
	public static Func<int, int, int> Subtract { get; set; } = (lhs, rhs) => lhs - rhs;
	public static Func<int, int, int> Multiply { get; set; } = (lhs, rhs) => lhs * rhs;
	public static Func<int, int, int> Divide { get;	set; } = (lhs, rhs) => lhs / rhs;

	public IReadOnlyDictionary<char, Func<int, int, int>> MathematicalOperations { get; }
	protected char[] Operators { get; }

	public Calculator()
	{
		MathematicalOperations = new Dictionary<char, Func<int, int, int>>()
		{
			{ '+', Add },
			{ '-', Subtract },
			{ '*', Multiply },
			{ '/', Divide },
		};
		Operators = MathematicalOperations.Keys.ToArray();
	}

	public bool TryCalculate(string expression, out int result)
	{
		int index = expression.LastIndexOfAny(Operators);
		if (index > -1)
		{
			if (int.TryParse(expression[(index+1)..], out int right)
				&& TryCalculate(expression[..index], out int left))
			{
				result = MathematicalOperations[expression[index]].Invoke(left, right);
				return true;
			}
			result = 0;
			return false;
		}
		return int.TryParse(expression, out result);
	}

	bool ICalculator.TryCalculate(string expression, [MaybeNullWhen(false)] out object result)
	{
		bool success = TryCalculate(expression, out int number);
		result = success ? number : null;
		return success;
	}
}
