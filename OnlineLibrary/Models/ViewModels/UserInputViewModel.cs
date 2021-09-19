using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Models.ViewModels
{
    public class UserInputViewModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
