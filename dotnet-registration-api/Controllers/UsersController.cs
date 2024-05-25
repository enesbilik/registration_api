using dotnet_registration_api.Data.Entities;
using dotnet_registration_api.Data.Models;
using dotnet_registration_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_registration_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        public UsersController(UserService userService)
        {
            _userService = userService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody]LoginRequest model)
        {
            throw new NotImplementedException();
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody]RegisterRequest model)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            throw new NotImplementedException();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            throw new NotImplementedException();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update(int id, [FromBody]UpdateRequest model)
        {
            throw new NotImplementedException();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
