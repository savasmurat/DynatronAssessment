using Dynatron.Application.Entities;
using Dynatron.Infrastructure.SeedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dynatron.Infrastructure.Database
{
    public class DataSeeder
    {
        private DatabaseContext _dbContext;

        public DataSeeder(DatabaseContext dbContext)
        {
            _dbContext = dbContext;   
        }

        public void SeedAll()
        {
            SeedCustomers();
        }

        private void SeedCustomers()
        {
            if (_dbContext.Customers.Any())
            {
                return;
            }

            // Read customers from JSON file
            var customersJson = File.ReadAllText("Data/customers.json");
            if (string.IsNullOrWhiteSpace(customersJson))
            {
                throw new Exception("Customers JSON data is null or empty");
            }

            // Deserialize JSON file
            var customers = JsonSerializer.Deserialize<IEnumerable<CustomerModel>>(customersJson);
            if (customers == null)
            {
                throw new Exception("Deserialization of Customers JSON data returned null");
            }

            // Create customers in the database
            foreach(var customer in customers)
            {
                var customerEntity = new CustomerEntity(customer.FirstName, customer.LastName, customer.Email);

                _dbContext.Customers.Add(customerEntity);

                _dbContext.SaveChanges();
            }
        }
    }
}
