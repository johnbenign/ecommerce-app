using ECommerceApp44.Model;
using ECommerceApp44.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ECommerceApp44.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public ActionResult<List<Customer>> GetAllCustomers()
        {
            var customers = _customerService.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("{customerId}")]
        public ActionResult<Customer> GetCustomerById(int customerId)
        {
            var customer = _customerService.GetCustomerById(customerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public ActionResult<Customer> AddCustomer([FromBody] Customer customer)
        {
            var createdCustomer = _customerService.AddCustomer(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { customerId = createdCustomer.CustomerId }, createdCustomer);
        }

        [HttpPut("{customerId}")]
        public ActionResult<Customer> UpdateCustomer(int customerId, [FromBody] Customer updatedCustomer)
        {
            var customer = _customerService.UpdateCustomer(customerId, updatedCustomer);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpDelete("{customerId}")]
        public ActionResult DeleteCustomer(int customerId)
        {
            var success = _customerService.DeleteCustomer(customerId);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }

}

