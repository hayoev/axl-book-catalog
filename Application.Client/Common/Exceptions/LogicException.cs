using System;

namespace Application.Client.Common.Exceptions
{
    public class LogicException: Exception
    {
        public LogicException(string message):base(message)
        {

        }
    }
}