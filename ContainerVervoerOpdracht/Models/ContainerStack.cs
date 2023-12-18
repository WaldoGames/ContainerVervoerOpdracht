using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoerOpdracht_Core.Models
{
	public class ContainerStack
	{
		public List<Container> stack;

		const int MaxWeigth = 120000;

		public int StackWeigth
		{
			get {
				int returnVal = 0;
                foreach (var item in stack)
                {
					returnVal += item.FullWeigth;
                }
				return returnVal;
            }
		}
		public bool ContainsValuable
		{
			get
			{
				foreach (var item in stack)
				{
					if (item.Valuable)
					{
						return true;
					}
				}
				return false;
			}
		}

        public bool ValuableAllowed { get; set; }

        public bool CoolableStack { get; set; }

		public bool Center {  get; set; }

        public ContainerStack(bool ValuableAllowed, bool CoolableStack, bool Center= false)
		{
			this.ValuableAllowed = ValuableAllowed;
			this.CoolableStack = CoolableStack;
			this.Center = Center;
			stack = new List<Container>();

        }
		public string GetWeigths()
		{
			string s = "";
			bool first = true;
			foreach (var item in stack)
			{
				if (first)
				{
					first = false;
				}
				else
				{
					s += "-";
				}
				s += item.FullWeigthInTons;
			}
			return s;
		}
		public bool TryAddContainerToStack(Container container)
		{
			if(container.Valuable && (ValuableAllowed == false || ContainsValuable==true))
			{
				return false;
			}
			if(container.Cooled && !CoolableStack)
			{
				return false;
			}
			if(StackWeigth + container.FullWeigth > MaxWeigth)
			{
				return false;
			}

			if (container.Valuable)
			{
				stack.Add(container);
			}
			else
			{
				stack.Insert(0, container);
			}
			return true;
		}
		public string GetContainerStringToken()
		{
			string s = "";
			for (int i = 0; i < stack.Count; i++)
			{
				s += stack[i].GetContainerStringToken();
				if (i != stack.Count - 1)
				{
					s += "-";
				}
			}
			return s;
		}
	}
}
