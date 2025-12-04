string path = Path.Combine(AppContext.BaseDirectory, "input.txt");

char[][] banks = File.ReadAllLines(path)
	.Select(line => line.ToCharArray())
	.ToArray();

const int maxRolls = 3;
bool found;

int sum1 = PartOne();
int sum2 = PartTwo();

Console.WriteLine(sum1);
Console.WriteLine(sum2);

int PartOne()
{
	int count = 0;
	for (int y = 0; y < banks.Length; y++)
	{
		for (int x = 0; x < banks[y].Length; x++)
		{
			if (banks[y][x] == '.')
			{
				continue;
			}

			var neighbors = GetNeighbors(y, x);

			if (neighbors.Count <= maxRolls)
			{
				count++;
			}
		}
	}
	return count;
}

int PartTwo()
{
	int count = 0;
	do
	{
		found = false;

		for (int y = 0; y < banks.Length; y++)
		{
			for (int x = 0; x < banks[y].Length; x++)
			{
				if (banks[y][x] == '.')
				{
					continue;
				}

				var neighbors = GetNeighbors(y, x);

				if (neighbors.Count <= maxRolls)
				{
					count++;
					banks[y][x] = '.';
					found = true;
				}
			}
		}

	} while (found);

	return count;
}

List<char> GetNeighbors(int yPos, int xPos)
{
	List<char> neighbors = new List<char>();

	for (int y = -1; y < 2; y++)
	{
		for (int x = -1; x < 2; x++)
		{
			if(x == 0 && y == 0)
				continue;

			var nY = yPos + y;
			var nX = xPos + x;

			if(nY < 0 || nY >= banks.Length || nX < 0 || nX >= banks[nY].Length)
				continue;


			if(banks[nY][nX] == '.')
			{
				continue;
			}

			neighbors.Add(banks[nY][nX]);
		}
	}

	return neighbors;
}