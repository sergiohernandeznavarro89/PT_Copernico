using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public class CustomerDTO
    {
        public int id { get; set; }
        public string email { get; set; }
        public string first { get; set; }
        public string last { get; set; }
        public string company { get; set; }
        public DateTime created_at { get; set; }
        public string country { get; set; }

        public static implicit operator List<object>(CustomerDTO v)
        {
            throw new NotImplementedException();
        }
    }
}
