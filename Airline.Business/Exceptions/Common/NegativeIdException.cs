using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.Exceptions.Common
{
    public class NegativeIdException : Exception
    {
        public NegativeIdException(string message) : base(message)
        {
        }
    }
}
