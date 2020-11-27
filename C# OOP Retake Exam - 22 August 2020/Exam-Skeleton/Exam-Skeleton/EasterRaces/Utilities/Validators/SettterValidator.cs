

using System;

namespace EasterRaces.Utilities.Validators
{
    public static class SettterValidator
    {
        public static void ValidateName(string value, int minimumlength, string message)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < minimumlength)
            {
                throw new ArgumentException(message);

            }
        }
    }
}
