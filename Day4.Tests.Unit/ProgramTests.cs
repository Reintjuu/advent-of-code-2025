using System.Linq;

namespace Day4.Tests.Unit;

public class ProgramTests
{
	[Fact]
	public void PartOne()
	{
		Assert.Equal(13,
			Program.AmountOfLessThanFourAdjacentPaperRolls([
				"..@@.@@@@.",
				"@@@.@.@.@@",
				"@@@@@.@.@@",
				"@.@@@@..@.",
				"@@.@@@@.@@",
				".@@@@@@@.@",
				".@.@.@.@@@",
				"@.@@@.@@@@",
				".@@@@@@@@.",
				"@.@.@@@.@."
			])
			.Count(x => x));
	}
}
