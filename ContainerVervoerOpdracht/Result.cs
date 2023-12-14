using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ContainerVervoerOpdracht_Core
{
	public class Result<T>
	{
		public T? Data { get; set; }



	}

	enum ResultFlags
	{
		success, not_balanced, not_enough_weigth, cooled_miss_placed, no_place_for_cooled, no_place_for_valuable, stack_to_heavy,
	}
}
