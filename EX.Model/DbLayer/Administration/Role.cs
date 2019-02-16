using System.Collections.Generic;

namespace EX.Model.DbLayer
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
        public bool IsDefault { get; set; }

        public virtual List<UserInRole> UserInRoles { get; set; }
        public virtual List<Command> Commands { get; set; } 
        public virtual List<Tab> Tabs { get; set; }
    }
}
