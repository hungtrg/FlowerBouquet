using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLayer.Repository;

namespace FlowerBouquetManagement.Pages.Order
{
    public class DetailsModel : PageModel
    {
        private readonly IOrderRepository _repo;

        public DetailsModel(IOrderRepository repo)
        {
            _repo = repo;
        }

        public DataLayer.Models.Order Order { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _repo.GetAll == null)
            {
                return NotFound();
            }

            var order = _repo.Get((int)id);

            if (order == null)
            {
                return NotFound();
            }
            else 
            {
                Order = order;
            }
            return Page();
        }
    }
}
