using System;
using Microsoft.AspNetCore.Mvc;
using TestFullStack.Domain.DTOs;
using TestFullStack.Domain.Services.Interfaces;
using TestFullStack.Domain.Utils;
using TestFullStack.Domain.Entities;

namespace TestFullStack.WebApi.Controllers
{

    
    [Route("")]
    [Produces("application/json")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        /// <summary>
        /// Do User Authentication on WebApi
        /// </summary>
        /// <param name="login">Login and user password for authentication</param>
        /// <returns>Authentication token to be used in private requests</returns>
        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody]LoginRequest login)
        {
            try
            {
                var token = _authService.Signin(login);
                if (token == null) return Ok( new { Token = false});
               
                var tokenString = token.GenerateToken();

                return Ok(new { Token = tokenString });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Do the user registration in the WebApi
        /// </summary>
        /// <param name="user">User object with name, username, password and email properties.</param>
        /// <returns>Authentication token to be used in private requests.</returns>
        [HttpPost]
        [Route("signup")]
        public IActionResult Signup([FromBody]User user)
        {
            try
            {
                var token = _authService.Signup(user);
                if (token == null) return Ok(new { Token = false });

                var tokenString = token.GenerateToken();

                return Ok(new { Token = tokenString });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }


    }
}
