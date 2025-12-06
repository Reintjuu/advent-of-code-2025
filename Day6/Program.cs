using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day6;

public class Program
{
	public static async Task Main()
	{
		var input = await File.ReadAllTextAsync("../../../input");

		Console.WriteLine(Solve(input));
	}

	public static long Solve(string input)
	{
		var splitInput = input
			.Split(Environment.NewLine);

		var operations = Regex.Matches(splitInput.Last(), "([*+]\\s+)")
			.Select(line => line.Value)
			.ToArray();

		var rowContent = splitInput
			.Take(splitInput.Length - 1)
			.Select(s => operations.Select((size, index) => new
				{
					Start = operations.Take(index).Sum(i => i.Length),
					Size = size.Length
				})
				.Where(x => x.Start < s.Length)
				.Select(x => s.Substring(x.Start, Math.Min(x.Size, s.Length - x.Start)))
				.ToArray())
			.ToArray();

		//.Select(i => input.Substring(i * chunkSize, chunkSize));

		var rows = rowContent.Length;
		var columns = rowContent[0].Length;
		var grid = new string[columns, rows];

		for (var y = 0; y < rows; y++)
		{
			for (var x = 0; x < columns; x++)
			{
				grid[x, y] = rowContent[y][x];
			}
		}

		// return FirstPart(grid, columns, rows);

		return Enumerable
			.Range(0, columns)
			.Reverse()
			.Sum(x =>
			{
				var numbers = AccumulateNumbers(grid, x, rows)
					.ToList();

				var longest = numbers.Aggregate(string.Empty, (max, current) => max.Length > current.Length ? max : current)
					.Length;

				var verticalNumbersOnCurrentColumn = Enumerable.Range(0, longest - 1)
					.Reverse()
					.Select(digitColumnIndex => numbers
						.Select(number => number.ElementAtOrDefault(^digitColumnIndex))
						.Where(d => d != 0)
						.Select(d => d.ToString().Trim())
						// .Aggregate((previous, currentDigit) => $"{previous}{currentDigit}"))
						// .Select(long.Parse)
						.ToList());
				return 0;
				//
				// return verticalNumbersOnCurrentColumn
				// 	.Aggregate(((previous, current) =>
				// 		(grid[x, rows - 1]) switch
				// 		{
				// 			"+" => previous + current,
				// 			"*" => previous * current,
				// 			_ => throw new ArgumentOutOfRangeException()
				// 		}));
			});
	}

	private static long FirstPart(string[,] grid, int columns, int rows)
	{
		return Enumerable
			.Range(0, columns)
			.Sum(x => AccumulateNumbers(grid, x, rows)
				.Select(long.Parse)
				.Aggregate(((previous, current) =>
					(grid[x, rows - 1]) switch
					{
						"+" => previous + current,
						"*" => previous * current,
						_ => throw new ArgumentOutOfRangeException()
					})));
	}

	private static IEnumerable<string> AccumulateNumbers(string[,] grid, int x, int rows)
	{
		for (int y = 0; y < rows - 1; y++)
		{
			yield return grid[x, y];
		}
	}
}

public static class IEnumerableExtensions
{
	public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source) where T : class
	{
		foreach (var item in source)
		{
			if (item is not null)
			{
				yield return item;
			}
		}
	}

	public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source) where T : struct
	{
		foreach (var item in source)
		{
			if (item is { } notNull)
			{
				yield return notNull;
			}
		}
	}
}
