using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model.Security
{
    public class UserRole
    {
        public int Id { get; set; }
        //nombre de la relacion
        public int UserId { get; set; }
        //referencia de la relacion
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ? UpdateAt  { get; set; }
        public DateTime ? DeletedAt { get; set; }
        public bool State { get; set; }
    }
}
