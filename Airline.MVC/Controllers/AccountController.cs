using Airline.Business.Exceptions;
using Airline.Business.Services.Implementations;
using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel.AccountVM;
using Airline.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace Airline.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _service;
        private readonly UserManager<AppUser> _userManager;
        public AccountController(IAccountService service, UserManager<AppUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        public IActionResult Register()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if(!ModelState.IsValid)return View();
            try
            {
                await _service.Register(register);

                return RedirectToAction(nameof(Login));
            }
            catch (UsedEmailException ex)
            {
                ModelState.AddModelError(ex.name, ex.Message);

                return View();
            }
            catch (UserRegistrationException ex)
            {
                ModelState.AddModelError(ex.name, ex.Message);

                return View();
            }
            catch (NullFieldException ex)
            {
                ModelState.AddModelError(ex.name, ex.Message);

                return View();
            }
        }

        public async Task<IActionResult> Login(string? ReturnUrl)
        {
            if (!ModelState.IsValid) return View();
            if (!User.Identity.IsAuthenticated)
            {
                if (ReturnUrl is not null) return Redirect(ReturnUrl);

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm, string? returnUrl)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                await _service.Login(vm);

                if (returnUrl is not null) return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError(ex.name, ex.Message);

                return View();
            }
            catch (NullFieldException ex)
            {
                ModelState.AddModelError(ex.name, ex.Message);

                return View();
            }
            catch (UsedEmailException ex)
            {
                ModelState.AddModelError(ex.name, ex.Message);
                return View();
            }
            
        }
        public async Task<IActionResult> CreateRoles()
        {
            await _service.CreateRoles();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _service.Logout();
            return RedirectToAction("Index", "Home");


        }
        public IActionResult Subscription()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Subscription(SubscribeVM vm, string? returnUrl)
        {
            try
            {
                await _service.Subscription(vm);

                if (returnUrl is not null) return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }
            catch (UsedEmailException ex)
            {
                ModelState.AddModelError(ex.name, ex.Message);

                if (returnUrl is not null) return Redirect(returnUrl);

                return RedirectToAction("Index", "Home", vm);
            }
        }

        public async Task<IActionResult> ConfirmEmail(string Id, string token)
        {
            var user = await _userManager.FindByIdAsync(Id);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                ViewBag.IsSuccess = true;
            }
            else
            {
                ViewBag.IsSuccess = false;
            }
            return View();
        }

    }
}
