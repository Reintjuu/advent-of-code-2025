using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day4;

public class Program
{
	public static async Task Main()
	{
		var input = await File.ReadAllTextAsync("../../../input");

		string[] rowContent = input.Trim().Split(Environment.NewLine);

		Console.WriteLine(AmountOfLessThanFourAdjacentPaperRollsWithRemovalLoop(rowContent));
	}


	public static int AmountOfLessThanFourAdjacentPaperRollsWithRemovalLoop(string[] rowContent)
	{
		var rows = rowContent.Length;
		var columns = rowContent[0].Length;
		var grid = new char[rows, columns];

		for (var y = 0; y < rows; y++)
		{
			for (var x = 0; x < columns; x++)
			{
				grid[y, x] = rowContent[y][x];
			}
		}

		int totalRollsOfPaperRemoved = 0;

		bool retry = true;
		while (retry)
		{
			int rollsOfPaperRemoved = 0;

			for (var y = 0; y < rows; y++)
			{
				for (var x = 0; x < columns; x++)
				{
					if (grid[y, x] != '@')
					{
						continue;
					}

					var canRemove = DoesMatchAdjacent(grid, rows, columns, y, x)
						.Count(b => b) < 4;

					if (!canRemove)
					{
						continue;
					}

					grid[y, x] = '.';
					rollsOfPaperRemoved++;
				}
			}

			if (rollsOfPaperRemoved == 0)
			{
				retry = false;
			}

			totalRollsOfPaperRemoved += rollsOfPaperRemoved;
		}

		return totalRollsOfPaperRemoved;
	}

	private static IEnumerable<bool> DoesMatchAdjacent(char[,] grid, int rows, int columns, int y, int x)
	{
		for (var yToCheck = y - 1; yToCheck <= y + 1; yToCheck++)
		{
			for (var xToCheck = x - 1; xToCheck <= x + 1; xToCheck++)
			{
				if (yToCheck == y && xToCheck == x || yToCheck < 0 || yToCheck >= rows || xToCheck < 0 || xToCheck >= columns)
				{
					continue;
				}

				if (grid[yToCheck, xToCheck] == '@')
				{
					yield return true;
				}
			}
		}
	}
}
