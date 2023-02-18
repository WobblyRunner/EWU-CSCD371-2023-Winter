using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace Calculate.ExtraCredit;

public class GenericCalculator<TNumber> : ICalculator<TNumber>
	where TNumber : INumber<TNumber>
{
	public static Func<TNumber, TNumber, TNumber> Add { get; set; } = TNumber (TNumber lhs, TNumber rhs) => lhs + rhs;

	public static Func<TNumber, TNumber, TNumber> Subtract { get; set; } = TNumber (TNumber lhs, TNumber rhs) => lhs - rhs;

	public static Func<TNumber, TNumber, TNumber> Multiply { get; set; } = TNumber (TNumber lhs, TNumber rhs) => lhs * rhs;

	public static Func<TNumber, TNumber, TNumber> Divide { get; set; } = TNumber (TNumber lhs, TNumber rhs) => lhs / rhs;

	public IReadOnlyDictionary<char, Func<TNumber, TNumber, TNumber>> MathematicalOperations { get; }
	protected char[] Operators { get; }

	public GenericCalculator()
	{
		MathematicalOperations = new Dictionary<char, Func<TNumber, TNumber, TNumber>>()
		{
			{ '+', Add },
			{ '-', Subtract },
			{ '*', Multiply },
			{ '/', Divide },
		};
		Operators = MathematicalOperations.Keys.ToArray();
	}

	public bool TryCalculate(string expression, [MaybeNullWhen(false)] out TNumber result)
	{
		int index = expression.LastIndexOfAny(Operators);
		if (index > -1)
		{
			if (TNumber.TryParse(expression[(index+1)..], System.Globalization.NumberStyles.Number, null, out TNumber? right)
				&& TryCalculate(expression[..index], out TNumber? left))
			{
				result = MathematicalOperations[expression[index]].Invoke(left, right);
				return true;
			}
			result = default;
			return false;
		}
		return TNumber.TryParse(expression[(index+1)..], System.Globalization.NumberStyles.Number, null, out result);
	}

	bool ICalculator.TryCalculate(string expression, [MaybeNullWhen(false)] out object result)
	{
		bool success = TryCalculate(expression, out TNumber? number);
		result = success ? number : null;
		return success;
	}
}
