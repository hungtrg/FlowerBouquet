using Microsoft.AspNetCore.Mvc.RazorPages;
using DataLayer.Models;
using BusinessLayer.Repository;

namespace FlowerBouquetManagement.Pages.Order
{
    public class DetailsModel : PageModel
    {
        private readonly IOrderDetailRepository _repo;

        public DetailsModel(IOrderDetailRepository repo)
        {
            _repo = repo;
        }

        public IList<OrderDetail> OrderDetail { get;set; } = default!;

        public async Task OnGetAsync(int id)
        {
            if (id == null || OrderDetail == null)
            {
                //return NotFound();
            }
            OrderDetail = _repo.GetAll(id).ToList();
        }
    }
}
