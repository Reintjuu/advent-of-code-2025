using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day1;

public class Program
{
	public static async Task Main(string[] args)
	{
		var input = (await File.ReadAllLinesAsync("../../../input"))
			.SelectMany(values => values.Split(Environment.NewLine));

		Console.WriteLine(DeterminePasswordOnDial(input));
	}

	public static int DeterminePasswordOnDial(IEnumerable<string> input)
	{
		var startsAt = 50;

		var inputs = input.Select(current => (Direction: current[0] == 'L' ? -1 : 1, Number: (int.Parse(current[1..]))));
		var password = 0;

		foreach ((int Direction, int Number) tuple in inputs)
		{
			for (int i = 0; i < tuple.Number; i++)
			{
				startsAt += tuple.Direction;

				if (startsAt < 0)
				{
					startsAt = 99;
				}

				if (startsAt > 99)
				{
					startsAt = 0;
				}

				// Second half of puzzle:
				if (startsAt == 0)
				{
					password++;
				}
			}

			// First half of puzzle:
			// if (startsAt == 0)
			// {
			// 	password++;
			// }
		}

		return password;
	}
}
