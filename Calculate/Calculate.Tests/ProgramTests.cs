namespace Calculate.Tests;

[TestClass]
public class ProgramTests
{
	[TestMethod]
	public void Program_ReadLine_ReturnsExpectedValue()
	{
		const string expected = "Inigo Montoya";
		string? actual;
		Program program = new()
		{
			ReadLine = () => expected,
		};

		actual = program.ReadLine();

		Assert.IsNotNull(actual);
		Assert.AreEqual<string?>(expected, actual);
	}

	[TestMethod]
	public void Program_WriteLine_WritesExpectedValue()
	{
		const string expected = "Hello, world!";
		string? actual = null;
		Program program = new()
		{
			WriteLine = (str) => actual = str,
		};

		program.WriteLine(expected);

		Assert.IsNotNull(actual);
		Assert.AreEqual<string?>(expected, actual);
	}
}
