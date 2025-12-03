string path = Path.Combine(AppContext.BaseDirectory, "input.txt");

string[] banks = File.ReadAllLines(path);


long sum1 = 0;
long sum2 = 0;

foreach (string bank in banks)
{
	byte[] digits = new byte[bank.Length];
	for (int i = 0; i < bank.Length; i++)
	{
		digits[i] = (byte)(bank[i] - '0');
	}

	// Part 1
	int first = -1;
	int second = -1;
	int firstIndex = -1;

	for (int i = 0; i < digits.Length; i++)
	{
		if (digits[i] > first)
		{
			first = digits[i];
			firstIndex = i;
		}
	}

	if (firstIndex >= 0 && firstIndex < digits.Length - 1)
	{
		for (int i = firstIndex + 1; i < digits.Length; i++)
		{
			if (digits[i] > second)
			{
				second = digits[i];
			}
		}
	}

	long pairValue = first * 10L + second;
	sum1 += pairValue;

	const int digitsToPick = 12;
	// Part 2
	if (digits.Length >= digitsToPick)
	{
		int start = 0;
		long value = 0;

		for (int pos = 0; pos < digitsToPick; pos++)
		{
			int remaining = digitsToPick - pos;
			int end = digits.Length - remaining;
			int best = -1;
			int bestIndex = start;

			for (int i = start; i <= end; i++)
			{
				if (digits[i] > best)
				{
					best = digits[i];
					bestIndex = i;

					if (best == 9)
					{
						break;
					}
				}
			}

			value = value * 10 + best;
			start = bestIndex + 1;
		}

		sum2 += value;
	}
}

Console.WriteLine(sum1);
Console.WriteLine(sum2);