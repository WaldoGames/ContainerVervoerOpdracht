using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContainerVervoerOpdracht_Core.Models;
using Container = ContainerVervoerOpdracht_Core.Models.Container;

namespace ContainerVervoerOpdracht_UnitTest
{
	public class Shiptests
	{
		[Fact]
		public void TryAddContainerToShip_3_new_containers_allsuccesFull()
		{
			Container fullcontainer = new Container(26000,false,false);
			
			ContainerStack fullStack = new ContainerStack(false, false);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			ContainerStack partialStack = new ContainerStack(false, false);
			partialStack.TryAddContainerToStack(fullcontainer);
			partialStack.TryAddContainerToStack(fullcontainer);
			partialStack.TryAddContainerToStack(fullcontainer);
			Ship ship = new Ship(2, 3);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 0, 0);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(partialStack.stack, 1, 0);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 0, 1);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(partialStack.stack, 1, 1);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(partialStack.stack, 0, 2);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 1, 2);

			Assert.True(ship.TryAddContainerToShip(fullcontainer));
			Assert.True(ship.TryAddContainerToShip(fullcontainer));
			Assert.True(ship.TryAddContainerToShip(fullcontainer));

		}
		[Fact]
		public void TryAddContainerToShip_5_new_containers_3_failed()
		{
			Container fullcontainer = new Container(26000, false, false);

			ContainerStack fullStack = new ContainerStack(false, false);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			ContainerStack partialStack = new ContainerStack(false, false);
			partialStack.TryAddContainerToStack(fullcontainer);
			partialStack.TryAddContainerToStack(fullcontainer);
			Ship ship = new Ship(2, 3);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 0, 0);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 1, 0);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 0, 1);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 1, 1);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 0, 2);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(partialStack.stack, 1, 2);

			Assert.True(ship.TryAddContainerToShip(fullcontainer));
			Assert.True(ship.TryAddContainerToShip(fullcontainer));
			Assert.False(ship.TryAddContainerToShip(fullcontainer));
			Assert.False(ship.TryAddContainerToShip(fullcontainer));
			Assert.False(ship.TryAddContainerToShip(fullcontainer));

		}
		[Fact]
		public void CheckRight_Full()
		{
			Container fullcontainer = new Container(26000, false, false);

			ContainerStack fullStack = new ContainerStack(false, false);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			ContainerStack partialStack = new ContainerStack(false, false);
			partialStack.TryAddContainerToStack(fullcontainer);
			partialStack.TryAddContainerToStack(fullcontainer);
			Ship ship = new Ship(2, 3);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(partialStack.stack, 0, 0);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 1, 0);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 0, 1);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 1, 1);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 0, 2);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 1, 2);

			Assert.False(ship.checkStacksRight(fullcontainer));
		}
		[Fact]
		public void CheckLeft_Full()
		{
			Container fullcontainer = new Container(26000, false, false);

			ContainerStack fullStack = new ContainerStack(false, false);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			ContainerStack partialStack = new ContainerStack(false, false);
			partialStack.TryAddContainerToStack(fullcontainer);
			partialStack.TryAddContainerToStack(fullcontainer);
			Ship ship = new Ship(2, 3);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 0, 0);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 1, 0);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 0, 1);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 1, 1);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 0, 2);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(partialStack.stack, 1, 2);

			Assert.False(ship.checkStacksLeft(fullcontainer));
		}
		[Fact]
		public void CheckRight_Space()
		{
			Container fullcontainer = new Container(26000, false, false);

			ContainerStack fullStack = new ContainerStack(false, false);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			ContainerStack partialStack = new ContainerStack(false, false);
			partialStack.TryAddContainerToStack(fullcontainer);
			partialStack.TryAddContainerToStack(fullcontainer);
			Ship ship = new Ship(2, 3);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 0, 0);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 1, 0);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 0, 1);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 1, 1);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 0, 2);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(partialStack.stack, 1, 2);

			Assert.True(ship.checkStacksRight(fullcontainer));
		}
		[Fact]
		public void CheckLeft_Space()
		{
			Container fullcontainer = new Container(26000, false, false);

			ContainerStack fullStack = new ContainerStack(false, false);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			ContainerStack partialStack = new ContainerStack(false, false);
			partialStack.TryAddContainerToStack(fullcontainer);
			partialStack.TryAddContainerToStack(fullcontainer);
			Ship ship = new Ship(2, 3);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(partialStack.stack, 0, 0);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 1, 0);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 0, 1);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 1, 1);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 0, 2);
			ship.TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(fullStack.stack, 1, 2);

			Assert.True(ship.checkStacksLeft(fullcontainer));
		}
	}
}
