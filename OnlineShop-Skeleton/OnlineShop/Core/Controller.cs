using OnlineShop.Common.Constants;
using OnlineShop.Common.Enums;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.Peripherals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Core
{
    public class Controller : IController
    {
        private readonly List<IComputer> computers;
        private readonly List<IComponent> components;
        private readonly List<IPeripheral> peripherals;

        public Controller()
        {
            computers = new List<IComputer>();
            components = new List<IComponent>();
            peripherals = new List<IPeripheral>();
        }
        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            ComputerType computerTypeEnum;
            IComputer computer = null;
            if (!Enum.TryParse(computerType, out computerTypeEnum))
            {
                throw new ArgumentException(ExceptionMessages.InvalidComputerType);
            }

            switch (computerType)
            {
                case nameof(DesktopComputer):
                    computer = new DesktopComputer(id, manufacturer, model, price);
                    break;
                case nameof(Laptop):
                    computer = new Laptop(id, manufacturer, model, price);
                    break;
            }

            if (computers.Any(c => c.Id == computer.Id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComputerId);
            }

            computers.Add(computer);
            return string.Format(SuccessMessages.AddedComputer, computer.Id);
        }


        public string AddComponent(int computerId, int id, string componentType, string manufacturer, string model, decimal price, double overallPerformance, int generation)
        {
            ThrowExceptionIfComputerIdDoesNotExist(computerId);

            ThrowExceptionIfComponentTypeIsInvalid(componentType);

            IComponent component = null;
            switch (componentType)
            {
                case nameof(CentralProcessingUnit):
                    component = new CentralProcessingUnit(id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case nameof(Motherboard):
                    component = new Motherboard(id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case nameof(PowerSupply):
                    component = new PowerSupply(id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case nameof(RandomAccessMemory):
                    component = new RandomAccessMemory(id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case nameof(SolidStateDrive):
                    component = new SolidStateDrive(id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case nameof(VideoCard):
                    component = new VideoCard(id, manufacturer, model, price, overallPerformance, generation);
                    break;
            }

            if (components.Any(c => c.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComponentId);
            }

            IComputer computerToAddIn = computers.FirstOrDefault(c => c.Id == computerId);
            computerToAddIn.AddComponent(component);
            components.Add(component);

            return string.Format(SuccessMessages.AddedComponent, componentType, id, computerId);
            //If it's successful, returns "Component {component type} with id {component id} added successfully in computer with id {computer id}.".
        }


        public string RemoveComponent(string componentType, int computerId)
        {
            ThrowExceptionIfComputerIdDoesNotExist(computerId);

            ThrowExceptionIfComponentTypeIsInvalid(componentType);
            //Removes a component, with the given type from the computer with that id, then removes component from the collection of components.
            IComputer computerToRemoveFrom = computers.FirstOrDefault(c => c.Id == computerId);

            IComponent removedComponent = computerToRemoveFrom.RemoveComponent(componentType);
            components.Remove(removedComponent);
            //If it's successful, it returns "Successfully removed {component type} with id {component id}.".
            return string.Format(SuccessMessages.RemovedComponent, componentType, computerId);
        }



        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
        {
            ThrowExceptionIfComputerIdDoesNotExist(computerId);

            ThrowExceptionIfPeripheralTypeIsInvalid(peripheralType);
            //Creates a peripheral, with the correct type, and adds it to the computer with that id, then adds it to the collection of peripherals in the controller.
            IPeripheral peripheral = null;
            switch (peripheralType)
            {
                case nameof(Headset):
                    peripheral = new Headset(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;
                case nameof(Keyboard):
                    peripheral = new Keyboard(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;
                case nameof(Monitor):
                    peripheral = new Monitor(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;
                case nameof(Mouse):
                    peripheral = new Mouse(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;
            }

            if (peripherals.Any(c => c.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingPeripheralId);
            }
            //If it's successful, it returns "Peripheral {peripheral type} with id {peripheral id} added successfully in computer with id {computer id}.".
            IComputer computerToAddIn = computers.FirstOrDefault(c => c.Id == computerId);
            computerToAddIn.AddPeripheral(peripheral);
            peripherals.Add(peripheral);

            return string.Format(SuccessMessages.AddedPeripheral, peripheralType, id, computerId);
        }



        public string RemovePeripheral(string peripheralType, int computerId)
        {

            ThrowExceptionIfComputerIdDoesNotExist(computerId);

            ThrowExceptionIfPeripheralTypeIsInvalid(peripheralType);

            IComputer computerToRemoveFrom = computers.FirstOrDefault(c => c.Id == computerId);

            IPeripheral removedPeripheral = computerToRemoveFrom.RemovePeripheral(peripheralType);
            peripherals.Remove(removedPeripheral);
            return string.Format(SuccessMessages.RemovedPeripheral, peripheralType, removedPeripheral.Id);
        }



        public string BuyComputer(int id)
        {
            ThrowExceptionIfComputerIdDoesNotExist(id);
            IComputer boughtComputer = computers.FirstOrDefault(c => c.Id == id);

            computers.Remove(boughtComputer);
            //Removes a computer, with the given id, from the collection of computers.
            //If it's successful, it returns ToString method on the removed computer.
            return boughtComputer.ToString();
        }

        public string BuyBest(decimal budget)
        {
            //Removes the computer with the highest overall performance and with a price, less or equal to the budget, from the collection of computers.
            IComputer bestComputer = computers
                .OrderByDescending(c => c.OverallPerformance)//TODO maybe not be the best way to meet the conditions??
                .FirstOrDefault(c => c.Price <= budget);
            if (bestComputer == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CanNotBuyComputer, budget));
            }
            //If there are not any computers in the collection or the budget is insufficient for any computer, throws an ArgumentException with the message " Can't buy a computer with a budget of ${budget}."
            //If it's successful, it returns ToString method on the removed computer.
            computers.Remove(bestComputer);

            return bestComputer.ToString();
        }

        public string GetComputerData(int id)
        {
            ThrowExceptionIfComputerIdDoesNotExist(id);

            IComputer computer = computers.FirstOrDefault(c => c.Id == id);
            return computer.ToString();

        }


        private void ThrowExceptionIfComputerIdDoesNotExist(int computerId)
        {
            if (!computers.Any(c => c.Id == computerId))
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }
        }

        private static ComponentType ThrowExceptionIfComponentTypeIsInvalid(string componentType)
        {
            ComponentType componentTypeEnum;
            if (!Enum.TryParse(componentType, out componentTypeEnum))
            {
                throw new ArgumentException(ExceptionMessages.InvalidComponentType);
            }

            return componentTypeEnum;
        }

        private static void ThrowExceptionIfPeripheralTypeIsInvalid(string peripheralType)
        {
            PeripheralType peripheralTypeEnum;
            if (!Enum.TryParse(peripheralType, out peripheralTypeEnum))
            {
                throw new ArgumentException(ExceptionMessages.InvalidPeripheralType);
            }
        }
    }
}
