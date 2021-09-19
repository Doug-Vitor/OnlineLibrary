using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Models.ViewModels;
using OnlineLibrary.Services;
using OnlineLibrary.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace OnlineLibrary.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountServices _userServices;

        public AccountController(AccountServices userServices)
        {
            _userServices = userServices;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(UserInputViewModel inputModel)
        {
            dynamic registered = await _userServices.SignUpAsync(inputModel);

            if (registered is true)
                return RedirectToAction(nameof(Index), "Home");

            foreach (IdentityError error in registered)
            {
                ModelState.AddModelError("", _userServices.GetErrorMessages(error));
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
            bool userSigned = await _userServices.SignInAsync(inputModel);

            if (userSigned)
                return RedirectToAction(nameof(Index), "Home");

            ModelState.AddModelError("", "Não foi possível encontrar um usuário correspondente às credenciais informadas.");
            return View(inputModel);
        }

        public async Task<IActionResult> SignOut()
        {
            await _userServices.SignOutAsync();
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
