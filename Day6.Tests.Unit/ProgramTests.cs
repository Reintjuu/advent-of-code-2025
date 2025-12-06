namespace Day6.Tests.Unit;

public class ProgramTests
{
	[Fact]
	public void ExampleInput()
	{
		Assert.Equal(4277556, Program.Solve(
			"""
			123 328  51 64
			 45 64  387 23
			  6 98  215 314
			*   +   *   +  
			"""));
	}
}
