using EasterRaces.Core.Contracts;
using EasterRaces.IO;
using EasterRaces.IO.Contracts;
using EasterRaces.Core.Entities;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
using System;
using EasterRaces.Models.Drivers.Entities;
using System.IO;

namespace EasterRaces
{
    public class StartUp
    {
        public static void Main()
        {

            string pathFile = Path.Combine("..", "..", "..", "output.txt");
            File.Create(pathFile).Close();
            IChampionshipController controller = new ChampionshipController();
            IReader reader = new ConsoleReader();
            IWriter writer = new FileWriter(pathFile);
            //IWriter writer = new ConsoleWriter();

            Engine enigne = new Engine(controller, reader, writer);
            enigne.Run();

            //try
            //{
            //    Driver driver = new Driver("Pehso");
            //}
            //catch (ArgumentException ae)
            //{
            //    Console.WriteLine(ae.Message);
            //}
        }
    }
}
