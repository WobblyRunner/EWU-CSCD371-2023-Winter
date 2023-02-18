namespace Calculate;

public class Program
{
	public Func<string?> ReadLine { get; init; }
	public Action<string> WriteLine { get; init; }

	public Program()
	{
		ReadLine = Console.ReadLine;
		WriteLine = Console.WriteLine;
	}

	static void Main(string[] args)
	{
		Program program = new Program();

		Calculator calculator = new();
		string input;
		do
		{
			input = program.ReadLine.Invoke() ?? string.Empty;
			bool success = calculator.TryCalculate(input, out decimal result);
			program.WriteLine(success ? $"Result: {result}" : $"Bad input: {input}");
		} while (input.ToLower() != "exit");
	}
}