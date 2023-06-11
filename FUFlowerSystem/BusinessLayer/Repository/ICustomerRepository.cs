using BusinessLayer.DAO;
using DataLayer.Models;

namespace BusinessLayer.Repository
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
        Customer Get(int id);
        Customer CheckLogin(string email);
        bool AddCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool RemoveCustomer(int id);
    }

    public class CustomerRepository : ICustomerRepository
    {
        public bool AddCustomer(Customer customer) => CustomerDAO.Instance.AddCustomer(customer);

        public Customer CheckLogin(string email) => CustomerDAO.Instance.CheckLogin(email);

        public Customer Get(int id) => CustomerDAO.Instance.Get(id);

        public IEnumerable<Customer> GetAll() => CustomerDAO.Instance.GetAll();

        public bool RemoveCustomer(int id) => CustomerDAO.Instance.RemoveCustomer(id);

        public bool UpdateCustomer(Customer customer) => CustomerDAO.Instance.UpdateCustomer(customer);
    }
}
