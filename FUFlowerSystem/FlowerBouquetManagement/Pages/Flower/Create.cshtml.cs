using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataLayer.Models;
using BusinessLayer.Repository;

namespace FlowerBouquetManagement.Pages.Flower
{
    public class CreateModel : PageModel
    {
        private readonly IFlowerBouquetRepository _repo;

        public CreateModel(IFlowerBouquetRepository repo)
        {
            _repo = repo;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryId"] = new SelectList(_repo.GetCategories().ToList(), "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(_repo.GetSuppliers().ToList(), "SupplierId", "SupplierName");
            return Page();
        }

        [BindProperty]
        public FlowerBouquet FlowerBouquet { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var flower = new FlowerBouquet
            {
                CategoryId = FlowerBouquet.CategoryId,
                FlowerBouquetName = FlowerBouquet.FlowerBouquetName,
                Description = FlowerBouquet.Description,
                Price = FlowerBouquet.Price,
                UnitsInStock = FlowerBouquet.UnitsInStock,
                SupplierId = FlowerBouquet.SupplierId,
                Status = FlowerBouquet.Status
            };

            _repo.AddFlowerBouquet(flower);

            return RedirectToPage("./Index");
        }
    }
}
