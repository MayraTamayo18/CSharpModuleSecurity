using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model.Security
{
    public class Person
    {
        public int Id { get; set; }
        public string Frist_name { get; set; }
        public string Last_name { get; set; }
        public string Email { get; set; }
        public string Addres { get; set; }
        public string Type_documment { get; set; }
        public string Document { get; set; }
        public DateTime Birth_of_date { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ? UpdateAt { get; set; }
        public DateTime ? DeletedAt { get; set; }
        public string  Phone { get; set; }
        public bool State { get; set; }
        
       

    }
}
