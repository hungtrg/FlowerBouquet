using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.DAO
{
    public class OrderDAO
    {
        private static OrderDAO? instance;
        private static readonly object instanceLock = new object();
        private readonly FuflowerSystemDbContextContext _context = new FuflowerSystemDbContextContext();

        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Order> GetAll()
        {
            List<Order> list;
            try
            {
                list = _context.Orders.Include(o => o.Customer).Include(o => o.OrderDetails).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public IEnumerable<Order> Search(string search)
        {
            List<Order> list;
            try
            {
                list = _context.Orders.Include(o => o.Customer).Include(o => o.OrderDetails)
                    .Where(o => o.Customer.CustomerName.Contains(search)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public Order Get(int id)
        {
            Order? order;
            try
            {
                order = _context.Orders.FirstOrDefault(o => o.OrderId.Equals(id));
            }
            catch (Exception)
            {
                throw;
            }
            return order;
        }

        public bool AddOrder(Customer customer, List<OrderDetail> cart)
        {
            bool result = false;
            try
            {
                var order = new Order
                {
                    CustomerId = customer.CustomerId,
                    OrderDetails = cart,
                    OrderDate = DateTime.Now,
                    DeliveryDate = DateTime.Now.AddDays(3),
                    Freight = GetShippingFee(customer.City),
                    Status = 0,
                    Total = GetTotalFee(GetShippingFee(customer.City), cart)
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool UpdateOrder(Order order)
        {
            bool result = false;
            try
            {
                Order o = Get(order.OrderId);
                if (o != null)
                {
                    _context.Entry<Order>(o).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                    _context.Update(order);
                    _context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool RemoveOrder(int id)
        {
            bool result = false;
            try
            {
                Order o = Get(id);
                if (o != null)
                {
                    _context.Remove(o);
                    _context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public decimal GetShippingFee(string deliveryTo)
        {
            const decimal intercityDeliveryFee = 30.0m;
            const decimal intracityDeliveryFee = 20.0m;

            if (string.IsNullOrWhiteSpace(deliveryTo))
            {
                return 0.0m;
            }

            string deliveryToUpperCase = deliveryTo.ToUpper().Replace(" ", "");
            if (deliveryToUpperCase.Contains("HOCHIMINH"))
            {
                return intracityDeliveryFee;
            }

            return intercityDeliveryFee;
        }

        public decimal GetTotalFee(decimal shippingFee, List<OrderDetail> cart)
        {
            decimal? total = decimal.Zero;

            foreach (var item in cart)
            {
                decimal discount = item.Discount ?? 0m; // Use 0 if Discount is null
                decimal? itemTotal = (item.Price - discount) * item.Quantity;
                total += itemTotal;
            }

            total += shippingFee;
            return decimal.Round(total ?? 0m, 2);
        }
    }
}
