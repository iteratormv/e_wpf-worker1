using EX.Model.DbLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX.Model.Repositories.Administration
{
    public class CommandRepository
    {
        EContext context;

        public CommandRepository()
        {
            context = new EContext();
        }

        public void AddCommandForCurrentRole(int roleId)
        {
            context.Commands.Add(new Command { Name = "AddUser", IsChecked = true, RoleId = roleId });
            context.Commands.Add(new Command { Name = "DelUser", IsChecked = true, RoleId = roleId });
            context.Commands.Add(new Command { Name = "AddRoles", IsChecked = true, RoleId = roleId });
            context.Commands.Add(new Command { Name = "DelRoles", IsChecked = true, RoleId = roleId });
            context.Commands.Add(new Command { Name = "SaveChanges", IsChecked = true, RoleId = roleId });
            context.Commands.Add(new Command { Name = "LoadFile", IsChecked = true, RoleId = roleId });
            context.SaveChanges();
        }

        public void RemoveCommandRepository(int roleId)
        {
            var dellCommands = context.Commands.Where(c => c.RoleId == roleId);
            foreach (var d in dellCommands) { context.Commands.Remove(d); }
            context.SaveChanges();
        }

        public IEnumerable<Command> GetAllCommands() { return context.Commands; }

        public void UpdateCommandRepository(IEnumerable<Command> commands)
        {
            foreach (var c in commands) { context.Commands.AddOrUpdate(c); }
            context.SaveChanges();
        }
    }
}
