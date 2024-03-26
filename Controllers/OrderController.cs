using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement.Data;
using PharmacyManagement.DTO;
using PharmacyManagement.Interfaces;
using PharmacyManagement.Models;

namespace PharmacyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IStoreManager<Order> _storeManager;
        private readonly ILogger<OrderController> _logger;

        private readonly ApplicationDbContext _context;

        public OrderController(IStoreManager<Order> storeManager, ILogger<OrderController> logger, ApplicationDbContext context)
        {
            _storeManager = storeManager;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IList<Order>> Get()
        {
            return await _storeManager.FindAsync();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            return await _storeManager.FindByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Post([FromBody] DtoOrderCreate order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            try
            {
                //order.OrderDate = DateTime.Now;
                var supplier = await _context.Suppliers.FindAsync(order.supplier);
                var ord = await _storeManager.CreateAsync(new() { Supplier= supplier,Name=order.name,OrderDate=DateTime.Now});
                foreach (var item in order.products)
                {
                    _context.Add<OrderProduct>(new() { OrderId = ord.Id, ProductId = item });
                }
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = ord.Id }, ord);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _storeManager.FindByIdAsync(id);
            if (order == null)
            {
                return BadRequest();
            }
            try
            {
                _storeManager.DeleteAsync(order);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Order>> Put(int id, [FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            try
            {
                var ord = await _storeManager.UpdateAsync(order);
                return CreatedAtAction(nameof(Get), new { id = ord.Id }, ord);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
