using System;

namespace Application.Admin.Common.Exceptions
{
    public class LogicException: Exception
    {
        public LogicException(string message):base(message)
        {

        }
    }
}