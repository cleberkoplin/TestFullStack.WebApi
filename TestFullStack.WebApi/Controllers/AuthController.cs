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
        /// Efetua a Autenticação do Usuário
        /// </summary>
        /// <param name="login">Login e Senha do Usuário para a Autenticação</param>
        /// <returns>Token de Autenticação à ser utilizado nas requisições privadas</returns>
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
