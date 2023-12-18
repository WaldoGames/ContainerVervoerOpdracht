using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoerOpdracht_Core.Models
{
	public class Container
	{
        const int BaseWeigth = 4000;

        public int ContentWeigth { get; set; }
        public int FullWeigth { get { return ContentWeigth + BaseWeigth; } }
		public int FullWeigthInTons { get { return (ContentWeigth + BaseWeigth) / 1000; } }


		public bool Valuable { get; set; }
        public bool Cooled { get; set; }

        public Container(int contentWeigth, bool valuable, bool cooled)
        {
			ContentWeigth = contentWeigth;
			Valuable = valuable;
			Cooled = cooled;
        }

        public string GetContainerStringToken()
        {
            if(Valuable && Cooled)
            {
                return "4";
            }
			else if (Cooled)
			{
				return "3";
			}
			else if (Valuable)
			{
				return "2";
			}
			else
			{
				return "1";
			}
		}
    }
}
