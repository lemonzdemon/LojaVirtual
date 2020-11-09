using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BiaBraga.API.Models.Enums;
using BiaBraga.API.Services;
using BiaBraga.Domain.Enums;
using BiaBraga.Domain.Models.Dtos;
using BiaBraga.Domain.Models.Entitys;
using BiaBraga.Repository.Classes;
using BiaBraga.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BiaBraga.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;

        public UsersController(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              IMapper mapper,
                              IUserRepository repository,
                              ILogger<UsersController> logger) : base(logger)
        {
            _signInManager = signInManager;
            _mapper = mapper;
            _userManager = userManager;
            _repository = repository;
        }


        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserInsertDto userInsertDto)
        {
            try
            {
                NewLog(nameof(Register), TypeLogger.StartProcess);
                var hasUser = await AnalyzesExistingDataAsync(userInsertDto.CPF, userInsertDto.Email);
                if (!string.IsNullOrEmpty(hasUser))
                {
                    NewLog(nameof(AnalyzesExistingDataAsync),
                    TypeLogger.FinishDivergence,
                    $"Usuario com dados iguais");
                    return StatusCode(409, hasUser);
                }

                NewLog(nameof(AnalyzesExistingDataAsync), TypeLogger.StartMapping, "userDtoToUser");
                var user = _mapper.Map<User>(userInsertDto);

                user.Role = Role.Cliente;
                user.Date = DateTime.UtcNow;
                user.Password = Encript.HashValue(user.Password);
                user.Image = string.Empty;

                IdentityResult result = await _userManager.CreateAsync(user, user.Password);

                if(result.Succeeded)
                {
                    NewLog(nameof(AnalyzesExistingDataAsync), TypeLogger.StartMapping, "userToUserDto");
                    var userToReturn = _mapper.Map<UserViewDto>(user);

                    NewLog(nameof(AnalyzesExistingDataAsync), TypeLogger.FinishSucess);
                    return Created("", userToReturn);
                }
                else
                {
                    return BadRequest("Não foi possível concluir o seu registro, por favor tente novamente em alguns instantes");
                }
            }
            catch (Exception ex)
            {
                return ErrorException(ex, nameof(Register));
            }
        }

        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                NewLog(nameof(Login), TypeLogger.StartProcess);

                var result = await _repository.VerifyLoginAsync(loginDto);

                if(result.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    NewLog(nameof(Login), TypeLogger.Other, "generating token");
                    var newToken = TokenService.GenerateToken(result.Entity);

                    NewLog(nameof(Login), TypeLogger.StartMapping);
                    var userToReturn = _mapper.Map<UserViewDto>(result.Entity);

                    NewLog(nameof(Login), TypeLogger.FinishSucess);
                    return Ok(new
                    {
                        token = newToken,
                        user = userToReturn
                    });
                }
                NewLog(nameof(Login), TypeLogger.FinishDivergence, "unauthorized");
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return ErrorException(ex, nameof(Login));
            }
        }

        #region methods privates

        private async Task<string> AnalyzesExistingDataAsync(string cpf, string email)
        {
            cpf = cpf.Replace("-", string.Empty).Replace(".", string.Empty).Replace("/", string.Empty);
            email = email.ToLower().Trim();

            string result = string.Empty;

            NewLog(nameof(AnalyzesExistingDataAsync),
                TypeLogger.Other,
                $"Verificando existencia de dados cpf:{cpf},email:{email}");

            var usersExisting = await _repository.GetWhereAsync<User>(x =>
               x.Email.ToLower() == email ||
               x.CPF.Replace("-", string.Empty).Replace(".", string.Empty).Replace("/", string.Empty) == cpf);

            if (usersExisting.Any())
            {
                var firstUser = usersExisting.First();

                if (firstUser.Email.ToLower() == email)
                {
                    result = "Email já cadastrado em nossa base de dados";
                }
                else
                {
                    result = "CPF/CNPJ Já cadastrado em nossa base de dados";
                }
            }

            return result;
        }

        #endregion

    }
}
