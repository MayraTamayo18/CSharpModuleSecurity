using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model.Security
{
    public class RoleView
    {
        public int Id { get; set; }
        // nombre de la relacion
        public int RoleId { get; set; }
        // referencia de la relacion
        public Role role { get; set; }

        public int ViewId { get; set; }

        public View view { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ? UpdateAt { get; set; }
        public DateTime ? DeletedAt { get; set; }
        public bool State { get; set; }
    }
}
