using Microsoft.AspNetCore.Mvc;
using PharmacyManagement.DTO;
using PharmacyManagement.Interfaces;
using PharmacyManagement.Models;

namespace PharmacyManagement.Controllers
{

    [ApiController]
    [Route("api/admin/[controller]")]
    public class UserController:ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet]
        public async Task<IEnumerable<ApplicationUser>> FindAsync([FromQuery] DtoPagination? pagination)
        {
            return await _userRepository.FindAsync(pagination);
        }

        [HttpGet("{id}")]
        public async Task<ApplicationUser> FindById(string id)
        {
            return await _userRepository.FindById(id);
        }

        [HttpPost]
        public async Task<ActionResult> PostUser([FromBody] ApplicationUser user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            user.CreatedAt = DateTime.Now;
            var CurrentUser = await _userRepository.CreateUser(user);
            return CreatedAtAction(nameof(FindById), new { id= CurrentUser.Id }, CurrentUser);
        }


        [HttpPut("{id}")]

        public async Task<ActionResult> PutUser(string id, [FromBody] ApplicationUser user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            user.UpdatedAt = DateTime.Now;
            var currentUser= await _userRepository.UpdateUser(user);
            return CreatedAtAction(nameof(FindById),new {id=currentUser.Id }, currentUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await FindById(id);
            if (user == null)
            {
                return BadRequest();
            }
            try
            {
                 _userRepository.DeleteUser(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
