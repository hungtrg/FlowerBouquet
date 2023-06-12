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
        public string? Username { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public LoginModel(ICustomerRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public void OnGet()
        {
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
            var account = _repo.CheckLogin(Username, Password);
            if (Username == adminEmail && Password == adminPassword)
            {
                HttpContext.Session.SetString("ROLE", "ADMIN");
                return RedirectToPage("/Index");
            }
            else if (account != null)
            {
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
