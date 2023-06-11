using BusinessLayer.DAO;
using DataLayer.Models;

namespace BusinessLayer.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Order Get(int id);
        bool AddOrder(Order order);
        bool UpdateOrder(Order order);
        bool RemoveOrder(int id);
    }

    public class OrderRepository : IOrderRepository
    {
        public bool AddOrder(Order order) => OrderDAO.Instance.AddOrder(order);

        public Order Get(int id) => OrderDAO.Instance.Get(id);

        public IEnumerable<Order> GetAll() => OrderDAO.Instance.GetAll();

        public bool RemoveOrder(int id) => OrderDAO.Instance.RemoveOrder(id);

        public bool UpdateOrder(Order order) => OrderDAO.Instance.UpdateOrder(order);
    }
}
