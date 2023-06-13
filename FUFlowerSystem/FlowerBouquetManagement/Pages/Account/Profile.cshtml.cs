using BusinessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlowerBouquetManagement.Pages.Account
{
    public class ProfileModel : PageModel
    {
        private readonly ICustomerRepository _repo;

        public ProfileModel(ICustomerRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public DataLayer.Models.Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            int id = (int)HttpContext.Session.GetInt32("USERID");
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

            return RedirectToPage("../Index");
        }
    }
}
