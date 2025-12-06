string path = Path.Combine(AppContext.BaseDirectory, "input.txt");

string[] input = File.ReadAllLines(path);
int emptyIndex = Array.FindIndex(input, string.IsNullOrWhiteSpace);

List<(long Start, long End)> ranges = input.Take(emptyIndex).Select(line =>
{
	var parts = line.Split('-');
	return (long.Parse(parts[0]), long.Parse(parts[1]));
}).ToList();


List<long> products = input.Skip(emptyIndex + 1).Select(long.Parse).ToList();

int sum1 = 0;
foreach (var product in products)
{
	bool isFresh = ranges.Any(range => product >= range.Item1 && product <= range.Item2);
	if (isFresh)
	{
		sum1++;
	}
}

ranges.Sort((a, b) => a.Item1.CompareTo(b.Item1));
var mergedRanges = new List<(long Start, long End)>(ranges.Count);
foreach (var range in ranges)
{
	if (mergedRanges.Count == 0 || range.Item1 > mergedRanges[^1].End + 1)
	{
		mergedRanges.Add(range);
	}
	else
	{
		mergedRanges[^1] = (mergedRanges[^1].Start, Math.Max(mergedRanges[^1].End, range.End));
	}
}

long freshCount = 0;
foreach (var r in mergedRanges)
{
	freshCount += r.End - r.Start + 1;
}

Console.WriteLine(sum1);
Console.WriteLine(freshCount);