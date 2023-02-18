using Calculate.ExtraCredit;

namespace Calculate;

public class Program
{
	internal enum ProgramMode { NonGeneric = 0, Generic = 1 };

	public Func<string?> ReadLine { get; init; }
	public Action<string> Write { get; init; }
	public Action<string> WriteLine { get; init; }

	internal ProgramMode Mode { get; set; }

	public Program()
	{
		ReadLine = Console.ReadLine;
		Write = Console.Write;
		WriteLine = Console.WriteLine;
		Mode = ProgramMode.NonGeneric;
	}

	void WriteHelp()
	{
		WriteLine("""
		Calculator Assignment by Joshua Willis

		Usage:
		  Process complex mathematical expressions. Only supports the following operations: *, /, +, -
		  Follows left-to-right order of operations.
		  (I looked it up and PEMDAS wasn't mentioned until around the 1800s so this is a period-accurate pre-1800s calculator)

		Modes:
		  (ints)		Only processes integers and produces integer results. Fails to parse non-integers.
		  (real)		Processes integers and all real number. Produces real number results.

		Commands:
		  exit		Terminates the program
		  switch	Switches between calculator programs (integer and real)
		  help		Shows this help menu
		""");
		
	}

	static internal void Main(string[] args)
	{
		Program program = new Program();

		Calculator intCalculator = new();
		GenericCalculator<decimal> realCalculator = new();
		Func<ICalculator> getCurrentCalculator = () => program.Mode switch { ProgramMode.Generic => realCalculator, _ => intCalculator };

		string input;
		bool terminateProgram = false;

		Dictionary<string, Action> commands = new Dictionary<string, Action>();

		commands.Add("exit", () => {
			program.WriteLine("Exiting...");
			terminateProgram = true;
		});
		commands.Add("switch", () => {
			program.WriteLine("Switching calculator type...");
			program.Mode = program.Mode == ProgramMode.NonGeneric ? ProgramMode.Generic : ProgramMode.NonGeneric;
		});
		commands.Add("help", program.WriteHelp);

		program.WriteHelp();
		while (!terminateProgram)
		{
			program.Write(program.Mode == ProgramMode.NonGeneric ? "(ints) " : "(real) ");
			input = program.ReadLine()?.Trim().ToLower() ?? string.Empty;

			if (commands.ContainsKey(input))
			{
				commands[input].Invoke();
				continue;
			}
			else if (getCurrentCalculator().TryCalculate(input, out object? result))
			{
				program.WriteLine($"Result: {result}");
			}
			else
			{
				program.WriteLine($"Unable to evaulate given expression: {input}");
			}
		}
	}
}