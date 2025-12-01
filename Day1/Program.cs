using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day1;

public class Program
{
	public static async Task Main(string[] args)
	{
		var input = (await File.ReadAllLinesAsync("../../../input"))
			.Select(values => values.Split(Environment.NewLine));

	}
}
