using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BiaBraga.Repository.Interfaces;
using BiaBraga.Domain.Models.Entitys;
using Microsoft.AspNetCore.Authorization;
using BiaBraga.Domain.Models;
using BiaBraga.Admin.Models.FormViewModel.Users;
using BiaBraga.Domain.Enums;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using BiaBraga.Admin.Services;

namespace BiaBraga.Admin.Controllers
{
    [AuthService(Role.Administrador, Role.Supervisor)]
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToRoute(new
                {
                    controller = "Home",
                    action = "Index"
                });
            }

            return View();
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.VerifyLoginAsync(login);

                if (result.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    var claims = new List<Claim>
                        {
                        new Claim(ClaimTypes.Name, result.Entity.Name),
                        new Claim(ClaimTypes.Role, result.Entity.Role.ToString()),
                        new Claim(ClaimTypes.Email, result.Entity.Email),
                    };

                    var identidadeDeUsuario = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identidadeDeUsuario);

                    var propriedadeDeAutentificacao = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTime.Now.ToLocalTime().AddHours(2),
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, propriedadeDeAutentificacao);

                    return RedirectToRoute(new
                    {
                        controller = "Home",
                        action = "Index"
                    });
                }

                return View(new LoginViewModel
                {
                    Login = login,
                    ResultDefault = result
                });
            }

            return View(new LoginViewModel
            {
                Login = login,
                ResultDefault = new ResultDefault
                {
                    HttpStatusCode = System.Net.HttpStatusCode.Unauthorized,
                    Message = "Campos devem estar preenchidos"
                }
            });
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAsync<User>());
        }

        [Route("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _repository.GetByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Nick,AboutMe,Password,CPF,Birth,Telephone,CellPhone,Email,ReceiveCellPhoneMessage,ReceiveEmailMessage,Date,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _repository.GetByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [Route("Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Nick,AboutMe,Password,CPF,Birth,Telephone,CellPhone,Email,ReceiveCellPhoneMessage,ReceiveEmailMessage,Date,Role")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _repository.UpdateAsync(user);

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _repository.GetByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [Route("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }

        [Route("Logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
