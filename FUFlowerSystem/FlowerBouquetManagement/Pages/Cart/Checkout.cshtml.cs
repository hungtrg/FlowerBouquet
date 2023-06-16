using BusinessLayer.Repository;
using BusinessLayer.UtilExtensions;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace FlowerBouquetManagement.Pages.Cart
{
    public class CheckoutModel : PageModel
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IFlowerBouquetRepository _flowerRepo;

        public CheckoutModel(IFlowerBouquetRepository flowerRepo, ICustomerRepository customerRepo)
        {
            _flowerRepo = flowerRepo;
            _customerRepo = customerRepo;
        }

        [BindProperty]
        public DataLayer.Models.Customer Customer { get; set; } = default!;
        public decimal? Freight { get; set; }
        public IList<string> FlowerName { get; set; }
        public IList<OrderDetail> Cart { get; set; } = default!;
        public async Task OnGetAsync()
        {
            int id = (int)HttpContext.Session.GetInt32("USERID");
            var customer = _customerRepo.Get((int)id);
            Freight = 30;
            Customer = customer;
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
                        var item = _flowerRepo.Get(cartJson[i].FlowerBouquetId);
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
