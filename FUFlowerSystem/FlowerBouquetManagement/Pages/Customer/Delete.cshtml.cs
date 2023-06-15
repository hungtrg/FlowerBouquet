using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLayer.Repository;

namespace FlowerBouquetManagement.Pages.Customer
{
    public class DeleteModel : PageModel
    {
        private readonly ICustomerRepository _repo;

        public DeleteModel(ICustomerRepository repo)
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
            else
            {
                Customer = customer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _repo.GetAll == null)
            {
                return NotFound();
            }

            var customer = _repo.Get((int)id);

            if (customer != null)
            {
                Customer = customer;
                _repo.RemoveCustomer((int)id);
            }

            return RedirectToPage("./Index");
        }
    }
}
