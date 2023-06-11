using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.DAO
{
    public class FlowerBouquetDAO
    {
        private static FlowerBouquetDAO? instance;
        private static readonly object instanceLock = new object();
        private readonly FuflowerSystemDbContextContext _context = new FuflowerSystemDbContextContext();

        public static FlowerBouquetDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new FlowerBouquetDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<FlowerBouquet> GetAll()
        {
            List<FlowerBouquet> list;
            try
            {
                list = _context.FlowerBouquets.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public FlowerBouquet Get(int id)
        {
            FlowerBouquet? flower;
            try
            {
                flower = _context.FlowerBouquets.FirstOrDefault(f => f.FlowerBouquetId.Equals(id));
            }
            catch (Exception)
            {
                throw;
            }
            return flower;
        }
        public IEnumerable<FlowerBouquet> Search(string search)
        {
            List<FlowerBouquet> list;
            try
            {
                list = _context.FlowerBouquets.Where(f => f.FlowerBouquetName.Contains(search)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }
        public bool AddFlowerBouquet(FlowerBouquet flower)
        {
            bool result = false;
            try
            {
                FlowerBouquet f = Get(flower.FlowerBouquetId);
                if (f == null)
                {
                    _context.FlowerBouquets.Add(flower);
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

        public bool UpdateFlowerBouquet(FlowerBouquet flower)
        {
            bool result = false;
            try
            {
                FlowerBouquet f = Get(flower.FlowerBouquetId);
                if (f != null)
                {
                    _context.Entry<FlowerBouquet>(flower).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

        public bool RemoveFlowerBouquet(int id)
        {
            bool result = false;
            try
            {
                FlowerBouquet f = Get(id);
                if (f != null)
                {
                    var associatedOrders = _context.Orders.Where(o => o.FlowerBouquetId == id).ToList();
                    if (associatedOrders.Any())
                    {
                        f.Status = false;
                        _context.Entry(f).State = EntityState.Modified;
                    }
                    else
                    {
                        _context.Remove(f);
                    }
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
