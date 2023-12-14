using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ContainerVervoerOpdracht_Core.Models
{
	public class Ship
	{
		Dictionary<(int, int), ContainerStack> ShipStacks;
		int width;
		int length;

		List<ContainerStack> LeftStacks;
		List<ContainerStack> RightStacks;

        public Ship(int width, int length)
        {
			this.width = width;
			this.length = length;

            ShipStacks = new Dictionary<(int, int), ContainerStack>();
			LeftStacks = new List<ContainerStack>();
			RightStacks = new List<ContainerStack>();

			bool hasCenter = HasCenterLine(out int location);

			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < length; j++)
				{
					bool valuablesAllowed = (length % 3 == 2);
					bool coolable = (j == 0);
					ContainerStack newStack;
					if (hasCenter && location == width)
					{
						newStack = new ContainerStack(valuablesAllowed, coolable, true);
					}
					else
					{
						newStack = new ContainerStack(valuablesAllowed, coolable);
					}
					if (hasCenter)
					{
						if (i <= location)
						{
							LeftStacks.Add(newStack);
						}
						else
						{
							RightStacks.Add(newStack);
						}
					}
					else
					{
						if (i < location)
						{
							LeftStacks.Add(newStack);
						}
						else
						{
							RightStacks.Add(newStack);
						}
					}



					ShipStacks[(i, j)] = newStack;


				}
			}

        }
		//%3 01 2
        public bool CheckWeigthDiff()
		{
			int LeftWeigth = LeftStacks.Select(ls => ls.StackWeigth).Sum();
			int RightWeigth = RightStacks.Select(ls => ls.StackWeigth).Sum();

			float LeftWeigthPrecentage = 100 / (LeftWeigth + RightWeigth) * LeftWeigth ;
			float RightWeigthPrecentage = 100 / (LeftWeigth + RightWeigth) * RightWeigth;

			if(MathF.Abs(LeftWeigth-RightWeigth) > 20.0f)
			{
				return false;
			}
			return true;
		}
		public leftRight PlaceLeftOrRight()
		{
			int LeftWeigth = LeftStacks.Where(ls=>ls.Center == false).Select(ls => ls.StackWeigth).Sum();
			int RightWeigth = RightStacks.Where(ls => ls.Center == false).Select(ls => ls.StackWeigth).Sum();

			if(LeftWeigth< RightWeigth)
			{
				return leftRight.left;
			}
			else
			{
				return leftRight.right;
			}
		}

		public bool TryAddContainerToShip(Container container)
		{
			if(PlaceLeftOrRight() == leftRight.left)
			{
				foreach (var item in LeftStacks)
				{
					if(item.TryAddContainerToStack(container) == true)
					{
						return true;
					}
				}
				foreach (var item in RightStacks)
				{
					if (item.TryAddContainerToStack(container) == true)
					{
						return true;
					}
				}
			}
			else
			{
				foreach (var item in RightStacks)
				{
					if (item.TryAddContainerToStack(container) == true)
					{
						return true;
					}
				}
				foreach (var item in LeftStacks)
				{
					if (item.TryAddContainerToStack(container) == true)
					{
						return true;
					}
				}
			}
			return false;
		}

		public bool HasCenterLine(out int Location)
		{
			bool hasCenter = (width % 2 == 1);
			int center = (int)(width / 2 + 0.5f);
			Location = center;
			return hasCenter;
		}

		public void Fillship(List<Container> containers)
		{
			containers = containers.OrderByDescending(c => c.Cooled).ThenByDescending(c => c.Valuable).ToList();

			foreach(var container in containers)
			{
				TryAddContainerToShip(container);
			}
		}
		public string GetLink()
		{
			string Link = "https://i872272.luna.fhict.nl/ContainerVisualizer/index.html?length=";
			Link += length;
			Link += "&width=";
			Link += width;
			Link += "&stacks=";

			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j< length; j++)
				{
					if (ShipStacks[(i, j)].StackWeigth > 0)
					{
						Link += ShipStacks[(i, j)].GetContainerStringToken();
						if (j != length - 1)
						{
							Link += ",";
						}
					}
					
				}
				if (i != width - 1)
				{
					Link += "/";
				}
				
			}
			Link += "&weights=";

			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < length; j++)
				{
					if (ShipStacks[(i, j)].StackWeigth > 0)
					{
						Link += ShipStacks[(i, j)].GetWeigths();
						if (j != length - 1)
						{
							Link += ",";
						}
					}

				}
				if (i != width - 1)
				{
					Link += "/";
				}

			}
			return Link;
			//3&stacks=141,141/111,111/111,111&weights=1-1-1,1-1-1/1-1-1,1-1-1/1-1-1,1-1-1
		}
		/*public List<ResultFlags> CheckShipStatus()
		{

		}*/

	}

	public enum leftRight
	{
		left, right
	}
	public enum ResultFlags
	{
		success, not_balanced, not_enough_weigth, cooled_miss_placed, no_place_for_cooled, no_place_for_valuable, stack_to_heavy,
	}
}
