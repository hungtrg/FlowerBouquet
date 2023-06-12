using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlowerBouquetManagement.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public RedirectToPageResult OnGet()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("../Index");
        }
    }
}
