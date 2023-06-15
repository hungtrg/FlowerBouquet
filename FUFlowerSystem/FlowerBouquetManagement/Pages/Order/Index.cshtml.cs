using BusinessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlowerBouquetManagement.Pages.Order
{
    public class IndexModel : PageModel
    {
        private readonly IOrderRepository _repo;

        public IndexModel(IOrderRepository repo)
        {
            _repo = repo;
        }

        public string search;

        public IList<DataLayer.Models.Order> Order { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync(string search)
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (string.IsNullOrEmpty(role) || role != "ADMIN")
            {
                return RedirectToPage("/Error");
            }

            if (search != null)
            {
                Order = _repo.Search(search).ToList();
            }
            else
            {
                Order = _repo.GetAll().ToList();
            }

            return Page();
        }
    }
}
