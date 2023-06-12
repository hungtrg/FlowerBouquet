using Microsoft.AspNetCore.Mvc.RazorPages;
using DataLayer.Models;
using BusinessLayer.Repository;

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

        public async Task OnGetAsync(string search)
        {
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
