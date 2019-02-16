using System.Collections.Generic;

namespace EX.Model.DbLayer
{
    public class Tab
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
 //       public bool IsSelected { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}