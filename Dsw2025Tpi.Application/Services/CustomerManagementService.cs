using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Services
{
    public class CustomerManagementService
    {
        private readonly IRepository _repo;
        public CustomerManagementService(IRepository repo) => _repo = repo;


        public async Task<CustomerResponse> AddCustomer(CustomerRequest dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || 
                string.IsNullOrWhiteSpace(dto.Email))
            {
                throw new ArgumentException("Valores para el cliente no válidos");
            }
            var c = new Customer(dto.Name, dto.Email, dto.PhoneNumber);
            await _repo.Add<Customer>(c);
            return new CustomerResponse(c.Id, c.Name, c.Email, c.PhoneNumber);
        }
        public async Task<IEnumerable<CustomerResponse>> GetAllCustomers() =>
            (await _repo.GetAll<Customer>()).Select(c => new CustomerResponse(c.Id, c.Name, c.Email, c.PhoneNumber));
        public async Task<CustomerResponse> GetCustomerById(Guid id)
        {
            var c = await _repo.GetById<Customer>(id) ?? throw new KeyNotFoundException();
            return new CustomerResponse(c.Id, c.Name, c.Email, c.PhoneNumber);
        }
    }
}
