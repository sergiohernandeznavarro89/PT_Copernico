using AutoMapper;
using Database.BD.Context;
using Database.BD.Models;
using Microsoft.EntityFrameworkCore;
using PagedList;
using Service.DTO;
using Service.Helpers;
using Service.Service;
using Service.Service.IService;
using TestSupport.EfHelpers;
using Customer = Database.BD.Models.Customer;
using CustomerService = Service.Service.CustomerService;

namespace Test
{
    [TestClass]
    public class CurstomerTest
    {

        private readonly IMapper mapper;

        public CurstomerTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            mapper = new Mapper(mappingConfig);
        }

        #region GET_ALL
        [TestMethod]
        public async Task GetAllPaginated()
        {
            DbContextOptions<MyContext> options = SqliteInMemory.CreateOptions<MyContext>();

            using (MyContext context = new MyContext(options))
            {
                await SeedData(context);

                CustomerService service = new CustomerService(context, mapper);

                IPagedList<Service.DTO.CustomerDTO> customers = await service.GetAll(1);

                Assert.AreEqual(customers.Count, 2);
                Assert.AreEqual(customers.TotalItemCount, 2);
            }
        }

        [TestMethod]
        public async Task GetAllPagedEmpty()
        {
            DbContextOptions<MyContext> options = SqliteInMemory.CreateOptions<MyContext>();

            using (MyContext context = new MyContext(options))
            {
                await SeedData(context);

                CustomerService service = new CustomerService(context, mapper);

                IPagedList<Service.DTO.CustomerDTO> customers = await service.GetAll(2);
                Assert.AreEqual(customers.Count, 0);
                Assert.AreEqual(customers.TotalItemCount, 2);
            }
        }
        #endregion

        #region GET_BY_ID
        [TestMethod]
        public async Task GetByIdOk()
        {
            DbContextOptions<MyContext> options = SqliteInMemory.CreateOptions<MyContext>();

            using (MyContext context = new MyContext(options))
            {
                await SeedData(context);

                CustomerService service = new CustomerService(context, mapper);

                Service.DTO.CustomerDTO customer = await service.GetById(1);

                Assert.IsNotNull(customer);                
            }
        }

        [TestMethod]
        public async Task GetByIdEmpty()
        {
            DbContextOptions<MyContext> options = SqliteInMemory.CreateOptions<MyContext>();

            using (MyContext context = new MyContext(options))
            {
                await SeedData(context);

                CustomerService service = new CustomerService(context, mapper);

                Service.DTO.CustomerDTO customer = await service.GetById(4);

                Assert.IsNull(customer);
            }
        }
        #endregion

        #region ADD
        [TestMethod]
        public async Task AddNewCustomer()
        {
            DbContextOptions<MyContext> options = SqliteInMemory.CreateOptions<MyContext>();

            using (MyContext context = new MyContext(options))
            {
                await SeedData(context);

                CustomerService service = new CustomerService(context, mapper);

                CustomerDTO customer = new CustomerDTO { id = 3, first = "first3", last = "last3", company = "company3", email = "email3", country = "country3", created_at = DateTime.Now };

                int result = await service.Add(customer);

                Assert.AreEqual(result, 3);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task AddExistingId()
        {
            DbContextOptions<MyContext> options = SqliteInMemory.CreateOptions<MyContext>();

            using (MyContext context = new MyContext(options))
            {
                await SeedData(context);

                CustomerService service = new CustomerService(context, mapper);

                CustomerDTO customer = new CustomerDTO { id = 1, first = "first1", last = "last1", company = "company1", email = "email1", country = "country1", created_at = DateTime.Now };
                
                int result = await service.Add(customer);                    
            }
        }
        #endregion
        
        #region UPDATE
        [TestMethod]
        public async Task UpdateExistingCustomer()
        {
            DbContextOptions<MyContext> options = SqliteInMemory.CreateOptions<MyContext>();

            using (MyContext context = new MyContext(options))
            {
                await SeedData(context);

                CustomerService service = new CustomerService(context, mapper);

                CustomerDTO customer = new CustomerDTO { id = 2, first = "first3", last = "last3", company = "company3", email = "email3", country = "country3", created_at = DateTime.Now };

                CustomerDTO result = await service.Update(customer);

                Assert.AreEqual(result, customer);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task UpdateNotExistingCustomer()
        {
            DbContextOptions<MyContext> options = SqliteInMemory.CreateOptions<MyContext>();

            using (MyContext context = new MyContext(options))
            {
                await SeedData(context);

                CustomerService service = new CustomerService(context, mapper);

                CustomerDTO customer = new CustomerDTO { id = 3, first = "first3", last = "last3", company = "company3", email = "email3", country = "country3", created_at = DateTime.Now };
                
                CustomerDTO result = await service.Update(customer);                          
            }
        }
        #endregion
        
        #region DELETE
        [TestMethod]
        public async Task DeleteExistingCustomer()
        {
            DbContextOptions<MyContext> options = SqliteInMemory.CreateOptions<MyContext>();

            using (MyContext context = new MyContext(options))
            {
                await SeedData(context);

                CustomerService service = new CustomerService(context, mapper);

                int result = await service.Delete(1);

                Assert.AreEqual(result, 1);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task DeleteNotExistingCustomer()
        {
            DbContextOptions<MyContext> options = SqliteInMemory.CreateOptions<MyContext>();

            using (MyContext context = new MyContext(options))
            {
                await SeedData(context);

                CustomerService service = new CustomerService(context, mapper);
               
                await service.Delete(34);                    
            }
        }
        #endregion

        #region METODOS PRIVADOS
        private async Task SeedData(MyContext context)
        {
            context.CreateEmptyViaDelete();

            Customer customer1 = new Customer { id = 1, first = "first1", last = "last1", company = "company1", email = "email1", country = "country1", created_at = DateTime.Now };
            Customer customer2 = new Customer { id = 2, first = "first2", last = "last2", company = "company2", email = "email2", country = "country2", created_at = DateTime.Now };

            context.Customers.AddRange(customer1, customer2);

            await context.SaveChangesAsync();
        }
        #endregion
    }
}