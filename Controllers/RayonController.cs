using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement.DTO;
using PharmacyManagement.Interfaces;
using PharmacyManagement.Models;

namespace PharmacyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RayonController : ControllerBase
    {
        private readonly IStoreManager<Rayon> _storeManager;
        private readonly ILogger<RayonController> _logger;

        public RayonController(IStoreManager<Rayon> storeManager, ILogger<RayonController> logger)
        {
            _storeManager = storeManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IList<Rayon>> Get([FromQuery] DtoPagination? pagination)
        {
            return await _storeManager.FindAsync(pagination);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Rayon>> Get(int id)
        {
            return await _storeManager.FindByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Rayon>> Post([FromBody] Rayon rayon)
        {
            if (rayon == null)
            {
                return BadRequest();
            }
            try
            {
                var ray = await _storeManager.CreateAsync(rayon);
                return CreatedAtAction(nameof(Get), new { id = ray.Id }, ray);
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
            var rayon = await _storeManager.FindByIdAsync(id);
            if (rayon == null)
            {
                return BadRequest();
            }
            try
            {
                _storeManager.DeleteAsync(rayon);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Rayon>> Put(int id, [FromBody] Rayon rayon)
        {
            if (rayon == null)
            {
                return BadRequest();
            }
            try
            {
                var ray = await _storeManager.UpdateAsync(rayon);
                return CreatedAtAction(nameof(Get), new { id = ray.Id }, ray);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
