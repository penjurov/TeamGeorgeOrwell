namespace Rpg
{
    using System;

    public class NegativeDataException : ApplicationException
    {
        public NegativeDataException(string msg, int value) : base(msg + ": " + value)
        {
        }
    }
}