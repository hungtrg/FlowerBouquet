using BusinessLayer.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlowerBouquetManagement.Pages.Order
{
    public class IndexModel : PageModel
    {
        private readonly IOrderRepository _repo;

        public IndexModel(IOrderRepository repo)
        {
            _repo = repo;
        }

        public string search;

        public IList<DataLayer.Models.Order> Order { get;set; } = default!;

        public async Task OnGetAsync(string search)
        {
            if (search != null)
            {
                Order = _repo.Search(search).ToList();
            }
            else
            {
                Order = _repo.GetAll().ToList();
            }
        }
    }
}
