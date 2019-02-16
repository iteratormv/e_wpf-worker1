using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX.Model.DTO
{
    public class SubTabDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }

        public int TabId { get; set; }
        public int RoleId { get; set; }
    }
}
