using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Models.ViewModels;
using OnlineLibrary.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace OnlineLibrary.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountServices _accountServices;

        public AccountController(IAccountServices userServices)
        {
            _accountServices = userServices;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(UserInputViewModel inputModel)
        {
            dynamic registered = await _accountServices.SignUpAsync(inputModel);

            if (registered is true)
                return RedirectToAction(nameof(Index), "Home");

            foreach (IdentityError error in registered)
            {
                ModelState.AddModelError(string.Empty, _accountServices.GetErrorMessages(error));
            }

            return View(inputModel);
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(UserInputViewModel inputModel)
        {
            bool userSigned = await _accountServices.SignInAsync(inputModel);

            if (userSigned)
                return RedirectToAction(nameof(Index), "Home");

            ModelState.AddModelError(string.Empty, "Não foi possível encontrar um usuário correspondente às credenciais informadas.");
            return View(inputModel);
        }

        public async Task<IActionResult> SignOut()
        {
            await _accountServices.SignOutAsync();
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
