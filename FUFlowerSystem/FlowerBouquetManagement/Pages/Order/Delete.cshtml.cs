using BusinessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlowerBouquetManagement.Pages.Order
{
    public class DeleteModel : PageModel
    {
        private readonly IOrderRepository _repo;

        public DeleteModel(IOrderRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _repo.GetAll == null)
            {
                return NotFound();
            }
            var order = _repo.Get((int)id);

            if (order != null)
            {
                Order = order;
                _repo.RemoveOrder((int)id);
            }

            return RedirectToPage("./Index");
        }
    }
}
