namespace Day3.Tests.Unit;

public class ProgramTests
{
	[Fact]
	public void PartOne()
	{
		Assert.Equal(357,
			Program.CalculateJoltage([
				"987654321111111",
				"811111111111119",
				"234234234234278",
				"818181911112111"
			], 2));
	}

	[Fact]
	public void PartTwo()
	{
		Assert.Equal(3121910778619,
			Program.CalculateJoltage([
				"987654321111111",
				"811111111111119",
				"234234234234278",
				"818181911112111"
			], 12));
	}

	[Fact]
	public void PartTwoThreeParts()
	{
		Assert.Equal(889,
			Program.CalculateJoltage([
				"818181911112111",
			], 12));
	}
}
