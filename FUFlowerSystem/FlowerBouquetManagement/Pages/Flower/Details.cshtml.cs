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
        private readonly ICartService _service;

        public DetailsModel(IFlowerBouquetRepository repo, ICartService service)
        {
            _repo = repo;
            _service = service;
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
            Stock = flower.UnitsInStock;

            if (!_service.GetCart().IsNullOrEmpty())
            {
                var cartFlower = _service.GetCart().FirstOrDefault(c => c.FlowerBouquetId.Equals(flower.FlowerBouquetId));
                if (cartFlower != null)
                {
                    inCartQuantity = cartFlower.Quantity;
                }
            }
            if (Input.Quantity + inCartQuantity > Stock)
            {
                ViewData["ErrorMessage"] = "This quantity plus the quantity in your cart exceeds stock!";
                FlowerBouquet = flower;
                return Page();
            }

            _service.AddToCart(flower, Input.Quantity);
            return Redirect("../Cart/Cart");
        }
    }
}
