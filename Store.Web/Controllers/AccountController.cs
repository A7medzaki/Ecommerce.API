using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Data.Entities.IdentityEntities;
using Store.Service.HandleResponses;
using Store.Service.Services.UserService;
using Store.Service.Services.UserService.DTOs;

namespace Store.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IUserService userService, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO input)
        {
            var user = await _userService.Login(input);

            if (user is null)
                return BadRequest(new CustomException(400, "Email Not Found"));

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO input)
        {
            var user = await _userService.Register(input);

            if (user is null)
                return BadRequest(new CustomException(400, "Email Already Exists"));

            return Ok(user);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentDetails()
        {
            var userIdClaim = User?.FindFirst("UserId");

            if (userIdClaim == null)
                return Unauthorized("User ID claim is missing");

            var user = await _userManager.FindByIdAsync(userIdClaim.Value);

            if (user == null)
                return NotFound("User not found");

            return new UserDTO
            {
                Id = Guid.Parse(user.Id),
                DisplayName = user.DisplayName,
                Email = user.Email
            };
        }



    }
}
