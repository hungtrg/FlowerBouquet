using BusinessLayer.DAO;
using DataLayer.Models;

namespace BusinessLayer.Repository
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> GetAll();
        OrderDetail Get(int oId, int fId);
        bool AddOrderDetail(OrderDetail order);
        bool UpdateOrderDetail(OrderDetail order);
        bool RemoveOrderDetail(OrderDetail order);
    }

    public class OrderDetailRepository : IOrderDetailRepository
    {
        public bool AddOrderDetail(OrderDetail order) => OrderDetailDAO.Instance.AddOrderDetail(order);

        public OrderDetail Get(int oId, int fId) => OrderDetailDAO.Instance.Get(oId,fId);

        public IEnumerable<OrderDetail> GetAll() => OrderDetailDAO.Instance.GetAll();

        public bool RemoveOrderDetail(OrderDetail order) => OrderDetailDAO.Instance.RemoveOrderDetail(order);

        public bool UpdateOrderDetail(OrderDetail order) => OrderDetailDAO.Instance.UpdateOrderDetail(order);
    }
}
