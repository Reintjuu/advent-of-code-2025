using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day5;

public class Program
{
	public static async Task Main()
	{
		var allLines = (await File.ReadAllLinesAsync("../../../input"));

		Console.WriteLine(Solve(allLines));
	}

	public static (int CurrentFreshIngredients, long TotalFreshIngredients) Solve(string[] allLines)
	{
		var listDelimiter = string.Empty;

		var ranges = allLines
			.TakeWhile(s => s != listDelimiter)
			.Select(i => i.Split('-'))
			.Select((longs => (Start: long.Parse(longs.First()), End: long.Parse(longs.Last()))))
			.OrderBy(r => r.Start)
			.ToList();

		var total = ranges
			.Aggregate((Total: 0L, PreviousEnd: long.MinValue),
				(accumulator, currentRange) =>
				{
					if (currentRange.End <= accumulator.PreviousEnd)
					{
						return accumulator;
					}

					var start = Math.Max(accumulator.PreviousEnd + 1, currentRange.Start);
					var length = currentRange.End - start + 1;

					return (accumulator.Total + length, currentRange.End);
				})
			.Total;

		var currentFreshIngredients = allLines
			.SkipWhile(s => s != listDelimiter)
			.Skip(1)
			.Select(long.Parse)
			.Count(ingredient => ranges.Any(v => ingredient >= v.Start && ingredient <= v.End));

		return (currentFreshIngredients, total);
	}
}
