namespace Day1.Tests.Unit;

public class ProgramTests
{
	[Fact]
	public void Test()
	{
		Assert.Equal(6, Program.DeterminePasswordOnDial([
			"L68",
			"L30",
			"R48",
			"L5",
			"R60",
			"L55",
			"L1",
			"L99",
			"R14",
			"L82"
		]));
	}
}
