using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BiaBraga.API.Dtos;
using BiaBraga.Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BiaBraga.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : BiaBragaBaseController
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IMapper _mapper;
       // private readonly IUsuarioRepositorio _repositorio;

        public UsersController(UserManager<Usuario> userManager,
                              SignInManager<Usuario> signInManager,
                              IMapper mapper
                              //IUsuarioRepositorio usuarioRepositorio,
                              )
        {
            _signInManager = signInManager;
            _mapper = mapper;
            _userManager = userManager;
           //_repositorio = usuarioRepositorio;
        }




        [HttpPost, Route("login"), AllowAnonymous]
        public async Task<IActionResult> Authenticate(LoginDto loginDto)
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
