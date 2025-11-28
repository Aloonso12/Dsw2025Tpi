using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Exceptions
{
    internal class StockIn : Exception
    {
        public StockIn(int stock) : base($"El stock {stock} no es valido, debe ser mayor o igual a 0"){ }
    }
}
