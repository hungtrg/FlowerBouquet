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
        private readonly ICartService _service;

        public CheckoutModel(IFlowerBouquetRepository flowerRepo, ICustomerRepository customerRepo, ICartService service)
        {
            _flowerRepo = flowerRepo;
            _customerRepo = customerRepo;
            _service = service;
        }

        [BindProperty]
        public DataLayer.Models.Customer Customer { get; set; } = default!;
        public decimal? Freight { get; set; }
        public IList<string> FlowerName { get; set; }
        public IList<OrderDetail> Cart { get; set; } = default!;
        public async Task OnGetAsync()
        {
            int? id = HttpContext.Session.GetInt32("USERID");
            var customer = _customerRepo.Get((int)id);
            Freight = 30;
            Customer = customer;
            List<string> flowerName = new List<string>();
            var cart = _service.GetCart();
            for (int i = 0; i < cart.Count; i++)
            {
                var item = _flowerRepo.Get(cart[i].FlowerBouquetId);
                flowerName.Add(item.FlowerBouquetName);
            }
            Cart = cart;
            FlowerName = flowerName;
        }
    }
}
