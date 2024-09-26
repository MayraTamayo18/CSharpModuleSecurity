using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model.Security
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool State { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ? UpdatedAt { get; set; }
        public DateTime ? DeletedAt { get; set; }

    }
}
