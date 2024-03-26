using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement.DTO;
using PharmacyManagement.Interfaces;
using PharmacyManagement.Models;

namespace PharmacyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IStoreManager<Client> _storeManager;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IStoreManager<Client> storeManager, ILogger<ClientController> logger)
        {
            _storeManager = storeManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IList<Client>> Get([FromQuery] DtoPagination? pagination)
        {
            return await _storeManager.FindAsync(pagination);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Client>> Get(int id)
        {
            return await _storeManager.FindByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Client>> Post([FromBody] Client client)
        {
            if (client == null)
            {
                return BadRequest();
            }
            try
            {
                var ctl = await _storeManager.CreateAsync(client);
                return CreatedAtAction(nameof(Get), new { id = ctl.Id }, ctl);
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
            var client = await _storeManager.FindByIdAsync(id);
            if (client == null)
            {
                return BadRequest();
            }
            try
            {
                _storeManager.DeleteAsync(client);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Client>> Put(int id, [FromBody] Client client)
        {
            if (client == null)
            {
                return BadRequest();
            }
            try
            {
                var clt = await _storeManager.UpdateAsync(client);
                return CreatedAtAction(nameof(Get), new { id = clt.Id }, clt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
