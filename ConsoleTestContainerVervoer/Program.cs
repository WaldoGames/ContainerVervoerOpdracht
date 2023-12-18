// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using ContainerVervoerOpdracht_Core.Models;
using Container = ContainerVervoerOpdracht_Core.Models.Container;
Console.WriteLine("Hello, World!");

List<Container> Containers = new List<Container>();

Random random = new Random();

for (int i = 0; i < random.Next(200,300); i++) {

	AddNewContainer(3000, false, false);
}
for (int i = 0; i < random.Next(500, 600); i++)
{

	AddNewContainer(1000, true, false);
}
for (int i = 0; i < random.Next(50, 60); i++)
{

	AddNewContainer(1000, false, true);
}
for (int i = 0; i < random.Next(50, 60); i++)
{

	AddNewContainer(1100, true, true);
}

Ship ship = new Ship(4, 8);



Console.WriteLine(ship.Fillship(Containers));
Console.WriteLine(ship.GetLink());

void AddNewContainer(int weight, bool valueable, bool cooled)
{
	Container c = new Container(weight, valueable, cooled);
	Containers.Add(c);
}
