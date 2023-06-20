using Microsoft.AspNetCore.Mvc.RazorPages;
using DataLayer.Models;
using BusinessLayer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FlowerBouquetManagement.Pages.Order
{
    public class DetailsModel : PageModel
    {
        private readonly IOrderDetailRepository _repo;

        public DetailsModel(IOrderDetailRepository repo)
        {
            _repo = repo;
        }

        public IList<OrderDetail> OrderDetail { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (string.IsNullOrEmpty(role))
            {
                return RedirectToPage("/Error");
            }

            if (id == null)
            {
                return NotFound();
            }
            OrderDetail = _repo.GetAll(id).ToList();

            return Page();
        }
    }
}
