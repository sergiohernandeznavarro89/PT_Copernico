using AutoMapper;
using AutoMapper.QueryableExtensions;
using Database.BD.Context;
using Database.BD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PagedList;
using Service.DTO;
using Service.Helpers;
using Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Fabric.Query;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly MyContext _context;
        private readonly IMapper _mapper;

        public CustomerService(MyContext myContext, IMapper mapper)
        {
            _context = myContext;
            _mapper = mapper;
        }

        public async Task<IPagedList<CustomerDTO>> GetAll(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;
            try
            {
                IPagedList<CustomerDTO> result = _context.Customers.AsQueryable().AsNoTracking().ProjectTo<CustomerDTO>(_mapper.ConfigurationProvider).ToPagedList(pageNumber, pageSize);
                return result;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }            
        }

        public async Task<CustomerDTO> GetById(int id)
        {
            try
            {
                IQueryable<Customer> query = _context.Customers.Where(c => c.id == id).AsNoTracking();
                CustomerDTO result = await query.ProjectTo<CustomerDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
                return result;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }            
        }

        public async Task<int> Add(CustomerDTO customerDto)
        {
            try
            {
                Customer customer = _mapper.Map<Customer>(customerDto);
                customer.created_at = DateTime.Now;
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return customer.id;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task AutoAdd()
        {
            string fileName = "../Service/Static/customers.json";
            string jsonString = File.ReadAllText(fileName);
            List<Customer> customers = JsonSerializer.Deserialize<List<Customer>>(jsonString);
            _context.AddRange(customers);
            _context.SaveChanges();
        }

        public async Task<CustomerDTO> Update(CustomerDTO customerDto)
        {
            Customer? customer = _context.Customers.FirstOrDefault(c => c.id == customerDto.id);

            if (customer != null)
            {
                _context.Update(customer);

                customer.email = customerDto.email;
                customer.first = customerDto.first;
                customer.last = customerDto.last;
                customer.company = customerDto.company;
                customer.country = customerDto.country;

                await _context.SaveChangesAsync();
                return customerDto;
            }
            else
            {
                throw new Exception("Customer doesnt exist");
            }            
        }

        public async Task<int> Delete(int id)
        {
            Customer customer = _context.Customers.FirstOrDefault(item => item.id == id);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return id;
            }
            else
            {
                throw new Exception("Customer doesnt exist");
            }            
        }
    }
}
