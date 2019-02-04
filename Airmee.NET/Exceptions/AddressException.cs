using System;

namespace AirmeeDotNET.Exceptions
{
    internal class AddressException : Exception
    {
        public AddressException(string e) : base(e)
        {
        }
    }
}