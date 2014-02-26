namespace Rpg
{
    using System;

    public class NegativeDataException : ApplicationException
    {
        public NegativeDataException(string msg, int value) : base(string.Format("{0}: {1}", msg, value))
        {
        }
    }
}