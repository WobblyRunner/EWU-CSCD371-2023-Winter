namespace Calculate.Tests;

[TestClass]
public class GenericCalculatorTests
{
	[TestInitialize]
	public void TestInitialize()
	{
	}

	[TestMethod]
	public void GenericCalculator_TestAddSingles_SuccessReturnsTrue()
	{
		GenericCalculator<float> calculator = new GenericCalculator<float>();
		const string expression = "1 + 3 * 4";
		const float expected = (1F + 3F) * 4F;
		bool success;

		success = calculator.TryCalculate(expression, out float actual);

		Assert.IsTrue(success);
		Assert.AreEqual<float>(expected, actual);
	}

	[TestMethod]
	public void GenericCalculator_TestAddDecimals_UnableToParseReturnsFalseAndDefault()
	{
		
		GenericCalculator<decimal> calculator = new GenericCalculator<decimal>();
		const string expression = "Hello, world! Today, I just don't feel like I have it in me to write anything too extra in here.";
		const decimal expected = default;
		bool success;

		success = calculator.TryCalculate(expression, out decimal actual);

		Assert.IsFalse(success);
		Assert.AreEqual<decimal>(expected, actual);
	}

	[TestMethod]
	public void GenericCalculator_TestSubtractLongs_SuccessReturnsTrue()
	{
		
		GenericCalculator<long> calculator = new GenericCalculator<long>();
		const string expression = "100 * 100 * 100";
		const decimal expected = 100L * 100L * 100L;
		bool success;

		success = calculator.TryCalculate(expression, out long actual);

		Assert.IsTrue(success);
		Assert.AreEqual<decimal>(expected, actual);
	}

	
	[TestMethod]
	public void GenericCalculator_TestOverrideMultiplyBytes_SuccessReturnsTrue()
	{
		static byte multiplyIsDivide(byte left, byte right) => (byte)(left / right);
		GenericCalculator<byte>.Multiply = multiplyIsDivide;
		GenericCalculator<byte> calculator = new GenericCalculator<byte>();
		const string expression = "15 * 3";
		const byte expected = 15 / 3;
		bool success;

		success = calculator.TryCalculate(expression, out byte actual);

		Assert.IsTrue(success);
		Assert.AreEqual<byte>(expected, actual);
	}
}
