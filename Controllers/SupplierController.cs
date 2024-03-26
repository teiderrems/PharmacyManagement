using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement.Interfaces;
using PharmacyManagement.Models;

namespace PharmacyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IStoreManager<Supplier> _storeManager;
        private readonly ILogger<SupplierController> _logger;

        public SupplierController(IStoreManager<Supplier> storeManager, ILogger<SupplierController> logger)
        {
            _storeManager = storeManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IList<Supplier>> Get()
        {
            return await _storeManager.FindAsync();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Supplier>> Get(int id)
        {
            return await _storeManager.FindByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Supplier>> Post([FromBody] Supplier supplier)
        {
            if (supplier == null)
            {
                return BadRequest();
            }
            try
            {
                supplier.CreatedAt = DateTime.Now;
                var supp = await _storeManager.CreateAsync(supplier);
                return CreatedAtAction(nameof(Get), new { id = supp.Id }, supp);
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
            var supplier = await _storeManager.FindByIdAsync(id);
            if (supplier == null)
            {
                return BadRequest();
            }
            try
            {
                _storeManager.DeleteAsync(supplier);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Supplier>> Put(int id, [FromBody] Supplier supplier)
        {
            if (supplier == null)
            {
                return BadRequest();
            }
            try
            {
                supplier.UpdatedAt = DateTime.Now;
                var cat = await _storeManager.UpdateAsync(supplier);
                return CreatedAtAction(nameof(Get), new { id = cat.Id }, cat);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
