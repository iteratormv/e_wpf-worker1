using EX.Model.DbLayer;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace EX.Model.Repositories.Administration
{
    public class UserInRoleRepository
    {
        EContext context;

        public UserInRoleRepository()
        {
            context = new EContext();
        }

        public UserInRole AddOrUpdateUserInRole(UserInRole userInRole)
        {
            var checkUserInRole = context.UserInRoles.Where(ur =>
            ur.RoleId == userInRole.RoleId &&
            ur.UserId == userInRole.UserId).FirstOrDefault();
            if(checkUserInRole == null)
            {
                context.UserInRoles.AddOrUpdate(userInRole);
                context.SaveChanges();
                return (context.UserInRoles.Where(ur =>
                        ur.RoleId == userInRole.RoleId &&
                        ur.UserId == userInRole.UserId).FirstOrDefault());
            }
            else return checkUserInRole;
        }

        public IEnumerable<UserInRole> GetAllUserInRoles()
        {
            var ur = context.UserInRoles.ToList();
            return ur;
        }

        public bool RemoveUserInRoles(UserInRole userInRole)
        {
            bool result=false;
            //try
            //{
                var _userInRole = context.UserInRoles.Where(ur => ur.Id == userInRole.Id).FirstOrDefault();
                context.UserInRoles.Remove(_userInRole);
                context.SaveChanges();
            //    result = true;
            //}
            //catch { result = false; }
            //var _userInRole = context.UserInRoles.Where(ur => ur.Id == userInRole.Id).FirstOrDefault();
            //context.UserInRoles.Remove(_userInRole);
            //context.SaveChanges();
            return result;
        }
    }
}
