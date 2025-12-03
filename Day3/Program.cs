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
		return input
			.Sum(bank =>
			{
				var digits = bank
					.Select(d => int.Parse(d.ToString()))
					.ToList();

				var largestPair = GetLargestPairForSecondPart(digits);

				return largestPair;
			});
	}

	public static long GetLargestPairForSecondPart(List<int> digits)
	{
		long largestPair = 0;

		for (int i = digits.Count - 1; i >= 0; i--)
		{
			for (int j = i - 1; j >= 0; j--)
			{
				for (int k = j - 1; k >= 0; k--)
				{
					for (int l = k - 1; l >= 0; l--)
					{
						for (int m = l - 1; m >= 0; m--)
						{
							for (int n = m - 1; n >= 0; n--)
							{
								for (int o = n - 1; o >= 0; o--)
								{
									for (int p = o - 1; p >= 0; p--)
									{
										for (int q = p - 1; q >= 0; q--)
										{
											for (int r = q - 1; r >= 0; r--)
											{
												for (int s = r - 1; s >= 0; s--)
												{
													for (int t = s - 1; t >= 0; t--)
													{
														var pair = long.Parse(
															$"{digits[t]}{digits[s]}{digits[r]}{digits[q]}{digits[p]}{digits[o]}{digits[n]}{digits[m]}{digits[l]}{digits[k]}{digits[j]}{digits[i]}");

														if (pair > largestPair)
														{
															largestPair = pair;
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		return largestPair;
	}

	private static int GetLargestPairForFirstPart(List<int> digits)
	{
		var largestPair = 0;
		for (int i = 0; i < digits.Count; i++)
		{
			for (int j = i + 1; j < digits.Count; j++)
			{
				var pair = int.Parse($"{digits[i]}{digits[j]}");

				if (pair > largestPair)
				{
					largestPair = pair;
				}
			}
		}

		return largestPair;
	}
}
