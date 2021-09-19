﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Extensions;
using OnlineLibrary.Models.ViewModels;
using OnlineLibrary.Services.Interfaces;
using System.Threading.Tasks;

namespace OnlineLibrary.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountServices(UserManager<IdentityUser> userManager,  
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [ValidateAntiForgeryToken]
        public async Task<dynamic> SignUpAsync(UserInputViewModel inputModel)
        {
            IdentityUser user = new(inputModel.UserName);

            var result = await _userManager.CreateAsync(user, inputModel.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Default");
                await _signInManager.SignInAsync(user, false);
                return true;
            }

            return result.Errors;
        }

        [ValidateAntiForgeryToken]
        public async Task<bool> SignInAsync(UserInputViewModel inputModel)
        {
            var user = await _userManager.FindByNameAsync(inputModel.UserName);
            if (user is null)
                return false;

            var result = await _signInManager.PasswordSignInAsync(user, inputModel.Password, false, false);
            if (result.Succeeded)
                return true;

            return false;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public string GetErrorMessages(IdentityError error)
        {
            if (IdentityErrorExtensions.ErrorIsSafeToShare(error))
                return IdentityErrorExtensions.TranslateErrorDescription(error.Code);

            return "Ocorreu um erro desconhecido.";
        }
    }
}
