using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement.Data;
using PharmacyManagement.DTO;
using PharmacyManagement.Interfaces;
using PharmacyManagement.Models;

namespace PharmacyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenteController : ControllerBase
    {
        private readonly IStoreManager<Vente> _storeManager;
        private readonly ILogger<VenteController> _logger;

        private readonly ApplicationDbContext _context;

        public VenteController(IStoreManager<Vente> storeManager, ILogger<VenteController> logger, ApplicationDbContext context)
        {
            _storeManager = storeManager;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IList<Vente>> Get([FromQuery] DtoPagination? pagination)
        {
            return await _storeManager.FindAsync(pagination);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Vente>> Get(int id)
        {
            return await _storeManager.FindByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Vente>> Post([FromBody] DtoCreateVente vente)
        {
            if (vente == null)
            {
                return BadRequest();
            }
            try
            {
                var user = await _context.Clients.FindAsync(vente.Owner);
                Vente v = new() { Client = user!, Date = DateTime.Now, Title = vente.Title,TotalPrice=vente.price};
                var vent = await _storeManager.CreateAsync(v);
                for(int i = 0; i < vente.products.Count(); i++)
                {
                    _context.Add<VenteProduct>(new() { ProductId = vente.products[i], VenteId = vent.Id});
                }
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = vent.Id }, vent);
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
            var vente = await _storeManager.FindByIdAsync(id);
            if (vente == null)
            {
                return BadRequest();
            }
            try
            {
                _storeManager.DeleteAsync(vente);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Vente>> Put(int id, [FromBody] Vente vente)
        {
            if (vente == null)
            {
                return BadRequest();
            }
            try
            {
                vente.Date = DateTime.Now;
                var vent = await _storeManager.UpdateAsync(vente);
                return CreatedAtAction(nameof(Get), new { id = vent.Id }, vent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
