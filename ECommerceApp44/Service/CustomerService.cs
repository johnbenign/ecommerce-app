using ECommerceApp44.Model;
using ECommerceApp44.Config;

namespace ECommerceApp44.Service
{
    public class CustomerService
    {
        private readonly CustomDbContext _context;

        public CustomerService(CustomDbContext context)
        {
            _context = context;
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer GetCustomerById(int customerId)
        {
            return _context.Customers.Find(customerId);
        }

        public Customer AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public Customer UpdateCustomer(int customerId, Customer updatedCustomer)
        {
            var existingCustomer = _context.Customers.Find(customerId);
            if (existingCustomer != null)
            {
                existingCustomer.FirstName = updatedCustomer.FirstName;
                existingCustomer.LastName = updatedCustomer.LastName;
                existingCustomer.Email = updatedCustomer.Email;
                existingCustomer.PhoneNumber = updatedCustomer.PhoneNumber;

                _context.SaveChanges();
            }
            return existingCustomer;
        }

        public bool DeleteCustomer(int customerId)
        {
            var customer = _context.Customers.Find(customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }

}

