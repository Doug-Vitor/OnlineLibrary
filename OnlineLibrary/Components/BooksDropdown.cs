using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OnlineLibrary.Components
{
    public class BooksDropdown : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
