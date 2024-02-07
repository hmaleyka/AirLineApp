using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public string name { get; set; }
        public UserNotFoundException(string? message, string paramName) : base(message)
        {
            name = paramName ?? string.Empty;
        }
    }
}
