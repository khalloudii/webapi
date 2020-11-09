using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities.Identity;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Errors;
using WebAPI.Extensions;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager
            , SignInManager<AppUser> signInManager
            , ITokenService tokenService
            , IMapper mapper)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserViewModel>> GetCurrentUser()
        {
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            //var user = await _userManager.FindByEmailAsync(email);
            var user = await _userManager.FindByEmailFromClaimsPrincipal(HttpContext.User);

            return new UserViewModel
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressViewModel>> GetUserAddress()
        {
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            //var user = await _userManager.FindByEmailAsync(email);
            var user = await _userManager.FindByUserByClaimsPrincipalWithAddressAsync(HttpContext.User);

            return _mapper.Map<Address, AddressViewModel>(user.Address);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressViewModel>> UpdateUserAddress(AddressViewModel address)
        {
            var user = await _userManager.FindByUserByClaimsPrincipalWithAddressAsync(HttpContext.User);

            user.Address = _mapper.Map<AddressViewModel, Address>(address);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return Ok(_mapper.Map<Address, AddressViewModel>(user.Address));

            return BadRequest("problem updating usr");
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserViewModel>> Login(LoginViewModel loginVm)
        {
            var user = await _userManager.FindByEmailAsync(loginVm.Email);

            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginVm.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new UserViewModel
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserViewModel>> Register(RegisterViewModel registerVm)
        {
            if (CheckEmailExistsAsync(registerVm.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse{Errors = new[]
                {"Email address is in use"}});
            }

            var user = new AppUser
            {
                DisplayName = registerVm.DisplayName,
                Email = registerVm.Email,
                UserName = registerVm.Email
            };

            var result = await _userManager.CreateAsync(user, registerVm.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new UserViewModel
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            };
        }
    }
}