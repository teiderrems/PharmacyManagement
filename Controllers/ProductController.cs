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
    public class ProductController : ControllerBase
    {
        private readonly IStoreManager<Product> _storeManager;
        private readonly ILogger<ProductController> _logger;

        private readonly ApplicationDbContext _context;

        public ProductController(IStoreManager<Product> storeManager, ILogger<ProductController> logger , ApplicationDbContext context)
        {
            _storeManager = storeManager;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IList<Product>> Get([FromQuery] DtoPagination? pagination)
        {
            return await _storeManager.FindAsync(pagination);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            return await _storeManager.FindByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] DtoCreateProduct product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            try
            {
                var prod = await _storeManager.CreateAsync(new() { Name=product.Name,Description=product.Description,CreatedAt= DateTime.Now });
                foreach (var item in product.categories)
                {
                   _context.Add<CategorieProduct>(new() { CategorieId = item, ProductId = prod.Id });
                }
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = prod.Id }, prod);
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
            var product = await _storeManager.FindByIdAsync(id);
            if (product == null)
            {
                return BadRequest();
            }
            try
            {
                _storeManager.DeleteAsync(product);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> Put(int id, [FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            try
            {
                product.UpdatedAt = DateTime.Now;
                var cat = await _storeManager.UpdateAsync(product);
                return CreatedAtAction(nameof(Get), new { id = cat.Id }, cat);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
