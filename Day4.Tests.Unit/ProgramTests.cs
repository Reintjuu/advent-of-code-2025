namespace Day4.Tests.Unit;

public class ProgramTests
{
	// CBA to make this backwards compatible for part one.
	[Fact]
	public void PartTwo()
	{
		Assert.Equal(43,
			Program.AmountOfLessThanFourAdjacentPaperRollsWithRemovalLoop([
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
			]));
	}
}
