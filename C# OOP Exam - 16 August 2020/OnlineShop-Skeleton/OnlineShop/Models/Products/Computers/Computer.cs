
using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Peripherals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Models.Products.Computers
{
    public abstract class Computer : Product, IComputer
    {
        private readonly List<IComponent> components;
        private readonly List<IPeripheral> peripherals;

        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            components = new List<IComponent>();
            peripherals = new List<IPeripheral>();
        }
        public override double OverallPerformance
            => !Components.Any() ? base.OverallPerformance
            : base.OverallPerformance + Components.Average(c => c.OverallPerformance);

        public override decimal Price
            => base.Price + Components.Sum(c => c.Price) + Peripherals.Sum(p => p.Price);


        public IReadOnlyCollection<IComponent> Components => components.AsReadOnly();
        public IReadOnlyCollection<IPeripheral> Peripherals => peripherals.AsReadOnly();


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine($" Components ({Components.Count}):");
            foreach (IComponent component in Components)
            {
                sb.AppendLine($"  {component}");
            }
            double averageOP = Peripherals.Any() ? Peripherals.Average(p => p.OverallPerformance) : 0;
            sb.AppendLine($" Peripherals ({Peripherals.Count}); " +
                $"Average Overall Performance ({averageOP:f2}):");
            foreach (IPeripheral peripheral in Peripherals)
            {
                sb.AppendLine($"  {peripheral}");
            }

            return sb.ToString().TrimEnd();
        }

        public void AddComponent(IComponent component)
        {
            if (components.Any(c => c.GetType() == component.GetType()))
            {
                throw new ArgumentException(
                    string.Format(ExceptionMessages.ExistingComponent, component.GetType().Name, GetType().Name, Id));
            }
            components.Add(component);
        }
        public IComponent RemoveComponent(string componentType)
        {
            //If the components collection is empty or does not have a component of that type, throw an ArgumentException with the message "Component {component type} does not exist in {computer type} with Id {id}."
            if (!components.Any() || !components.Any(c => c.GetType().Name == componentType))
            {
                throw new ArgumentException(
                    string.Format(ExceptionMessages.NotExistingComponent, componentType, GetType().Name, Id));
            }
            IComponent removedComponent = components.FirstOrDefault(c => c.GetType().Name == componentType);
            components.Remove(removedComponent);
            return removedComponent;

        }
        public void AddPeripheral(IPeripheral peripheral)
        {
            if (peripherals.Any(c => c.GetType() == peripheral.GetType()))
            {
                throw new ArgumentException(
                    string.Format(ExceptionMessages.ExistingPeripheral, peripheral.GetType().Name, GetType().Name, Id));
            }

            peripherals.Add(peripheral);

        }
        public IPeripheral RemovePeripheral(string peripheralType)
        {
            //If the peripherals collection is empty or does not have a peripheral of that type, throw an ArgumentException with the message "Peripheral {peripheral type} does not exist in {computer type} with Id {id}."
            if (!peripherals.Any() || !peripherals.Any(p => p.GetType().Name == peripheralType))
            {
                throw new ArgumentException(
                    string.Format(ExceptionMessages.NotExistingPeripheral, peripheralType, GetType().Name, Id));
            }
            IPeripheral removedPeripheral = peripherals.FirstOrDefault(p => p.GetType().Name == peripheralType);
            peripherals.Remove(removedPeripheral);
            return removedPeripheral;
        }

    }
}
