using System.Collections.Generic;
using System.IO;
using System.Linq;
using OnlineShop.Core;
using OnlineShop.IO;
using OnlineShop.Models.Products.Components;

namespace OnlineShop
{
    public class StartUp
    {
        static void Main()
        {
            //Clears output.txt file
            string pathFile = Path.Combine("..", "..", "..", "output.txt");
            File.Create(pathFile).Close();
            /* new ConsoleWriter();*/
            //new FileWriter(pathFile);
            IReader reader = new ConsoleReader();
            IWriter writer = new FileWriter(pathFile);
            ICommandInterpreter commandInterpreter = new CommandInterpreter();
            IController controller = new Controller();

            IEngine engine = new Engine(reader, writer, commandInterpreter, controller);
            engine.Run();
        }
    }
}
