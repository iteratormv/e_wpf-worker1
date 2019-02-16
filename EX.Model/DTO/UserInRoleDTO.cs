using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX.Model.DTO
{
    public class UserInRoleDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
