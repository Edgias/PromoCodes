using System;

namespace TheRoom.PromoCodes.ApplicationCore.SharedKernel
{
    public static class Guard
    {
        public static void AgainstNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
                throw new ArgumentNullException(argumentName);
        }

        public static void AgainstNullOrEmpty(string argumentValue, string argumentName)
        {
            if (string.IsNullOrEmpty(argumentValue))
                throw new ArgumentNullException(argumentName);
        }

        public static void AgainstZero(int argumentValue, string argumentName)
        {
            if (argumentValue == 0)
                throw new ArgumentException($"Argument '{argumentName}' cannot be zero");
        }

        public static void AgainstZero(decimal argumentValue, string argumentName)
        {
            if (argumentValue == 0)
                throw new ArgumentException($"Argument '{argumentName}' cannot be zero");
        }

    }
}
