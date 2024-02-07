using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.Exceptions
{
    public class UserRegistrationException : Exception
    {
        public string name { get; set; }
        public UserRegistrationException(string? message, string paramName) : base(message)
        {
            name = paramName ?? string.Empty;
        }
    }
}
