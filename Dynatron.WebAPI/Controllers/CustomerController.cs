using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dynatron.Application.Entities;
using Dynatron.Application.Interfaces;
using Dynatron.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dynatron.WebAPI.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public CustomerController(ILogger<CustomerController> logger, IMapper mapper, IRepository repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CustomerModel>))]
        public async Task<IActionResult> GetCustomers(CancellationToken cancellationToken)
        {
            var vust = _repository.Customers;

            return Ok(await _repository.Customers
                .ProjectTo<CustomerModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken));
        }

        [HttpGet("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomer(int customerId, CancellationToken cancellationToken)
        {
            var model = await _repository.Customers
                .Where(x => x.CustomerId == customerId)
                .ProjectTo<CustomerModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (model == null)
                return NotFound($"CustomerId: {customerId} not found.");

            return Ok(model);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCustomer(CustomerModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = new CustomerEntity(model.FirstName, model.LastName, model.Email);          
            // You can validate entity through FluentValidation for more complex validations like checking unique email, etc.

            await _repository.Customers.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            var newCustomerModel = await _repository.Customers
                .Where(x => x.CustomerId == entity.CustomerId)
                .ProjectTo<CustomerModel>(_mapper.ConfigurationProvider)
                .SingleAsync(cancellationToken);

            return CreatedAtAction(nameof(GetCustomer), new { customerId = newCustomerModel.CustomerId}, newCustomerModel);
        }

        [HttpPut("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCustomer(int customerId, CustomerModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await _repository.Customers.SingleOrDefaultAsync(c => c.CustomerId == customerId);

            if (entity == null)
                return NotFound($"CustomerId: {customerId} not found.");

            entity.UpdateCustomer(model.FirstName, model.LastName, model.Email);

            await _repository.SaveChangesAsync(cancellationToken);

            var updatedModel = await _repository.Customers
                .Where(x => x.CustomerId == entity.CustomerId)
                .ProjectTo<CustomerModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            return Ok(updatedModel);
        }

        [HttpDelete("{customerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCustomer(int customerId, CancellationToken cancellationToken)
        {
            var entity = await _repository.Customers.SingleOrDefaultAsync(c => c.CustomerId == customerId, cancellationToken);

            if (entity == null)
                return NotFound($"CustomerId: {customerId} not found.");

            _repository.Customers.Remove(entity);
            await _repository.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}
