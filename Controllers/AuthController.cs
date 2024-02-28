using HomeBankingNet8V3.Services.Interfaces;
using HomeBankingNet8V3.Models;
using HomeBankingNet8V3.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HomeBankingNet8V3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IClientRepository _clientRepository;
        private IPasswordHasher _passwordHasher;
        public AuthController(IClientRepository clientRepository, IPasswordHasher passwordHasher)
        {
            _clientRepository = clientRepository;
            _passwordHasher = passwordHasher;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] NewClientDTO NewClientDto)
        {
            try
            {
                Client user = _clientRepository.FindByEmail(NewClientDto.Email);

                var result = _passwordHasher.VerifyPassword(user.HashedPassword, NewClientDto.Password);
                if (result == false)
                {
                    throw new Exception("Username is not correct");
                }


                var claims = new List<Claim>
                {
                    new Claim("Client", user.Email),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                    );

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {

                await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}