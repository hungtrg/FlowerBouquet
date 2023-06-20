using BusinessLayer.Repository;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlowerBouquetManagement.Pages.Flower
{
    public class IndexModel : PageModel
    {
        private readonly IFlowerBouquetRepository _repo;

        public IndexModel(IFlowerBouquetRepository repo)
        {
            _repo = repo;
        }

        public IList<FlowerBouquet> FlowerBouquet { get; set; } = default!;

        public string search;
        public string role;

        public async Task OnGetAsync(string search)
        {
            role = HttpContext.Session.GetString("ROLE");
            if (!string.IsNullOrEmpty(search))
            {
                FlowerBouquet = _repo.Search(search).ToList();
            }
            else
            {
                FlowerBouquet = _repo.GetAll().ToList();
            }
        }
    }
}
