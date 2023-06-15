using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLayer.Repository;

namespace FlowerBouquetManagement.Pages.Customer
{
    public class EditModel : PageModel
    {
        private readonly ICustomerRepository _repo;

        public EditModel(ICustomerRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public DataLayer.Models.Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (string.IsNullOrEmpty(role) || role != "ADMIN")
            {
                return RedirectToPage("/Error");
            }

            if (id == null || _repo.GetAll == null)
            {
                return NotFound();
            }

            var customer = _repo.Get((int)id);
            if (customer == null)
            {
                return NotFound();
            }
            Customer = customer;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _repo.UpdateCustomer(Customer);

            return RedirectToPage("./Index");
        }
    }
}
