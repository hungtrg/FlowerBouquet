using BusinessLayer.Repository;
using BusinessLayer.UtilExtensions;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlowerBouquetManagement.Pages.Cart
{
    public class CartModel : PageModel
    {
        private readonly IFlowerBouquetRepository _repo;
        private readonly ICartService _service;

        public CartModel(IFlowerBouquetRepository repo, ICartService service)
        {
            _repo = repo;
            _service = service;
        }

        [BindProperty]
        public ProductInput Input { get; set; }
        public int? Stock { get; set; }
        public IList<string> FlowerName { get; set; }
        public IList<OrderDetail> Cart { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("ROLE")))
            {
                return RedirectToPage("../Account/Login");
            }
            List<string> flowerName = new List<string>();
            var cart = _service.GetCart();
            for (int i = 0; i < cart.Count; i++)
            {
                var item = _repo.Get(cart[i].FlowerBouquetId);
                flowerName.Add(item.FlowerBouquetName);
            }
            Cart = cart;
            FlowerName = flowerName;
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {
            Stock = _repo.Get((int)id).UnitsInStock;
            if (Input.Quantity > Stock)
            {
                TempData["ErrorMessage"] = "This quantity plus the quantity in your cart exceeds stock!";
                return RedirectToPage();
            }
            _service.UpdateCartItemQuantity(id, Input.Quantity);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            _service.RemoveCartItem(id);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostClearAsync()
        {
            _service.ClearCart();
            return RedirectToPage();
        }
    }
}
