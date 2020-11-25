
namespace CounterStrike.Utilities.Validators
{
    using CounterStrike.Models.Players.Contracts;
    using System;
    public static class DataValidator
    {
        public static void ThrowArgumentExceptionIfStringValueIsEmpty(string value, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(message);
            }
        }

        public static void ThrowArgumentExceptionIfIntegerValueIsBelowZero(int value, string message)
        {
            if (value < 0)
            {
                throw new ArgumentException(message);
            }
        }

        public static void ThrowArgumentExceptionIfTryingToAddNullInRepository(Object model, string message)
        {
            if (model == null)
            {
                throw new ArgumentException(message);
            }
        }
    }
}
