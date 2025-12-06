using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day6;

public partial class Program
{
	public static async Task Main()
	{
		var input = await File.ReadAllLinesAsync("../../../input");

		Console.WriteLine(Solve(input));
	}

	public static long Solve(string[] splitInput)
	{
		// Chunk all operations.
		var operations = OperationChunkWithWhiteSpace()
			.Matches(splitInput.Last())
			.Select(line => line.Value)
			.ToArray();

		var numberChunksBasedOnOperationSize = splitInput
			.Take(splitInput.Length - 1)
			.Select(s => operations
				.Select((operation, index) =>
					(Start: operations.Take(index).Sum(i => i.Length), Size: operation.Length))
				.Where(x => x.Start < s.Length)
				.Select(x => s.Substring(x.Start, Math.Min(x.Size, s.Length - x.Start)))
				.ToArray())
			.Append(operations)
			.ToArray();

		var rows = numberChunksBasedOnOperationSize.Length;
		var columns = numberChunksBasedOnOperationSize[0].Length;
		var rotatedGrid = new string[columns, rows];

		for (var y = 0; y < rows; y++)
		{
			for (var x = 0; x < columns; x++)
			{
				rotatedGrid[x, y] = numberChunksBasedOnOperationSize[y][x];
			}
		}

		// return FirstPart(rotatedGrid, columns, rows);

		return SecondPart(rotatedGrid, columns, rows);
	}


	private static long FirstPart(string[,] grid, int columns, int rows)
	{
		return Enumerable
			.Range(0, columns)
			.Sum(x => AccumulateNumbers(grid, x, rows)
				.Select(s=> long.Parse(s.Trim()))
				.Aggregate(((previous, current) =>
					grid[x, rows - 1].Trim() switch
					{
						"+" => previous + current,
						"*" => previous * current,
						_ => throw new ArgumentOutOfRangeException()
					})));
	}

	private static long SecondPart(string[,] grid, int columns, int rows)
	{
		return Enumerable
			.Range(0, columns)
			.Reverse()
			.Sum(x =>
			{
				var numbers = AccumulateNumbers(grid, x, rows)
					.ToList();

				var longest = numbers.Aggregate(string.Empty,
						(max, current) => max.Length > current.Length ? max : current)
					.Length;

				var verticalNumbersOnCurrentColumn = Enumerable
					.Range(0, longest)
					.Select(digitColumnIndex => numbers
						.Select(number => number.ElementAtOrDefault(digitColumnIndex))
						.Where(d => d != 0)
						.Select(d => d.ToString())
						.Aggregate((previous, currentDigit) => $"{previous}{currentDigit}"))
					.Where(s => !string.IsNullOrWhiteSpace(s))
					.Select(long.Parse);

				return verticalNumbersOnCurrentColumn
					.Aggregate(((previous, current) =>
						grid[x, rows - 1].Trim() switch
						{
							"+" => previous + current,
							"*" => previous * current,
							_ => throw new ArgumentOutOfRangeException()
						}));
			});
	}

	private static IEnumerable<string> AccumulateNumbers(string[,] grid, int x, int rows)
	{
		for (int y = 0; y < rows - 1; y++)
		{
			yield return grid[x, y];
		}
	}

	[GeneratedRegex("([*+]\\s+)")]
	private static partial Regex OperationChunkWithWhiteSpace();
}
