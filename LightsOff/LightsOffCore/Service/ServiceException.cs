using System;
using System.Collections.Generic;
using System.Text;

namespace LightsOff.LightsOffCore.Service
{
    public class ServiceException : Exception
    {

        public ServiceException(string message) : base(message)
        {
        }

        public ServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
