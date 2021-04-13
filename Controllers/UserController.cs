using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using dotnetcore_api;
using dotnetcore_api.Models;
using dotnetcore_api.Repository;
using dotnetcore_api.Services;

namespace dotnetcore_api.Controllers
{
    [Route("v1/account")]
    public class UserController : Controller
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var user = UserRepo.Get(model.UserName, model.Password);

            if (user == null)
            {
                return NotFound(new { message = "Usuário não encontrado" });
            }

            var token = TokenService.GenerateToken(user);
            user.Password = "";

            return new
            {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Route("/inicio")]
        [AllowAnonymous]
        public string Inicio() => "Página Inicial";

        [HttpGet]
        [Route("firma")]
        [Authorize]
        public string Firma() => string.Format("Olá {0}", User.Identity.Name);

        [HttpGet]
        [Route("chefia")]
        [Authorize(Roles = "Gerente")]
        public string Chefia() => string.Format("Olá gerente {0}", User.Identity.Name);
    }
}
