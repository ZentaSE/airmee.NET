using System;

namespace AirmeeDotNET.Exceptions
{
    internal class InvalidParametersException : ArgumentException
    {
        public InvalidParametersException(string e) : base(e)
        {
        }
    }
}