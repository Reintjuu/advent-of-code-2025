namespace Day5.Tests.Unit;

public class ProgramTests
{
	[Fact]
	public void ExampleInput()
	{
		Assert.Equal((3, 14),
			Program.Solve([
				"3-5",
				"10-14",
				"16-20",
				"12-18",
				"12-18",
				"",
				"1",
				"5",
				"8",
				"11",
				"17",
				"32",
			]));
	}

	[Fact]
	public void MultipleOverlappingRanges()
	{
		Assert.Equal((4, 13),
			Program.Solve([
				"1-3",
				"2-4",
				"3-4",
				"4-6",
				"2-8",
				"10-14",
				"",
				"1",
				"5",
				"8",
				"11",
				"17",
				"32",
			]));
	}
}
