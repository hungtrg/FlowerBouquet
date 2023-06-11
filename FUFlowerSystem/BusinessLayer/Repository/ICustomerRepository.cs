using BusinessLayer.DAO;
using DataLayer.Models;

namespace BusinessLayer.Repository
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
        Customer Get(int id);
        IEnumerable<Customer> Search(string search);
        Customer CheckLogin(string email, string password);
        bool AddCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool RemoveCustomer(int id);
    }

    public class CustomerRepository : ICustomerRepository
    {
        public bool AddCustomer(Customer customer) => CustomerDAO.Instance.AddCustomer(customer);

        public Customer CheckLogin(string email, string password) => CustomerDAO.Instance.CheckLogin(email, password);

        public Customer Get(int id) => CustomerDAO.Instance.Get(id);

        public IEnumerable<Customer> GetAll() => CustomerDAO.Instance.GetAll();

        public bool RemoveCustomer(int id) => CustomerDAO.Instance.RemoveCustomer(id);

        public IEnumerable<Customer> Search(string search) => CustomerDAO.Instance.Search(search);

        public bool UpdateCustomer(Customer customer) => CustomerDAO.Instance.UpdateCustomer(customer);
    }
}
