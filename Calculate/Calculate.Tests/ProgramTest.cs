namespace Calculate.Tests;

[TestClass]
public class ProgramTest
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	Program Program;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

	const string ReadLineMessage = "Hello, world!";

	public Func<string> MockReadLine = () => ReadLineMessage;

	const string WriteLineMessage = "Inigo Montoya";
	static string ActualWriteLineMessage = string.Empty;

	public Action<string> MockWriteLine = (str) => ActualWriteLineMessage = WriteLineMessage;

	[TestInitialize]
	public void TestInitialize()
	{
		Program = new Program()
		{
			ReadLine = MockReadLine,
			WriteLine = MockWriteLine
		};
	}

	[TestMethod]
	public void Program_ReadLine_SuccessReturnsReadLineMessage()
	{
		string? actual = Program.ReadLine(), expected = ReadLineMessage;

		Assert.AreEqual<string?>(expected, actual);
	}

	[TestMethod]
	public void Program_WriteLine_SuccessSetsWriteLineMessage()
	{
		string expected = WriteLineMessage, actual;

		Program.WriteLine(WriteLineMessage);
		actual = ActualWriteLineMessage;

		Assert.AreEqual<string>(expected, actual);
	}

}
