using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessLayer.Repository;

namespace FlowerBouquetManagement.Pages.Order
{
    public class CreateModel : PageModel
    {
        private readonly IOrderRepository _repo;

        public CreateModel(IOrderRepository repo)
        {
            _repo = repo;
        }

        public IActionResult OnGet()
        {
        ViewData["CustomerId"] = new SelectList(_repo.GetAll(), "CustomerId", "CustomerId");
            return Page();
        }

        [BindProperty]
        public DataLayer.Models.Order Order { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _repo.GetAll() == null || Order == null)
            {
                return Page();
            }

            var order = new DataLayer.Models.Order
            {
                CustomerId = Order.CustomerId,
                OrderDate = Order.OrderDate,
                DeliveryDate = Order.DeliveryDate,
                Freight = Order.Freight,
                Total = Order.Total,
                Status = Order.Status
            };
            _repo.AddOrder(order);

            return RedirectToPage("./Index");
        }
    }
}
