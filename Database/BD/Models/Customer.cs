using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.BD.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public string email { get; set; }
        public string first { get; set; }
        public string last { get; set; }
        public string company { get; set; }
        public DateTime created_at { get; set; }
        public string country { get; set; }
    }
}
