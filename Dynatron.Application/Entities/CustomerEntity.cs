using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynatron.Application.Entities
{
    public class CustomerEntity
    {
        public int CustomerId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateTime CreatedDateTime { get; private set; }
        public DateTime? LastUpdatedDateTime { get; private set; }

#nullable disable
        public CustomerEntity() { }
#nullable enable

        public CustomerEntity(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CreatedDateTime = DateTime.Now;
        }

        public void UpdateCustomer(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            LastUpdatedDateTime = DateTime.Now;
        }
    }
}
