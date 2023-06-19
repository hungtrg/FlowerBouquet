using BusinessLayer.Repository;
using BusinessLayer.UtilExtensions;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace FlowerBouquetManagement.Pages.Cart
{
    public class CartModel : PageModel
    {
        private readonly IFlowerBouquetRepository _repo;

        public CartModel(IFlowerBouquetRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public ProductInput Input { get; set; }
        public int? Stock { get; set; }
        public IList<string> FlowerName { get; set; }
        public IList<OrderDetail> Cart { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var cartJson = UtilExtensions.GetObjectFromJson<List<OrderDetail>>(HttpContext.Session, "CART");
            List<string> flowerName = new List<string>();
            if (cartJson.IsNullOrEmpty())
            {
                // Cart is empty
                Cart = new List<OrderDetail>();
            }
            else
            {
                try
                {
                    for (int i = 0; i < cartJson.Count; i++)
                    {
                        var item = _repo.Get(cartJson[i].FlowerBouquetId);
                        flowerName.Add(item.FlowerBouquetName);
                    }
                    Cart = cartJson;
                    FlowerName = flowerName;
                }
                catch (JsonException ex)
                {
                    // Error deserializing cart from session state
                    Cart = new List<OrderDetail>();
                    // Log or handle the exception as appropriate
                }
            }
        }

        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {
            var cartJson = UtilExtensions.GetObjectFromJson<List<OrderDetail>>(HttpContext.Session, "CART");
            var checkCart = GetCartItemIndex(cartJson, id);
            Stock = _repo.Get((int)id).UnitsInStock;
            if (Input.Quantity > Stock)
            {
                TempData["ErrorMessage"] = "This quantity plus the quantity in your cart exceeds stock!";
                return RedirectToPage();
            }
            if (!checkCart.Equals((-1)))
            {
                cartJson[checkCart].Quantity = Input.Quantity;
            }
            UtilExtensions.SetObjectAsJson(HttpContext.Session, "CART", cartJson);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var cartJson = UtilExtensions.GetObjectFromJson<List<OrderDetail>>(HttpContext.Session, "CART");
            var checkCart = GetCartItemIndex(cartJson, id);
            cartJson.Remove(cartJson[checkCart]);
            UtilExtensions.SetObjectAsJson(HttpContext.Session, "CART", cartJson);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostClearAsync()
        {
            HttpContext.Session.Remove("CART");
            return RedirectToPage();
        }

        // Determine if the selected item is in the user's cart or not
        // Return the item's index in the cart
        private int GetCartItemIndex(List<OrderDetail> cart, int id)
        {
            if (cart != null)
            {
                for (int i = 0; i < cart.Count; i++)
                {
                    if (cart[i].FlowerBouquetId.Equals(id))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
    }
}
