using System.Threading.Tasks;
using AutoMapper;
using myJWTAPI.Controllers.Resources;
using myJWTAPI.Core.Models;
using myJWTAPI.Core.Repositories;
using myJWTAPI.Core.Security.Hashing;
using myJWTAPI.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace myJWTAPI.Controllers
{
    [Route("/api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCredentialsResource userCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<UserCredentialsResource, User>(userCredentials);
            
            var response = await _userService.CreateUserAsync(user, ERole.Common);
            if(!response.Success)
            {
                return BadRequest(response.Message);
            }

            var userResource = _mapper.Map<User, UserResource>(response.User);
            return Ok(userResource);
        }
    }
}