string path = Path.Combine(AppContext.BaseDirectory, "input.txt");

char[][] input = File.ReadAllLines(path)
	.Select(line => line.ToCharArray())
	.ToArray();

int rows = input.Length;
int cols = input[0].Length;

var startIndex = input[0].ToList().FindIndex(c => c == 'S');
input[1][startIndex] = '|';

long[] paths = new long[cols];
paths[startIndex] = 1;

int splitCount = 0;
for (int i = 1; i < rows; i++)
{
	for (int j = 0; j < cols; j++)
	{
		var bottomIndex = i + 1;
		if(input[i][j] == '^')
		{
			var ways = paths[j];
			if(j -1 >= 0)
			{
				paths[j - 1] += ways;
			}
			if(j +1 < cols)
			{
				paths[j + 1] += ways;
			}
			paths[j] = 0;
		}
		if(bottomIndex < rows && input[i][j] == '|' )
		{
			if (input[bottomIndex][j] == '.')
			{
				input[bottomIndex][j] = '|';
				continue;
			}

			int leftIndex = j - 1;
			int rightIndex = j + 1;
			bool isSplit = false;

			if (leftIndex >= 0 && input[i][leftIndex] == '.')
			{
				input[bottomIndex][leftIndex] = '|';
				isSplit = true;
			}

			if (rightIndex < input[i].Length && input[i][rightIndex] == '.')
			{
				input[bottomIndex][rightIndex] = '|';
				isSplit = true;
			}

			if (isSplit)
			{
				splitCount++;
			}
		}
	}
}

long totalPaths = paths.Sum();
Console.WriteLine(splitCount);
Console.WriteLine(totalPaths);