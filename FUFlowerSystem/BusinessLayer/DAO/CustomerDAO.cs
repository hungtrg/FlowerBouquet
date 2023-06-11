﻿using DataLayer.Models;

namespace BusinessLayer.DAO
{
    public class CustomerDAO
    {
        private static CustomerDAO? instance;
        private static readonly object instanceLock = new object();
        private readonly FuflowerSystemDbContextContext _context = new FuflowerSystemDbContextContext();

        public static CustomerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CustomerDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            List<Customer> list;
            try
            {
                list = _context.Customers.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public Customer Get(int id)
        {
            Customer? customer;
            try
            {
                customer = _context.Customers.FirstOrDefault(c => c.CustomerId.Equals(id));
            }
            catch (Exception)
            {
                throw;
            }
            return customer;
        }

        public Customer CheckLogin(string email)
        {
            Customer? customer;
            try
            {
                customer = _context.Customers.FirstOrDefault(c => c.Email.Equals(email));
            }
            catch (Exception)
            {
                throw;
            }
            return customer;
        }

        public bool AddCustomer(Customer customer)
        {
            bool result = false;
            try
            {
                Customer c = Get(customer.CustomerId);
                if (c == null)
                {
                    _context.Customers.Add(customer);
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

        public bool UpdateCustomer(Customer customer)
        {
            bool result = false;
            try
            {
                Customer c = Get(customer.CustomerId);
                if (c != null)
                {
                    _context.Entry<Customer>(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

        public bool RemoveCustomer(int id)
        {
            bool result = false;
            try
            {
                Customer c = Get(id);
                if (c != null)
                {
                    _context.Remove(c);
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
