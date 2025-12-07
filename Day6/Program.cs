using System.Text.RegularExpressions;

string path = Path.Combine(AppContext.BaseDirectory, "input.txt");

string[] rows = File.ReadAllLines(path);
int operationIndex = Array.FindIndex(rows, row => Regex.IsMatch(row, @"^[*+]"));

List<Problem> PartOne(string[] lines)
{
	var rowData = lines.Select(r => r.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToList();

	List<Problem> problems = new List<Problem>();

	int columnCount = rowData[0].Length;
	for (int i = 0; i < columnCount; i++)
	{
		var newProblem = new Problem();
		for (int j = 0; j < rows.Length; j++)
		{
			if (j < operationIndex)
			{
				newProblem.Numbers.Add(int.Parse(rowData[j][i]));
			}
			else
			{
				Operation op = rowData[j][i] == "+"
					? Operation.Add
					: Operation.Multiply;
				newProblem.Operation = op;
			}
		}

		problems.Add(newProblem);
	}

	return problems;
}

var sum1 = PartOne(rows).Sum(ApplyOperation);
var sum2 = PartTwo(rows).Sum(ApplyOperation);

Console.WriteLine(sum1);
Console.WriteLine(sum2);

List<Problem> PartTwo(string[] lines)
{
	var operatorsLine = lines[^1];
	char[] operators = [..operatorsLine.Where(c => c is '+' or '*')];

	List<Problem> problems = new List<Problem>(operatorsLine.Length);
	for (int i = 0; i < operators.Length; i++)
	{
		problems.Add(new Problem());
	}

	var problemIndex = 0;
	for (int i = 0; i < operatorsLine.Length; i++)
	{
		var nextOperatorIndex = operatorsLine.IndexOfAny(['+', '*'], i + 1);

		problems[problemIndex].Operation = operatorsLine[i] == '+'
			? Operation.Add
			: Operation.Multiply;

		if (nextOperatorIndex == -1)
		{
			nextOperatorIndex = operatorsLine.Length + 1;
		}

		int numberOfDigits = nextOperatorIndex - i - 1;

		for (int numberIndex = 0; numberIndex < numberOfDigits; numberIndex++)
		{
			var number = 0;
			for (var lineIndex = 0; lineIndex < lines.Length - 1; lineIndex++)
			{
				var c = lines[lineIndex][i + numberIndex];
				if (c != ' ')
				{
					number = number * 10 + (c - '0');
				}
			}

			problems[problemIndex].Numbers.Add(number);
		}

		i = nextOperatorIndex - 1;
		problemIndex++;
	}

	return problems;
}

long ApplyOperation(Problem problem)
{
	return problem.Operation == Operation.Add
		? problem.Numbers.Sum()
		: problem.Numbers.Aggregate((a, b) => a * b);
}

public class Problem()
{
	public List<long> Numbers = new();
	public Operation Operation;
}

public enum Operation
{
	Add,
	Multiply,
}