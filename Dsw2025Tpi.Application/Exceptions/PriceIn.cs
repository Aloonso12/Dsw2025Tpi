using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Exceptions
{
    internal class PriceIn : Exception
    {
        public PriceIn(decimal pre) : base($"El precio {pre} no es valido. Debe ser mayor a 0."){ }
    }
}
