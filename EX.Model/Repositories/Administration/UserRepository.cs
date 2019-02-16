using EX.Model.DbLayer;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace EX.Model.Repositories.Administration
{
    public class UserRepository 
    {
        EContext context;

        public UserRepository()
        {
            context = new EContext();
        }

        public User AddOrUpdateUser(User user)
        {
            if(context.Users.Where(s=>s.Login == user.Login&&s.Id==0).Count() == 0)
            {
                context.Users.AddOrUpdate(user);
                context.SaveChanges();
            }
            return (context.Users.Where(s => s.Login == user.Login).FirstOrDefault());
        }

        public IEnumerable<User> GetAllUsers()
        {
            var u = context.Users.ToList();
            return u;

        }

        public User GetUserById(int Id)
        {
            return context.Users.Where(u => u.Id == Id).FirstOrDefault();
        }

        public bool RemoveUser(User user)
        {
            bool result;
            try
            {
                var _user = context.Users.Where(u => u.Id == user.Id).FirstOrDefault();
                context.Users.Remove(_user);
                context.SaveChanges();
                result = true;
            }
            catch { result = false; }
            return result;

        }

        public bool RemoveUserById(int Id)
        {
            bool result;
            try
            {
                context.Users.Remove(context.Users.Where(u => u.Id == Id).FirstOrDefault());
                context.SaveChanges();
                result = true;
            }
            catch { result = false; }
            return result;
        }
    }
}
