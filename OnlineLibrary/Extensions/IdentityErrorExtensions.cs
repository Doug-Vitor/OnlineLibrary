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

        private static bool ErrorIsSafeToShare(string errorCode)
        {
            if (ErrorsCodeSafeToShareWithTranslation.Keys.Contains(errorCode))
                return true;

            return false;
        }

        public static string TranslatedErrorDescription(IdentityError error)
        {
            if (ErrorIsSafeToShare(error.Code))
                return ErrorsCodeSafeToShareWithTranslation.GetValueOrDefault(error.Code);

            return "Ocorreu um erro desconhecido.";
        }
    }
}
