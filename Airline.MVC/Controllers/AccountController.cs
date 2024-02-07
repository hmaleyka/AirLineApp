using Airline.Business.Exceptions;
using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel.AccountVM;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace Airline.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
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
        }
        public async Task<IActionResult> CreateRoles()
        {
            await _service.CreateRoles();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _service.Logout();
                return RedirectToAction(nameof(Login));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
