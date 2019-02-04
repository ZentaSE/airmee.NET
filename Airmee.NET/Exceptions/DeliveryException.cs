using System;

namespace AirmeeDotNET.Exceptions
{
    internal class DeliveryException : Exception
    {
        public DeliveryException(string e) : base(e)
        {
        }
    }
}