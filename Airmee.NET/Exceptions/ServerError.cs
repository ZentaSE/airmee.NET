using System;

namespace AirmeeDotNET.Exceptions
{
    internal class ServerError : Exception
    {
        public ServerError(string e) : base(e)
        {
        }
    }
}