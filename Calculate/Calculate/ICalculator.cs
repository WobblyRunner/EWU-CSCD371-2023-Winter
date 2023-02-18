using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace Calculate;

public interface ICalculator
{
	bool TryCalculate(string expression, [MaybeNullWhen(false)] out object result);
}

public interface ICalculator<TNumber> : ICalculator
	where TNumber : INumber<TNumber>
{
	abstract static Func<TNumber, TNumber, TNumber> Add { get; }
	abstract static Func<TNumber, TNumber, TNumber> Subtract { get; }
	abstract static Func<TNumber, TNumber, TNumber> Multiply { get; }
	abstract static Func<TNumber, TNumber, TNumber> Divide { get; }

	IReadOnlyDictionary<char, Func<TNumber, TNumber, TNumber>> MathematicalOperations { get; }

	bool TryCalculate(string expression, [MaybeNullWhen(false)] out TNumber result);
}