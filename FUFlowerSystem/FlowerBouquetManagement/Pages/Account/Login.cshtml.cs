using BusinessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FlowerBouquetManagement.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ICustomerRepository _repo;
        private readonly IConfiguration _config;


        [BindProperty]
        [Required]
        public string? email { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string? password { get; set; }

        public LoginModel(ICustomerRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public IActionResult OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("ROLE")))
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Perform login validation here
            var adminEmail = _config.GetSection("AdminAccount:AdminEmail").Value;
            var adminPassword = _config.GetSection("AdminAccount:AdminPassword").Value;
            var account = _repo.CheckLogin(email, password);
            if (email == adminEmail && password == adminPassword)
            {
                HttpContext.Session.SetString("ROLE", "ADMIN");
                return RedirectToPage("/Index");
            }
            else if (account != null)
            {
                HttpContext.Session.SetString("ROLE", "USER");
                HttpContext.Session.SetInt32("USERID", account.CustomerId);
                HttpContext.Session.SetString("FULLNAME", account.CustomerName);
                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return Page();
            }
        }
    }
}
