using PagedList;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Fabric.Query;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.IService
{
    public interface ICustomerService
    {
        Task<IPagedList<CustomerDTO>> GetAll(int? page);
        Task<CustomerDTO> GetById(int id);
        Task<int> Add(CustomerDTO customer);
        Task AutoAdd();
        Task<CustomerDTO> Update(CustomerDTO customer);
        Task<int> Delete(int customerId);
    }
}
