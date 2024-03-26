using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement.DTO;
using PharmacyManagement.Interfaces;
using PharmacyManagement.Models;

namespace PharmacyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorieController : ControllerBase
    {
        private readonly IStoreManager<Categorie> _storeManager;
        private readonly ILogger<CategorieController> _logger;

        public CategorieController(IStoreManager<Categorie> storeManager, ILogger<CategorieController> logger)
        {
            _storeManager = storeManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IList<Categorie>> Get([FromQuery]DtoPagination? pagination)
        {
            return await _storeManager.FindAsync(pagination);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Categorie>> Get(int id)
        {
            return await _storeManager.FindByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Categorie>> Post([FromBody] Categorie categorie)
        {
            if (categorie == null)
            {
                return BadRequest();
            }
            try
            {
                var cat = await _storeManager.CreateAsync(categorie);
                return CreatedAtAction(nameof(Get), new { id=cat.Id }, cat);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public  async Task<IActionResult> Delete(int id)
        {
            var cat = await _storeManager.FindByIdAsync(id);
            if (cat==null)
            {
                return BadRequest();
            }
            try
            {
                _storeManager.DeleteAsync(cat);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Categorie>> Put(int id, [FromBody] Categorie categorie)
        {
            if (categorie==null)
            {
                return BadRequest();
            }
            try
            {
                var cat=await _storeManager.UpdateAsync(categorie);
                return CreatedAtAction(nameof(Get),new {id= cat.Id}, cat);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
