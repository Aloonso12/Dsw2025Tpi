using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Exceptions
{
    internal class NameObliga : Exception
    {
        public NameObliga(string nom): base($"El campo {nom} es obligatorio") { }
    }
}
