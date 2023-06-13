using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessLayer.Repository;

namespace FlowerBouquetManagement.Pages.Order
{
    public class EditModel : PageModel
    {
        private readonly IOrderRepository _repo;

        public EditModel(IOrderRepository repo)
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
            Order = order;
            ViewData["CustomerId"] = new SelectList(_repo.GetAll(), "CustomerId", "CustomerId");
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

            _repo.UpdateOrder(Order);

            return RedirectToPage("./Index");
        }
    }
}
