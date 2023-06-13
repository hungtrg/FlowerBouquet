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

        public bool AddOrder(Order order)
        {
            bool result = false;
            try
            {
                Order o = Get(order.OrderId);
                if (o == null)
                {
                    _context.Orders.Add(order);
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
    }
}
