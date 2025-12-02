using System.Text;

string path = Path.Combine(AppContext.BaseDirectory, "input.txt");

string input = File.ReadAllText(path);

List<(long, long)> ids = input.Split(',').Select(s =>
{
	var parts = s.Split('-');
	return (long.Parse(parts[0]), long.Parse(parts[1]));
}).ToList();

long sum1 = 0;
long sum2 = 0;

foreach (var pair in ids)
{
	for (long i = pair.Item1; i <= pair.Item2; i++)
	{
		if (IsDoubleNumber(i))
		{
			sum1 += i;
		}

		if (IsRepeatingPattern(i))
		{
			sum2 += i;
		}
	}
}

Console.WriteLine(sum1);
Console.WriteLine(sum2);

bool IsRepeatingPattern(long number)
{
	string s = number.ToString();
	int length = s.Length;

	for (int patternLength = 1; patternLength <= length / 2; patternLength++)
	{
		if (length % patternLength != 0)
		{
			continue;
		}

		string pattern = s.Substring(0, patternLength);
		int repeats = length / patternLength;

		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < repeats; i++)
		{
			sb.Append(pattern);
		}

		if (sb.ToString() == s)
		{
			return true;
		}
	}

	return false;
}

bool IsDoubleNumber(long number)
{
	string s = number.ToString();
	if (s.Length % 2 != 0)
	{
		return false;
	}

	int half = s.Length / 2;
	string left = s.Substring(0, half);
	string right = s.Substring(half, half);

	return left == right;
}