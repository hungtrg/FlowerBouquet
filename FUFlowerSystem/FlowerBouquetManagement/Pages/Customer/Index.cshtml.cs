using BusinessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlowerBouquetManagement.Pages.Customer
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerRepository _repo;

        public string search { get; set; }

        public IndexModel(ICustomerRepository repo) => _repo = repo;

        public IList<DataLayer.Models.Customer> Customer { get; set; } = default!;

        public IActionResult OnGet(string search)
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (string.IsNullOrEmpty(role) || role != "ADMIN")
            {
                return RedirectToPage("/Error");
            }
            if (!string.IsNullOrEmpty(search))
            {
                Customer = _repo.Search(search).ToList();
            }
            else
            {
                Customer = _repo.GetAll().ToList();
            }

            return Page();
        }
    }
}
