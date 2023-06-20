using BusinessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlowerBouquetManagement.Pages.Order
{
    public class MyOrderModel : PageModel
    {
        private readonly IOrderRepository _repo;

        public MyOrderModel(IOrderRepository repo)
        {
            _repo = repo;
        }

        public int Filter { get; set; }

        public IList<DataLayer.Models.Order> Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? filter)
        {
            int? userId = HttpContext.Session.GetInt32("USERID");
            if (userId == null)
            {
                return RedirectToPage("/Error");
            }

            if (filter == null)
            {
                Order = _repo.GetAll().Where(o => o.CustomerId.Equals(userId)).ToList();
            }
            else
            {
                Order = _repo.GetAll().Where(o => o.CustomerId.Equals(userId) && o.Status.Equals(filter)).ToList();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync(int id, int status)
        {
            var order = _repo.Get(id);
            order.Status = status;
            _repo.UpdateOrder(order);
            return RedirectToPage();
        }
    }
}
