using BusinessLayer.Repository;
using BusinessLayer.UtilExtensions;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlowerBouquetManagement.Pages.Cart
{
    public class CheckoutModel : PageModel
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IFlowerBouquetRepository _flowerRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly ICartService _service;

        public CheckoutModel(IFlowerBouquetRepository flowerRepo,
            ICustomerRepository customerRepo, IOrderRepository orderRepo, ICartService service)
        {
            _flowerRepo = flowerRepo;
            _customerRepo = customerRepo;
            _orderRepo = orderRepo;
            _service = service;
        }

        [BindProperty]
        public DataLayer.Models.Customer Customer { get; set; } = default!;
        public IList<string> FlowerName { get; set; }
        public IList<OrderDetail> Cart { get; set; } = default!;
        public decimal? ShippingFee { get; set; }
        [BindProperty]
        public decimal? Total { get; set; }

        public async Task OnGetAsync()
        {
            int? id = HttpContext.Session.GetInt32("USERID");
            var customer = _customerRepo.Get((int)id);

            List<string> flowerName = new List<string>();
            var cart = _service.GetCart();

            for (int i = 0; i < cart.Count; i++)
            {
                var item = _flowerRepo.Get(cart[i].FlowerBouquetId);
                flowerName.Add(item.FlowerBouquetName);
            }
            Cart = cart;
            Customer = customer;
            FlowerName = flowerName;
            ShippingFee = _orderRepo.GetShippingFee(customer.City);
            Total = _orderRepo.GetTotalFee((decimal)ShippingFee, cart);
        }

        public async Task<IActionResult> OnPostOrderAsync()
        {
            int? id = HttpContext.Session.GetInt32("USERID");
            var customer = _customerRepo.Get((int)id);
            var cart = _service.GetCart();

            _orderRepo.AddOrder(customer, cart);
            _flowerRepo.UpdateFlowerStockQuantity(cart);
            _service.ClearCart();
            return RedirectToPage();
        }
    }
}
