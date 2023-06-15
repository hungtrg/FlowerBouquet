using BusinessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlowerBouquetManagement.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ICustomerRepository _repo;

        public RegisterModel(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public IActionResult OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("ROLE")))
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        [BindProperty]
        public DataLayer.Models.Customer Customer { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            var customer = new DataLayer.Models.Customer
            {
                CustomerName = Customer.CustomerName,
                Email = Customer.Email,
                City = Customer.City,
                Country = Customer.Country,
                Password = Customer.Password,
                Bithday = Customer.Bithday
            };

            _repo.AddCustomer(customer);

            return RedirectToPage("../Index");
        }
    }
}
