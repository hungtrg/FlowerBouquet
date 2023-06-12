using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataLayer.Models;
using BusinessLayer.Repository;

namespace FlowerBouquetManagement.Pages.Flower
{
    public class EditModel : PageModel
    {
        private readonly IFlowerBouquetRepository _repo;

        public EditModel(IFlowerBouquetRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public FlowerBouquet FlowerBouquet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _repo.GetAll == null)
            {
                return NotFound();
            }

            var flowerbouquet = _repo.Get((int)id);
            if (flowerbouquet == null)
            {
                return NotFound();
            }
            FlowerBouquet = flowerbouquet;
            ViewData["CategoryId"] = new SelectList(_repo.GetCategories().ToList(), "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(_repo.GetSuppliers().ToList(), "SupplierId", "SupplierName");
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

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _repo.UpdateFlowerBouquet(FlowerBouquet);

            return RedirectToPage("./Index");
        }
    }
}
