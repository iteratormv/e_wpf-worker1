using EX.Model.DbLayer;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace EX.Model.Repositories.Administration
{
    public class RoleRepository
    {
        EContext context;

        public RoleRepository()
        {
            context = new EContext();
        }

        public Role AddOrUpdate(Role role)
        {
            bool needAddAtribute = role.Id == 0;
            context.Roles.AddOrUpdate(role);
            context.SaveChanges();
            if (needAddAtribute)
            {
                var roleId = context.Roles.Where(r => r.Name == role.Name).FirstOrDefault().Id;
                var tabs = new TabRepository();
                var commands = new CommandRepository();
                tabs.AddTabsForCurrentRole(roleId);
                commands.AddCommandForCurrentRole(roleId);
            }
            return context.Roles.Where(r => r.Name == role.Name).FirstOrDefault();    
        }

        public void RemoveRole(Role role)
        {
            var delRole = context.Roles.Where(r => r.Id == role.Id).FirstOrDefault();
            context.Roles.Remove(delRole);
            context.SaveChanges();
        }

        public IEnumerable<Role> GetAllRoles() { return context.Roles; }
    }
}
