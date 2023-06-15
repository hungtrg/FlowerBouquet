using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataLayer.Models;
using BusinessLayer.Repository;

namespace FlowerBouquetManagement.Pages.Flower
{
    public class DeleteModel : PageModel
    {
        private readonly IFlowerBouquetRepository _repo;

        public DeleteModel(IFlowerBouquetRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public FlowerBouquet FlowerBouquet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (string.IsNullOrEmpty(role) || role != "ADMIN")
            {
                return RedirectToPage("/Error");
            }

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _repo.GetAll == null)
            {
                return NotFound();
            }

            var flower = _repo.Get((int)id);

            if (flower != null)
            {
                FlowerBouquet = flower;
                _repo.RemoveFlowerBouquet((int)id);
            }


            return RedirectToPage("./Index");
        }
    }
}
