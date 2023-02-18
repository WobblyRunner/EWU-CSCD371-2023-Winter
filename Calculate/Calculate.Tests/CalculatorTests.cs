namespace Calculate.Tests;

[TestClass]
public class CalculatorTests
{
	public required Calculator TestCalculator;

	[TestInitialize]
	public void TestInit()
	{
		/* Because delegates/Funcs/Actions are immutable, we have to set the static reference before we instantiate the calculator,
		 *	which will copy the immutable delegate into the new calculator's mathematical operations dictionary.
		 *	As an offer for a 'solution', I'd just make the delegate methods non-static. Otherwise, this has to be treated as intended behavior
		 *	as defined in the assignment guidelines. */

		Calculator.Add = (lhs, rhs) => lhs + rhs;
		Calculator.Subtract = (lhs, rhs) => lhs - rhs;
		Calculator.Multiply = (lhs, rhs) => lhs * rhs;
		Calculator.Divide = (lhs, rhs) => lhs / rhs;
		TestCalculator = new Calculator();
	}

	void TestCalculationGivenExpression(string expression, int expected)
	{
		bool success = TestCalculator.TryCalculate(expression, out int actual);
		Assert.IsTrue(success);
		Assert.AreEqual<int>(expected, actual);
	}

	[TestMethod]
	public void Calculator_DefaultAdd_SuccessReturnsSumAB()
	{
		const string expression = "42 + 1337";
		const int expected = 42 + 1337;
		TestCalculationGivenExpression(expression, expected);
	}

	[TestMethod]
	public void Calculator_DefaultSubtract_SuccessReturnsDifferenceAB()
	{
		const string expression = "1337 - 42";
		const int expected = 1337 - 42;
		TestCalculationGivenExpression(expression, expected);
	}

	[TestMethod]
	public void Calculator_DefaultMultiply_SuccessReturnsProductAB()
	{
		const string expression = "42 * 2";
		const int expected = 42 * 2;
		TestCalculationGivenExpression(expression, expected);
	}
	
	[TestMethod]
	public void Calculator_DefaultDivide_SuccessReturnsQuotientAB()
	{
		const string expression = "42 / 2";
		const int expected = 42 / 2;
		TestCalculationGivenExpression(expression, expected);
	}

	[TestMethod]
	public void Calculator_DefaultCalculations_FailureReturnsFalseZero()
	{
		const string expression = @"hello, world! +-*/******=-=-+***_+*_+_*+)+!@*#*!@//?@&#@#&!@(?#?@#!?@??//@ wait, is this a calculator? 5 + 7. Did you just swipe your hands across the keyboard? Mad man. Insane. Get a grip on reality, bud. You do realize nobody would ever use a calculator like that, right? Unless if they were a toddler or something. Why would a toddler be using a calculator? Actually, that's a dumb question - of course a toddler would use a calculator, they don't know math yet. I know math but I'm writing a calculator. What's up with that, right? \/\/\/\/\\/\/\/\/\/\/\/\\//\/\/\/\/\/\/\/\/\/\/\\\\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\\/\/\/\/\/\/\/\/\/ &amp; &nbsp; &nbsp; wow an html entity? don't you think that really goes out of the bounds of this test? who'd even try that? U+00A9 wow, daring aren't we? I'm not going to add an emoji even it would make for a good test case. https://youtu.be/wHGN-fAkk5Y";
		const int expected = 0;
		bool success;

		success = TestCalculator.TryCalculate(expression, out int actual);

		Assert.IsFalse(success);
		Assert.AreEqual<int>(expected, actual);
	}

	[TestMethod]
	public void Calculator_OverrideAddOperation_SuccessReturnsTrueExpected()
	{
		Func<int, int, int> doubleAdd = int (int lhs, int rhs) => (lhs + rhs) * 2;
		Calculator.Add = doubleAdd;
		TestCalculator = new Calculator();
		const string expression = "42 + 8";
		const int expected = (42 + 8) * 2;
		
		TestCalculator.TryCalculate(expression, out int actual);

		Assert.AreEqual<int>(expected, actual);
	}

	[TestMethod]
	public void Calculator_ComplexExpression_SuccessReturnsLeftToRightEvaluation()
	{
		const string expression = "123 + 456 * 7 / 8";
		const int expected = ((123 + 456) * 7) / 8;
		bool success;

		success = TestCalculator.TryCalculate(expression, out int actual);

		Assert.IsTrue(success);
		Assert.AreEqual<int>(expected, actual);
	}

	[TestMethod]
	public void Calculator_ComplexExpressionAndOverride_SuccessReturnsLeftToRightEvaluation()
	{
		Func<int, int, int> productIsQuotient = Calculator.Divide;
		Func<int, int, int> subtractIsAdd = Calculator.Add;
		const string expression = "12 - 2 * 2";
		const int expected = (12 + 2) / 2;
		bool success;

		Calculator.Multiply = productIsQuotient;
		Calculator.Subtract = subtractIsAdd;
		TestCalculator = new Calculator();

		success = TestCalculator.TryCalculate(expression, out int actual);

		Assert.IsTrue(success);
		Assert.AreEqual<int>(expected, actual);
	}
}
