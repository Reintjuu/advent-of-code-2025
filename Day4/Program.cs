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

		string[] rowContent = input.Trim().Split('\n');

		// Assignment 1.
		Console.WriteLine(AmountOfLessThanFourAdjacentPaperRolls(rowContent).Count(isMatch => isMatch));
	}


	public static IEnumerable<bool> AmountOfLessThanFourAdjacentPaperRolls(string[] rowContent)
	{
		var rows = rowContent.Length;
		var columns = rowContent[0].Length - 1;
		Func<int, int, bool> isMatch = (yToCheck, xToCheck) => rowContent[yToCheck][xToCheck] == '@';

		for (var y = 0; y < rows; y++)
		{
			for (var x = 0; x < columns; x++)
			{
				if (rowContent[y][x] != '@')
				{
					continue;
				}

				yield return DoesMatchAdjacent(rows, columns, y, x, isMatch).Count(b => b) < 4;
			}
		}
	}

	private static IEnumerable<bool> DoesMatchAdjacent(int rows, int columns, int y, int x, Func<int, int, bool> noMatch)
	{
		// horizontal
		yield return IsMatch(rows, columns, y, x, (yy, offset) => yy + offset, (xx, _) => xx, noMatch);
		yield return IsMatch(rows, columns, y, x, (yy, offset) => yy - offset, (xx, _) => xx, noMatch);

		// vertical
		yield return IsMatch(rows, columns, y, x, (yy, _) => yy, (xx, offset) => xx + offset, noMatch);
		yield return IsMatch(rows, columns, y, x, (yy, _) => yy, (xx, offset) => xx - offset, noMatch);

		// diagonal
		yield return IsMatch(rows, columns, y, x, (yy, o) => yy + o, (xx, o) => xx + o, noMatch);
		yield return IsMatch(rows, columns, y, x, (yy, o) => yy + o, (xx, o) => xx - o, noMatch);
		yield return IsMatch(rows, columns, y, x, (yy, o) => yy - o, (xx, o) => xx - o, noMatch);
		yield return IsMatch(rows, columns, y, x, (yy, o) => yy - o, (xx, o) => xx + o, noMatch);
	}

	private static bool IsMatch(
		int rows, int columns,
		int y, int x,
		Func<int, int, int> yOperation,
		Func<int, int, int> xOperation,
		Func<int, int, bool> noMatch)
	{
		var yToCheck = yOperation(y, 1);
		var xToCheck = xOperation(x, 1);

		// Out of bounds.
		if (yToCheck < 0 || yToCheck >= rows || xToCheck < 0 || xToCheck >= columns)
		{
			return false;
		}

		return noMatch(yToCheck, xToCheck);
	}
}
