namespace Calculate.Tests;

[TestClass]
public class CalculatorTests
{
	Calculator calculator;

	[TestInitialize]
	public void TestInitialize()
	{
		calculator = new Calculator();
	}
}
