using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.BD.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> data) : base(data) { }       
    }
}
