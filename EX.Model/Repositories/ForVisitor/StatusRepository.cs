using EX.Model.DbLayer;
using EX.Model.Exel;
using EX.Model.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace EX.Model.Repositories.ForVisitor
{
    public class StatusRepository
    {
        EContext context;
        Progress_Bar progress;

        public StatusRepository()
        {
            context = new EContext();
            progress = new Progress_Bar();
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





        public void importStatusRepositoryFromFileWithId(string fileName, Action<Progress_Bar> progressChanged)
        {

            ExelData exelData = new ExelData(fileName, progressChanged);
            progress.Status = "Delete old statuses";
            progress.Progress = 0;
            using (var ctx = new EContext())
            {
                int count = 1;
                var collection = ctx.Statuses.ToList();
                var size = collection.Count() + 1;
                foreach (var stat in collection)
                {
                    progress.Progress = (int)(count * 100 / size);
                    progressChanged(progress);
                    ctx.Statuses.Remove(stat);
                    count++;
                }
                ctx.SaveChanges();
                var dat = new List<Status>();
                exelData.importSatausToCollectionWithId(dat, progressChanged);
                progress.Status = "Add new data to database";
                progress.Progress = 0;
                int co = 1;
                var sd = dat.Count() + 1;
                foreach (var d in dat)
                {
                    const string InsertQuery = @"SET IDENTITY_INSERT dbo.Status ON;
                                                 INSERT INTO Status(Id, Name, ActionTime,
                                                 UserId, VisitorId) 
                                                 VALUES({0},{1},{2},{3},{4}); 
                                                 SET IDENTITY_INSERT dbo.Status OFF;";
                    try
                    {
                        context.Database.ExecuteSqlCommand(InsertQuery, d.Id, d.Name,
                        d.ActionTime, d.UserId, d.VisitorId);
                        context.SaveChanges();
                    }
                    catch { }
                    progress.Progress = (int)(co * 100 / sd);
                    progressChanged(progress);
                    co++;
                }
            }
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
