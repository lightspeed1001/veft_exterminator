using System;

namespace Exterminator.Models.Exceptions
{
    public class ModelFormatException : Exception
    {
        public ModelFormatException(string message) : base(message)
        {
        }
    }
}