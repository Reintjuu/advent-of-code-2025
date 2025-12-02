using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day2;

public class Program
{
	public static async Task Main(string[] args)
	{
		var input = (await File.ReadAllLinesAsync("../../../input"));

		Console.WriteLine(FindInvalidIds(input[0]));
	}

	public static long FindInvalidIds(string input)
	{
		return input
			.Split(',')
			.Select(i => i.Split('-'))
			.SelectMany(i => Enumerable.Sequence(long.Parse(i.First()), long.Parse(i.Last()), 1))
			.Sum(rangeInput =>
			{
				var id = rangeInput.ToString();
				for (int currentLengthToCheck = 1; currentLengthToCheck < id.Length; currentLengthToCheck++)
				{
					if (id.Length % currentLengthToCheck != 0)
					{
						continue;
					}

					if (id.Chunk(currentLengthToCheck).AllElementsEqual())
					{
						return rangeInput;
					}
				}

				return 0;
			});
	}
}

public static class IEnumerableExtensions
{
	public static bool AllElementsEqual<T>(this IEnumerable<T> items)
	{
		return items.Distinct().Take(2).Count() == 1;
	}
}

public static class StringExtensions
{
	public static IEnumerable<string> Chunk(this string input, int chunkSize)
	{
		return Enumerable
			.Range(0, input.Length / chunkSize)
			.Select(i => input.Substring(i * chunkSize, chunkSize));
	}
}
