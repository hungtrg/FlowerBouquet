using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataLayer.Models;
using BusinessLayer.Repository;

namespace FlowerBouquetManagement.Pages.Flower
{
    public class DetailsModel : PageModel
    {
        private readonly IFlowerBouquetRepository _repo;

        public DetailsModel(IFlowerBouquetRepository repo)
        {
            _repo = repo;
        }

        public FlowerBouquet FlowerBouquet { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _repo.GetAll == null)
            {
                return NotFound();
            }

            var flower = _repo.Get((int)id);
            if (flower == null)
            {
                return NotFound();
            }
            else
            {
                FlowerBouquet = flower;
            }
            return Page();
        }
    }
}
