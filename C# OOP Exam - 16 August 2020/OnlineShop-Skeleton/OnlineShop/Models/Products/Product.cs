using OnlineShop.Common.Constants;
using System;

namespace OnlineShop.Models.Products
{
    public abstract class Product : IProduct
    {
        private int id;
        private string manufacturer;
        private string model;
        private decimal price;
        private double overallPerformance;

        protected Product(int id, string manufacturer, string model, decimal price, double overallPerformance)
        {
            Id = id;
            Manufacturer = manufacturer;
            Model = model;
            Price = price;
            OverallPerformance = overallPerformance;
        }

        public int Id//TODO - possible error from exception methods???
        {
            get => id;
            private set
            {
                ThrowExceptionIfValueIsNegative(value, ExceptionMessages.InvalidProductId);
                id = value;
            }
        }

        public string Manufacturer
        {
            get => manufacturer;
            private set
            {
                ThrowExceptionIfValueIsNullOrWhitespace(value, ExceptionMessages.InvalidManufacturer);
                manufacturer = value;
            }
        }

        public string Model
        {
            get => model;
            private set
            {
                ThrowExceptionIfValueIsNullOrWhitespace(value, ExceptionMessages.InvalidModel);
                model = value;
            }
        }
        public virtual decimal Price
        {
            get => price;
            private set
            {
                ThrowExceptionIfValueIsNegative(value, ExceptionMessages.InvalidPrice);
                price = value;
            }
        }
        public virtual double OverallPerformance
        {
            get => overallPerformance;
            private set
            {
                ThrowExceptionOverAllIfValueIsNegative(value, ExceptionMessages.InvalidOverallPerformance);
                overallPerformance = value;
            }
        }

        public override string ToString()
        {
            return $"Overall Performance: {OverallPerformance:f2}. Price: {Price:f2} - {this.GetType().Name}: {Manufacturer} {Model} (Id: {Id})";
        }

        private static void ThrowExceptionIfValueIsNegative(decimal value, string message)
        {
            if (value <= 0)
            {
                throw new ArgumentException(message);
            }
        }

        private static void ThrowExceptionOverAllIfValueIsNegative(double value, string message)
        {
            if (value <= 0)
            {
                throw new ArgumentException(message);
            }
        }

        private static void ThrowExceptionIfValueIsNullOrWhitespace(string value, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(message);
            }
        }
    }
}
