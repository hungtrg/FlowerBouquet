using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.DAO
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO? instance;
        private static readonly object instanceLock = new object();
        private readonly FuflowerSystemDbContextContext _context = new FuflowerSystemDbContextContext();

        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<OrderDetail> GetAll(int id)
        {
            List<OrderDetail> list;
            try
            {
                list = _context.OrderDetails.Where(od => od.OrderId.Equals(id)).Include(od => od.Order).Include(od => od.FlowerBouquet).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public OrderDetail Get(int oId, int fId)
        {
            OrderDetail? orderDetail;
            try
            {
                orderDetail = _context.OrderDetails.FirstOrDefault(od => od.OrderId.Equals(oId) && od.FlowerBouquetId.Equals(fId));
            }
            catch (Exception)
            {
                throw;
            }
            return orderDetail;
        }

        public bool AddOrderDetail(OrderDetail orderDetail)
        {
            bool result = false;
            try
            {
                OrderDetail od = Get(orderDetail.OrderId, orderDetail.FlowerBouquetId);
                var checkStatus = od.FlowerBouquet.Status;
                if (od == null && checkStatus == true)
                {
                    _context.OrderDetails.Add(orderDetail);
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

        public bool UpdateOrderDetail(OrderDetail orderDetail)
        {
            bool result = false;
            try
            {
                OrderDetail od = Get(orderDetail.OrderId, orderDetail.FlowerBouquetId);
                if (od != null)
                {
                    _context.Entry<OrderDetail>(orderDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

        public bool RemoveOrderDetail(OrderDetail orderDetail)
        {
            bool result = false;
            try
            {
                OrderDetail od = Get(orderDetail.OrderId, orderDetail.FlowerBouquetId);
                if (od != null)
                {
                    _context.Remove(od);
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
