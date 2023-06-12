using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLayer.Repository;

namespace FlowerBouquetManagement.Pages.Customer
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerRepository _repo;

        public CreateModel(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public IActionResult OnGet()
        {
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

            return RedirectToPage("./Index");
        }
    }
}
