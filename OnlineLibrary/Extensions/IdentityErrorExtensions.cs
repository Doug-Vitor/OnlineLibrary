using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace OnlineLibrary.Extensions
{
    public static class IdentityErrorExtensions
    {
        private readonly static Dictionary<string, string> ErrorsCodeSafeToShareWithTranslation = new()
        {
            { "DuplicateUserName", "Esse nome de usuário já existe" },
            { "InvalidUserName", "O nome de usuário informado é inválido." },
            { "PasswordRequiresDigit", "A senha deve conter pelo menos um número." },
            { "PasswordRequiresLower", "A senha deve conter pelo menos uma letra minúscula." },
            { "PasswordRequiresNonAlphanumeric", "A senha deve conter pelo menos um símbolo especial." },
            { "PasswordRequiresUpper", "A senha deve conter pelo menos uma letra maiúscula." },
            { "PasswordTooShort", "A senha deve conter pelo menos 6 caracteres." },
        };

        public static bool ErrorIsSafeToShare(IdentityError error)
        {
            if (ErrorsCodeSafeToShareWithTranslation.Keys.Contains(error.Code))
                return true;

            return false;
        }

        public static string TranslateErrorDescription(string errorCode)
        {
            return ErrorsCodeSafeToShareWithTranslation.GetValueOrDefault(errorCode);
        }
    }
}
