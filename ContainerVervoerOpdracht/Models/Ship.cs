using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace ContainerVervoerOpdracht_Core.Models
{
	public class Ship
	{
		Dictionary<(int, int), ContainerStack> ShipStacks;
		int width;
		int length;

		List<ContainerStack> LeftAndCenterStacks;
		List<ContainerStack> RightStacks;

		int failedCount = 0;
		int failedCooled = 0;
		int failedValuable = 0;
		int failedCooledAndValuable = 0;
		int failedNormal = 0;
		int failedTooHeavy = 0;

		public Ship(int width, int length)
        {
			this.width = width;
			this.length = length;

            ShipStacks = new Dictionary<(int, int), ContainerStack>();
			LeftAndCenterStacks = new List<ContainerStack>();
			RightStacks = new List<ContainerStack>();

			bool hasCenter = HasCenterLine(out int location);

			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < length; j++)
				{
					ContainerStack newStack = CreateNewContainerStack(i, j, hasCenter, location);

					AddStackToSideList(newStack, hasCenter, location, i);

					ShipStacks[(i, j)] = newStack;

				}
			}

        }
		public ContainerStack CreateNewContainerStack(int x, int y, bool HasCenterLine, int Middle)
		{
			bool valuablesAllowed = (y % 3 != 2);
			bool coolable = (y == 0);
			ContainerStack newStack;
			if (HasCenterLine && Middle == x)
			{
				newStack = new ContainerStack(valuablesAllowed, coolable, true);
			}
			else
			{
				newStack = new ContainerStack(valuablesAllowed, coolable);
			}
			return newStack;
		}
		public void AddStackToSideList(ContainerStack newStack, bool HasCenterLine, int Middle, int x)
		{
			if (HasCenterLine)
			{
				if (x <= Middle)
				{
					LeftAndCenterStacks.Add(newStack);
				}
				else
				{
					RightStacks.Add(newStack);
				}
			}
			else
			{
				if (x < Middle)
				{
					LeftAndCenterStacks.Add(newStack);
				}
				else
				{
					RightStacks.Add(newStack);
				}
			}

		}
        public bool CheckWeigthDiff()
		{
			int LeftWeigth = LeftAndCenterStacks.Where(ls => ls.Center == false).Select(ls => ls.StackWeigth).Sum();
			int RightWeigth = RightStacks.Where(ls => ls.Center == false).Select(ls => ls.StackWeigth).Sum();

			float LeftWeigthPrecentage = 100 / (LeftWeigth + RightWeigth) * LeftWeigth ;
			float RightWeigthPrecentage = 100 / (LeftWeigth + RightWeigth) * RightWeigth;

			if(MathF.Abs(LeftWeigthPrecentage - RightWeigthPrecentage) > 20.0f)
			{
				return false;
			}
			return true;
		}
		public leftRight PlaceLeftOrRight()
		{
			int LeftWeigth = LeftAndCenterStacks.Where(ls=>ls.Center == false).Select(ls => ls.StackWeigth).Sum();
			int RightWeigth = RightStacks.Where(ls => ls.Center == false).Select(ls => ls.StackWeigth).Sum();

			if(LeftWeigth< RightWeigth)
			{
				return leftRight.left;
			}
				return leftRight.right;
		}
		public bool checkStacksLeft(Container container)
		{
			foreach (var item in LeftAndCenterStacks.OrderBy(c => c.stack.Count).ThenByDescending(c => c.ValuableAllowed))
			{
				if (TryPlaceContainerOnStack(container, item))
				{
					return true;
				}
			}
			return false;
		}
		public bool checkStacksRight(Container container)
		{
			foreach (var item in RightStacks.OrderBy(c => c.stack.Count).ThenByDescending(c => c.ValuableAllowed))
			{
				if (TryPlaceContainerOnStack(container, item))
				{
					return true;
				}
			}
			return false;
		}
		public bool TryPlaceContainerOnStack(Container container, ContainerStack stack)
		{
			(int, int) key = ShipStacks.FirstOrDefault(x => x.Value == stack).Key;

			if (CheckPostionForAdjacentContainers(key.Item1, key.Item2))
			{
				if (stack.TryAddContainerToStack(container) == true)
				{
					return true;
				}
			}
			return false;
		}
		public bool TryAddContainerToShip(Container container)
		{
			if(PlaceLeftOrRight() == leftRight.left)
			{
				if (checkStacksLeft(container)) return true;
				if (checkStacksRight(container)) return true;
			}
			else
			{
				if (checkStacksRight(container)) return true;
				if (checkStacksLeft(container)) return true;
				
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
		public string Fillship(List<Container> containers, bool simpleReturnString = false)
		{
			containers = containers.OrderByDescending(c => c.Cooled).ThenByDescending(c => c.Valuable).ToList();
			failedCount = containers.Count;
			foreach (var container in containers)
			{
				if (container.FullWeigthInTons > 30)
				{
					failedTooHeavy++;
					failedCount--;
				}
				
				else if (!TryAddContainerToShip(container))
				{
					AddFailedContainerToCounter(container);
				}
			}
			if (simpleReturnString)
			{
				return failedCount.ToString() + "/" + containers.Count.ToString();
			}
			return failedCount.ToString()+"/"+ containers.Count.ToString() +" containers placed missing containers \n valuable: " + failedValuable +"\n coolable: " + failedCooled + "\n valuable and cooled: " + failedCooledAndValuable + "\n normal: " + failedNormal+"\n Too heavy: "+ failedTooHeavy;
		}
		public void AddFailedContainerToCounter(Container container)
		{
			failedCount -= 1;
			if (container.Valuable == true && container.Cooled == true)
			{
				failedCooledAndValuable++;
			}
			else if (container.Valuable == true)
			{
				failedValuable++;
			}
			else if (container.Cooled == true)
			{
				failedCooled++;
			}
			else
			{
				failedNormal++;
			}
		}
		public bool CheckBoundsX(int x, int y)
		{
			if (x < 0 || x >= width)
			{
				return false;
			}
			return true;
		}
		public bool CheckBoundsFront(int x, int y)
		{
			if (y >= length)
			{
				return false;
			}
			return true;
		}
		public bool CheckBoundsBack(int x, int y)
		{
			if (y < 0)
			{
				return false;
			}
			return true;
		}
		public bool CheckFront(int x, int y)
		{
			if (!CheckBoundsX(x, y) || !CheckBoundsFront(x, y)) return false;

			if (y < length - 1)
			{
				if (ShipStacks[(x, y + 1)].stack.Count <= ShipStacks[(x, y)].stack.Count + 1 && ShipStacks[(x, y - 1)].ContainsValuable)
				{
					return false;
				}

			}
			return true;
		}
		public bool CheckBack(int x, int y)
		{
			if (!CheckBoundsX(x, y) || !CheckBoundsBack(x, y)) return false;

			if (y > 0)
			{
				if (ShipStacks[(x, y - 1)].stack.Count <= ShipStacks[(x, y)].stack.Count + 1 && ShipStacks[(x,y-1)].ContainsValuable)
				{
					return false;
				}
			}
			return true;
		}
		public bool CheckPostionForAdjacentContainers(int x, int y)
		{
			if (!ShipStacks[(x, y)].ValuableAllowed)
			{
				if (!CheckFront(x, y)) return false;

				if (!CheckBack(x, y)) return false;
			}
			return true;
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
				for (int j = 0; j < length; j++)
				{
					if (ShipStacks[(i, j)].StackWeigth > 0)
					{
						Link += ShipStacks[(i, j)].GetContainerStringToken();

					}
					if (j != length - 1)
					{
						Link += ",";
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

					}
					if (j != length - 1)
					{
						Link += ",";
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

		public bool TryAddContainerDirectlyToGrid_TEST_ONLY_FUNTION(Container c, int x, int y)
		{
			if(!CheckBoundsX(x, y) || !CheckBoundsFront(x, y)|| !CheckBoundsBack(x, y))
			{
				return false;
			}

			ShipStacks[(x,y)].TryAddContainerToStack(c);
			return true;
		}
		public bool TryFillContainerStackDirectlyToGrid_TEST_ONLY_FUNTION(List<Container> c, int x, int y)
		{
			if (!CheckBoundsX(x, y) || !CheckBoundsFront(x, y) || !CheckBoundsBack(x, y))
			{
				return false;
			}

			ShipStacks[(x, y)].stack.AddRange(c);
			return true;
		}
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
