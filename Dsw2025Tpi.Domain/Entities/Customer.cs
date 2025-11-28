using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    public class Customer : EntityBase
    {
        private Customer() { }
        public Customer(string name, string email, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Campo Nombre Vacio", nameof(name));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Campo Email Vacio", nameof(email));

            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
    }
}
