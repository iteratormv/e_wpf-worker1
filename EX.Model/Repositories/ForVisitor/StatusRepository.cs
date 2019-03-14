using EX.Model.DbLayer;
using EX.Model.Exel;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace EX.Model.Repositories.ForVisitor
{
    public class StatusRepository
    {
        EContext context;

        public StatusRepository()
        {
            context = new EContext();
        }
        public IEnumerable<Status> GetAllStatuses()
        {
            var s = context.Statuses.ToList();
            return s;
        }
        public void RemoveStatus(Status status)
        {
            context.Statuses.Remove(status);
            context.SaveChanges();
        }
        public Status Add(Status status)
        {
            context.Statuses.Add(status);
            context.SaveChanges();
            return context.Statuses.Where(s => s.Name == status.Name &&
                                               s.UserId == status.UserId &&
                                               s.VisitorId == status.VisitorId)
                                               .FirstOrDefault();
        }

        public void SaveStatusesToFile()
        {
            var statuses = context.Statuses;
            ExelData exelData = new ExelData();
            exelData.saveStatusesToFile(statuses);
        }

    }
}
