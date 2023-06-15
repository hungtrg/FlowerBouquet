using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlowerBouquetManagement.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public RedirectToPageResult OnGet()
        {
            HttpContext.Session.Remove("ROLE");
            HttpContext.Session.Remove("USERID");
            HttpContext.Session.Remove("FULLNAME");
            return RedirectToPage("../Index");
        }
    }
}
