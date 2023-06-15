using BusinessLayer.Repository;
using BusinessLayer.UtilExtensions;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace FlowerBouquetManagement.Pages.Flower
{
    public class DetailsModel : PageModel
    {
        private readonly IFlowerBouquetRepository _repo;

        public DetailsModel(IFlowerBouquetRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public ProductInput Input { get; set; }
        public int? Stock { get; set; }
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            int? inCartQuantity = 0;
            var flower = _repo.Get((int)id);
            var cart = UtilExtensions.GetObjectFromJson<List<OrderDetail>>(HttpContext.Session, "CART");
            Stock = flower.UnitsInStock;
            
            if (!cart.IsNullOrEmpty())
            {
                var cartFlower = cart.FirstOrDefault(c => c.FlowerBouquetId.Equals(flower.FlowerBouquetId));
                if (cartFlower != null)
                {
                    inCartQuantity = cartFlower.Quanity;
                }
            }
            if (Input.Quantity + inCartQuantity > Stock)
            {
                ViewData["ErrorMessage"] = "This quantity plus the quantity in your cart exceeds stock!";
                FlowerBouquet = flower;
                return Page();
            }

            // Determine if are there any item in the user's cart or not
            if (cart.IsNullOrEmpty())
            {
                cart = new List<OrderDetail>();
                cart.Add(new OrderDetail
                {
                    FlowerBouquetId = flower.FlowerBouquetId,
                    Price = flower.Price,
                    Quanity = Input.Quantity,
                    Discount = 0
                });
                UtilExtensions.SetObjectAsJson(HttpContext.Session, "CART", cart);
            }
            else
            {
                var checkCart = GetCartItemIndex(cart, flower.FlowerBouquetId);
                if (checkCart.Equals(-1))
                {
                    cart.Add(new OrderDetail
                    {
                        FlowerBouquetId = flower.FlowerBouquetId,
                        Price = flower.Price,
                        Quanity = Input.Quantity,
                        Discount = 0,
                    });
                }
                else
                {
                    cart[checkCart].Quanity += Input.Quantity;
                }
                UtilExtensions.SetObjectAsJson(HttpContext.Session, "CART", cart);
            }
            return Redirect("../Cart/Cart");
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
