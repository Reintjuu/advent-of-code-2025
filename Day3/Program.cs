using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day3;

public class Program
{
	public static async Task Main(string[] args)
	{
		var input = (await File.ReadAllLinesAsync("../../../input"))
			.SelectMany(values => values.Split(Environment.NewLine));

		Console.WriteLine(CalculateJoltage(input, 12));
	}

	public static long CalculateJoltage(IEnumerable<string> input, int sizeOfBattery)
	{
		return input.Sum(bank =>
		{
			var digits = bank.Select(c => c - '0').ToArray();

			return DetermineHighestDigits(digits, sizeOfBattery)
				.Aggregate(0L, (acc, d) => acc * 10 + d);
		});
	}

	private static IEnumerable<int> DetermineHighestDigits(int[] digits, int sizeOfBattery)
	{
		int amountOfDigits = digits.Length;
		int start = 0;

		for (int allowedSearchWindow = sizeOfBattery; allowedSearchWindow > 0; allowedSearchWindow--)
		{
			// The last index we can choose from when still having enough digits left.
			int end = amountOfDigits - allowedSearchWindow;

			// Find the highest digit in the allowed search window.
			var (highestDigit, highestDigitIndex) = Enumerable
				.Range(start, end - start + 1)
				.Select(i => (digit: digits[i], index: i))
				.MaxBy(x => x.digit);

			yield return highestDigit;

			// Move past the chosen digit.
			start = highestDigitIndex + 1;
		}
	}
}
