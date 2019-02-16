using System.Collections.Generic;

namespace EX.Model.DbLayer
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsSelected { get; set; }
        public bool IsDefault { get; set; }

        public virtual List<Status> Statuses { get; set; }
        public virtual List<UserInRole> UserInRoles { get; set; }
    }
}