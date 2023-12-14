// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using ContainerVervoerOpdracht_Core.Models;
using Container = ContainerVervoerOpdracht_Core.Models.Container;
Console.WriteLine("Hello, World!");

List<Container> Containers = new List<Container>();

for (int i = 0; i < 2;i++) {

	AddNewContainer(3000, false, false);
}
for (int i = 0; i < 2; i++)
{

	AddNewContainer(2000, true, false);
}
for (int i = 0; i < 2; i++)
{

	AddNewContainer(2000, false, true);
}
for (int i = 0; i < 2; i++)
{

	AddNewContainer(2000, true, true);
}

Ship ship = new Ship(2, 3);

ship.Fillship(Containers);
Console.WriteLine(ship.GetLink());

void AddNewContainer(int weight, bool valueable, bool cooled)
{
	Container c = new Container(weight, valueable, cooled);
	Containers.Add(c);
}
