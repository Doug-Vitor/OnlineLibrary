using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Models.ViewModels
{
    public class UserInputViewModel
    {
        [DisplayName("Nome de usuário")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Senha")]
        public string Password { get; set; }
    }
}
