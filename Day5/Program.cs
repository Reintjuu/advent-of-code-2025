using System;
using System.Collections.Generic;
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

		var merged = new List<(long Start, long End)>();
		foreach (var currentRange in ranges)
		{
			if (merged.Count == 0 || currentRange.Start > merged[^1].End + 1)
			{
				merged.Add(currentRange);
			}
			else
			{
				var last = merged[^1];
				merged[^1] = (last.Start, Math.Max(last.End, currentRange.End));
			}
		}

		var currentFreshIngredients = allLines
			.SkipWhile(s => s != listDelimiter)
			.Skip(1)
			.Select(long.Parse)
			.Count(ingredient => ranges.Any(v => ingredient >= v.Start && ingredient <= v.End));

		return (currentFreshIngredients, merged.Sum(r => r.End - r.Start + 1));
	}
}
