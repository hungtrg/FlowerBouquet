using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BusinessLayer.UtilExtensions
{
    public interface ICartService
    {
        List<OrderDetail> GetCart();
        void SaveCart(List<OrderDetail> cart);
        void AddToCart(FlowerBouquet flower, int quantity);
        void UpdateCartItemQuantity(int id, int quantity);
        void RemoveCartItem(int id);
        void ClearCart();
    }

    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ISession Session => _httpContextAccessor.HttpContext.Session;

        private string GetCartKey()
        {
            string userId;
            if (Session.TryGetValue("USERID", out byte[] bytes))
            {
                userId = Encoding.UTF8.GetString(bytes);
            }
            else
            {
                userId = null;
            }
            return $"cart_{Session.Id}_{userId}";
        }

        public List<OrderDetail> GetCart()
        {
            return Session.GetObjectFromJson<List<OrderDetail>>(GetCartKey()) ?? new List<OrderDetail>();
        }

        public void SaveCart(List<OrderDetail> cart)
        {
            Session.SetObjectAsJson(GetCartKey(), cart);
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

        public void AddToCart(FlowerBouquet flower, int quantity)
        {
            var cart = GetCart();
            // Determine if are there any item in the user's cart or not
            if (cart.IsNullOrEmpty())
            {
                cart = new List<OrderDetail>
                {
                    new OrderDetail
                    {
                        FlowerBouquetId = flower.FlowerBouquetId,
                        Price = flower.Price,
                        Quantity = quantity,
                        Discount = 0
                    }
                };
                SaveCart(cart);
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
                        Quantity = quantity,
                        Discount = 0,
                    });
                }
                else
                {
                    cart[checkCart].Quantity += quantity;
                }
                SaveCart(cart);
            }
        }

        public void UpdateCartItemQuantity(int id, int quantity)
        {
            var cart = GetCart();
            var checkCart = GetCartItemIndex(cart, id);
            if (!checkCart.Equals((-1)))
            {
                cart[checkCart].Quantity = quantity;
            }
            SaveCart(cart);
        }

        public void RemoveCartItem(int id)
        {
            var cart = GetCart();
            var checkCart = GetCartItemIndex(cart, id);
            cart.Remove(cart[checkCart]);
            SaveCart(cart);
        }

        public void ClearCart()
        {
            Session.Remove(GetCartKey());
        }
    }
}
