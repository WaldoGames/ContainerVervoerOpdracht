// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using ContainerVervoerOpdracht_Core.Models;
using Container = ContainerVervoerOpdracht_Core.Models.Container;
Console.WriteLine("Hello, World!");

List<Container> Containers = new List<Container>();

Random random = new Random();

for (int i = 0; i < random.Next(200,300); i++) {

	AddNewContainer(random.Next(7000, 13000), false, false);
}
for (int i = 0; i < random.Next(0, 1); i++)
{

	AddNewContainer(random.Next(7000, 10000), true, false);
}
for (int i = 0; i < random.Next(50, 60); i++)
{

	AddNewContainer(random.Next(6000, 12000), false, true);
}
for (int i = 0; i < random.Next(50, 60); i++)
{

	AddNewContainer(random.Next(1000, 30000), true, true);
}

Ship ship = new Ship(4, 8);



Console.WriteLine(ship.Fillship(Containers));
Console.WriteLine(ship.GetLink());

void AddNewContainer(int weight, bool valueable, bool cooled)
{
	Container c = new Container(weight, valueable, cooled);
	Containers.Add(c);
}
