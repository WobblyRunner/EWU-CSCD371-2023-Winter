namespace Calculate;

public class Program
{
	public Action<string> WriteLine { get; init; } = Console.WriteLine;
	public Func<string?> ReadLine { get; init; } = Console.ReadLine;

	public static void Main(string[] args)
	{
		Calculator calculator = new();

		for(;;)
		{
			string input = Console.ReadLine() ?? throw new ArgumentNullException();
			if (input.Trim().ToLower() == "quit")
				break;
			calculator.TryCalculate(input, out decimal val);
			Console.WriteLine($"Result: {val}");
		}
	}

}