using System;

namespace AirmeeDotNET.Exceptions
{
    internal class InsufficientParametersException : ArgumentException
    {
        public InsufficientParametersException(string e) : base(e)
        {
        }
    }
}