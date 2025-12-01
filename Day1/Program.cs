string path = Path.Combine(AppContext.BaseDirectory, "input.txt");

string[] input = File.ReadAllLines(path);

int value = 50;
int zeroes = 0;
int clicks = 0;

foreach (var line in input)
{
	char dir = line[0];
	int step = int.Parse(line.AsSpan(1));
	Direction direction = GetDirection(dir);

	int first = direction == Direction.Right
		? (value == 0 ? 100 : 100 - value)
		: (value == 0 ? 100 : value);

	if (step > first)
	{
		clicks += ((step - 1 - first) / 100) + 1;
	}

	int delta = direction == Direction.Right
		? step
		: -step;
	int newPos = (value + delta) % 100;

	if (newPos < 0)
	{
		newPos += 100;
	}

	if (newPos == 0)
	{
		zeroes++;
	}

	value = newPos;
}

int password = zeroes + clicks;
Console.WriteLine(password);

Direction GetDirection(char input)
{
	if(input == 'R')
	{
		return Direction.Right;
	}

	return Direction.Left;
}

enum Direction
{
	Right,
	Left
}