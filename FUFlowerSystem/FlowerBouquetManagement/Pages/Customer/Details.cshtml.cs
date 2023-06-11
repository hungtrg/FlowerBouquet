using BusinessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlowerBouquetManagement.Pages.Customer
{
    public class DetailsModel : PageModel
    {
        private readonly ICustomerRepository _repo;

        public DetailsModel(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public DataLayer.Models.Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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
    }
}
