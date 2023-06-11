using BusinessLayer.DAO;
using DataLayer.Models;
namespace BusinessLayer.Repository
{
    public interface IFlowerBouquetRepository
    {
        IEnumerable<FlowerBouquet> GetAll();
        FlowerBouquet Get(int id);
        IEnumerable<FlowerBouquet> Search(string search);
        bool AddFlowerBouquet(FlowerBouquet flower);
        bool UpdateFlowerBouquet(FlowerBouquet flower);
        bool RemoveFlowerBouquet(int id);
    }

    public class FlowerBouquetRepository : IFlowerBouquetRepository
    {
        public bool AddFlowerBouquet(FlowerBouquet flower) => FlowerBouquetDAO.Instance.AddFlowerBouquet(flower);

        public FlowerBouquet Get(int id) => FlowerBouquetDAO.Instance.Get(id);

        public IEnumerable<FlowerBouquet> GetAll() => FlowerBouquetDAO.Instance.GetAll();

        public bool RemoveFlowerBouquet(int id) => FlowerBouquetDAO.Instance.RemoveFlowerBouquet(id);

        public IEnumerable<FlowerBouquet> Search(string search) => FlowerBouquetDAO.Instance.Search(search);

        public bool UpdateFlowerBouquet(FlowerBouquet flower) => FlowerBouquetDAO.Instance.UpdateFlowerBouquet(flower);
    }
}
