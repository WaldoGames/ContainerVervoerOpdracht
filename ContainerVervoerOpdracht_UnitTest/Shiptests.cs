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

			List<Container> containers = new List<Container>();
			for (int i = 0; i < 21; i++)
			{
				containers.Add(fullcontainer);
			}
			ship.Fillship(containers);

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

			List<Container> containers = new List<Container>();
			for (int i = 0; i < 22; i++)
			{
				containers.Add(fullcontainer);
			}
			ship.Fillship(containers);
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
			Container Halfcontainer = new Container(10000, false, false);
			ContainerStack fullStack = new ContainerStack(false, false);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			ContainerStack partialStack = new ContainerStack(false, false);
			partialStack.TryAddContainerToStack(fullcontainer);
			partialStack.TryAddContainerToStack(fullcontainer);
			Ship ship = new Ship(2, 3);

			List<Container> containers = new List<Container>();
			for (int i = 0; i < 24; i++)
			{
				containers.Add(fullcontainer);
			}
			containers.Add(Halfcontainer);
			containers.Add(fullcontainer);

			ship.Fillship(containers);

			Assert.False(ship.checkStacksRight(Halfcontainer));
		}
		[Fact]
		public void CheckLeft_Full()
		{
			Container fullcontainer = new Container(26000, false, false);
			Container Halfcontainer = new Container(10000, false, false);
			ContainerStack fullStack = new ContainerStack(false, false);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			ContainerStack partialStack = new ContainerStack(false, false);
			partialStack.TryAddContainerToStack(fullcontainer);
			partialStack.TryAddContainerToStack(fullcontainer);
			Ship ship = new Ship(2, 3);

			List<Container> containers = new List<Container>();
			for (int i = 0; i < 24; i++)
			{
				containers.Add(fullcontainer);
			}
			containers.Add(Halfcontainer);
			containers.Add(fullcontainer);

			ship.Fillship(containers);

			Assert.False(ship.checkStacksLeft(Halfcontainer));
		}
		[Fact]
		public void CheckRight_Space()
		{
			Container fullcontainer = new Container(26000, false, false);
			Container Halfcontainer = new Container(10000, false, false);
			ContainerStack fullStack = new ContainerStack(false, false);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			ContainerStack partialStack = new ContainerStack(false, false);
			partialStack.TryAddContainerToStack(fullcontainer);
			partialStack.TryAddContainerToStack(fullcontainer);
			Ship ship = new Ship(2, 3);

			List<Container> containers = new List<Container>();
			for (int i = 0; i < 23; i++)
			{
				containers.Add(fullcontainer);

			}
			ship.Fillship(containers);

			Assert.True(ship.checkStacksRight(Halfcontainer));
			Assert.False(ship.checkStacksLeft(Halfcontainer));
		}

		[Fact]
		public void CheckLeft_Space()
		{
			Container fullcontainer = new Container(26000, false, false);
			Container Halfcontainer = new Container(10000, false, false);
			ContainerStack fullStack = new ContainerStack(false, false);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			fullStack.TryAddContainerToStack(fullcontainer);
			ContainerStack partialStack = new ContainerStack(false, false);
			partialStack.TryAddContainerToStack(fullcontainer);
			partialStack.TryAddContainerToStack(fullcontainer);
			Ship ship = new Ship(2, 3);

			List<Container> containers = new List<Container>();
			for (int i = 0; i < 22; i++)
			{
				containers.Add(fullcontainer);
			}
			containers.Add(Halfcontainer);
			containers.Add(fullcontainer);

			ship.Fillship(containers);

			Assert.True(ship.checkStacksLeft(Halfcontainer));
			Assert.False(ship.checkStacksRight(Halfcontainer));
		}

		[Fact]
		public void FillShip_Perfect_Fill()
		{
			Container fullcontainer = new Container(26000, false, false);

			List<Container> containers = new List<Container>();

			for (int i = 0; i < (4*6); i++)
			{
				containers.Add(fullcontainer);
			}
			Ship ship = new Ship(2, 3);

			string fillReturn = ship.Fillship(containers);

			Assert.StartsWith("24/24 ", fillReturn);

		}
		[Fact]
		public void FillShip_6_To_Many()
		{
			Container fullcontainer = new Container(26000, false, false);

			List<Container> containers = new List<Container>();

			for (int i = 0; i < (5 * 6); i++)
			{
				containers.Add(fullcontainer);
			}
			Ship ship = new Ship(2, 3);

			string fillReturn = ship.Fillship(containers);

			Assert.StartsWith("24/30 ", fillReturn);

		}
		[Fact]
		public void FillShip_Perfect_Fill_WithValueable()
		{
			Container fullcontainer = new Container(26000, false, false);

			Container fullcontainerValuable = new Container(26000, true, false);

			List<Container> containers = new List<Container>();

			for (int i = 0; i < (3 * 6); i++)
			{
				containers.Add(fullcontainer);
			}
			for (int i = 0; i < 4; i++)
			{
				containers.Add(fullcontainerValuable);
			}
			Ship ship = new Ship(2, 3);

			string fillReturn = ship.Fillship(containers);

			Assert.StartsWith("22/22 ", fillReturn);

		}

		[Fact]
		public void FillShip_Perfect_Fill_WithValueable_toomanyValuable()
		{
			Container fullcontainer = new Container(26000, false, false);

			Container fullcontainerValuable = new Container(26000, true, false);

			List<Container> containers = new List<Container>();

			for (int i = 0; i < (3 * 6); i++)
			{
				containers.Add(fullcontainer);
			}
			for (int i = 0; i < 8; i++)
			{
				containers.Add(fullcontainerValuable);
			}
			Ship ship = new Ship(2, 3);

			string fillReturn = ship.Fillship(containers);

			Assert.StartsWith("22/26 ", fillReturn);
			Assert.Contains("valuable: 4\n", fillReturn);

		}
		[Fact]
		public void FillShip_Perfect_Fill_WithCooled()
		{
			Container fullcontainer = new Container(26000, false, false);

			Container fullcontainerCooled = new Container(26000, false, true);

			List<Container> containers = new List<Container>();

			for (int i = 0; i < 9; i++)
			{
				containers.Add(fullcontainerCooled);
			}
			Ship ship = new Ship(2, 3);

			string fillReturn = ship.Fillship(containers);

			Assert.StartsWith("8/9 ", fillReturn);
			Assert.True(ship.TryAddContainerToShip(fullcontainer));
		}
	}
}
