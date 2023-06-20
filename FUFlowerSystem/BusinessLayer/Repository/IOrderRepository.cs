using BusinessLayer.DAO;
using DataLayer.Models;

namespace BusinessLayer.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        IEnumerable<Order> Search(string search);
        Order Get(int id);
        bool AddOrder(Customer customer, List<OrderDetail> cart);
        bool UpdateOrder(Order order);
        bool RemoveOrder(int id);
        decimal GetShippingFee(string deliveryTo);
        decimal GetTotalFee(decimal shippingFee, List<OrderDetail> cart);
    }

    public class OrderRepository : IOrderRepository
    {
        public bool AddOrder(Customer customer, List<OrderDetail> cart) => OrderDAO.Instance.AddOrder(customer, cart);

        public Order Get(int id) => OrderDAO.Instance.Get(id);

        public IEnumerable<Order> GetAll() => OrderDAO.Instance.GetAll();

        public decimal GetShippingFee(string deliveryTo) => OrderDAO.Instance.GetShippingFee(deliveryTo);

        public decimal GetTotalFee(decimal shippingFee, List<OrderDetail> cart) => OrderDAO.Instance.GetTotalFee(shippingFee, cart);

        public bool RemoveOrder(int id) => OrderDAO.Instance.RemoveOrder(id);

        public IEnumerable<Order> Search(string search) => OrderDAO.Instance.Search(search);

        public bool UpdateOrder(Order order) => OrderDAO.Instance.UpdateOrder(order);
    }
}
