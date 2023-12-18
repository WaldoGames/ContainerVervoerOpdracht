using ContainerVervoerOpdracht_Core.Models;
using System.Runtime.CompilerServices;

namespace ContainerVervoerOpdracht_UnitTest
{
	public class ContainerStackTests
	{
		[Fact]
		public void TryAddContainerAdd3Normals()
		{
			ContainerStack stack = new ContainerStack(false,false);

			Assert.True(stack.TryAddContainerToStack(new Container(2000, false, false)));
			Assert.True(stack.TryAddContainerToStack(new Container(4000, false, false)));
			Assert.True(stack.TryAddContainerToStack(new Container(5000, false, false)));
		}
		[Fact]
		public void TryAddContainerAdd4Normals1TooHeavy()
		{
			ContainerStack stack = new ContainerStack(false, false);

			Assert.True(stack.TryAddContainerToStack(new Container(26000, false, false)));
			Assert.True(stack.TryAddContainerToStack(new Container(26000, false, false)));
			Assert.True(stack.TryAddContainerToStack(new Container(26000, false, false)));
			Assert.True(stack.TryAddContainerToStack(new Container(26000, false, false)));
			Assert.False(stack.TryAddContainerToStack(new Container(1, false, false)));
		}
		[Fact]
		public void TryAddContainerAdd3Cooled1ValuableAndCooled()
		{
			ContainerStack stack = new ContainerStack(true, true);

			Assert.True(stack.TryAddContainerToStack(new Container(2000, true, true)));
			Assert.True(stack.TryAddContainerToStack(new Container(4000, false, true)));
			Assert.True(stack.TryAddContainerToStack(new Container(5000, false, true)));

			Assert.True(stack.stack[stack.stack.Count() - 1].Valuable == true);
			Assert.True(stack.stack[0].Valuable == false);
		}
	}
}