using BusinessLayer.Repository;
using BusinessLayer.UtilExtensions;
using DataLayer.Models;
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
    }
}
