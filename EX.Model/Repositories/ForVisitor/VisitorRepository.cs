using EX.Model.DbLayer;
using EX.Model.Exel;
using EX.Model.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.IO;
using System.Threading;

namespace EX.Model.Repositories
{
    public class VisitorRepository
    {
        EContext context;
        Progress_Bar progress;

        public VisitorRepository()
        {
            context = new EContext();
            progress = new Progress_Bar();
        }
        public IEnumerable<Visitor> AddDataToRepositoryFromFile(string fileName)
        {
            ExelData exelData = new ExelData(fileName);
            exelData.setDataToCollection(context.Visitors);
            context.SaveChanges();
            var v = context.Visitors.ToList();
            return v;
        }
        public void initRepositoryFromFile(string fileName, Action<Progress_Bar> progressChanged)
        {
            ExelData exelData = new ExelData(fileName, progressChanged);
            progress.Status = "Delete old visitors";
            progress.Progress = 0;
            using (var ctx = new EContext())
            {
                int count = 1;
                var collection = ctx.Visitors.ToList();
                var size = collection.Count() + 1;
                foreach (var u in collection)
                {
                    progress.Progress = (int)(count * 100 / size);
                    progressChanged(progress);
                    ctx.Visitors.Remove(u);
                    count++;
                }
                ctx.SaveChanges();
            }
            exelData.setDataToCollection(context.Visitors, progressChanged);
            context.SaveChanges();
            progress.Status = "Add new data to collection";
            progress.Progress = 0;
            int c = 1;
            var col = context.Visitors;
            var s = col.Count() + 1;
            foreach (var v in col)
            {
                progress.Progress = (int)(c * 100 / s);
                progressChanged(progress);
                //   visitorCollection.Add(v);
                AddOrUpdateVisitor(v);
                c++;
            }
        }
        public void importRepositoryFromFile(string fileName, Action<Progress_Bar> progressChanged)
        {
            ExelData exelData = new ExelData(fileName, progressChanged);
            progress.Status = "Delete old visitors";
            progress.Progress = 0;
            using (var ctx = new EContext())
            {
                int count = 1;
                var collection = ctx.Visitors.ToList();
                var size = collection.Count() + 1;
                foreach (var u in collection)
                {
                    progress.Progress = (int)(count * 100 / size);
                    progressChanged(progress);
                    ctx.Visitors.Remove(u);
                    count++;
                }
                ctx.SaveChanges();
            }
            exelData.importDataToCollection(context.Visitors, progressChanged);
            context.SaveChanges();
            progress.Status = "Add new data to collection";
            progress.Progress = 0;
            int c = 1;
            var col = context.Visitors;
            var s = col.Count() + 1;
            foreach (var v in col)
            {
                progress.Progress = (int)(c * 100 / s);
                progressChanged(progress);
                AddOrUpdateVisitor(v);
                c++;
            }
        }
        public void importRepositoryFromFileWithId(string fileName, Action<Progress_Bar> progressChanged)
        {

            ExelData exelData = new ExelData(fileName, progressChanged);
            progress.Status = "Delete old visitors";
            progress.Progress = 0;
            using (var ctx = new EContext())
            {
                int count = 1;
                var collection = ctx.Visitors.ToList();
                var size = collection.Count() + 1;
                foreach (var u in collection)
                {
                    progress.Progress = (int)(count * 100 / size);
                    progressChanged(progress);
                    ctx.Visitors.Remove(u);
                    count++;
                }
                ctx.SaveChanges();

                var dat = new List<Visitor>();
                exelData.importVisitorsToCollectionWithId(dat, progressChanged);
                progress.Status = "Add new data to database";
                progress.Progress = 0;
                int co = 1;
                var sd = dat.Count()+1;
                foreach (var d in dat)
                {
                    const string InsertQuery = @"SET IDENTITY_INSERT dbo.Visitors ON;
                                                 INSERT INTO Visitors(Id, Column1, Column2,
                                                 Column3, Column4, Column5, Column6, Column7, 
                                                 Column8, Column9, Column10, Column11, Column12,
                                                 Column13, Column14, Column15, CurrentStatus) 
                                                 VALUES({0},{1},{2},{3},{4},{5},{6},{7},
                                                 {8},{9},{10},{11},{12},{13},{14},{15},{16}); 
                                                 SET IDENTITY_INSERT dbo.Visitors OFF;";
                    context.Database.ExecuteSqlCommand(InsertQuery, d.Id, d.Column1,
                        d.Column2, d.Column3, d.Column4, d.Column5, d.Column6, d.Column7,
                        d.Column8, d.Column9, d.Column10, d.Column11, d.Column12,
                        d.Column13, d.Column14, d.Column15, d.CurrentStatus);
                    context.SaveChanges();
                    progress.Progress = (int)(co * 100 / sd);
                    progressChanged(progress);
                    co++;
                }
            }
            progress.Status = "Add new data to collection";
            progress.Progress = 0;
            int c = 1;
            var col = context.Visitors.ToList();
            var s = col.Count() + 1;
            foreach (var v in col)
            {
                progress.Progress = (int)(c * 100 / s);
                progressChanged(progress);
                AddOrUpdateVisitor(v);
                c++;
            }

        }
        public Visitor AddOrUpdateVisitor(Visitor visitor)
        {         
            context.Visitors.AddOrUpdate(visitor);
            context.SaveChanges();
            return context.Visitors.Where(s => s.Column1 == visitor.Column1 &&
                                               s.Column2 == visitor.Column2 &&
                                               s.Column3 == visitor.Column3 &&
                                               s.Column4 == visitor.Column4 &&
                                               s.Column5 == visitor.Column5 &&
                                               s.Column6 == visitor.Column6 &&
                                               s.Column7 == visitor.Column7 &&
                                               s.Column8 == visitor.Column8 &&
                                               s.Column9 == visitor.Column9 &&
                                               s.Column10 == visitor.Column10 &&
                                               s.Column11 == visitor.Column11 &&
                                               s.Column12 == visitor.Column12 &&
                                               s.Column13 == visitor.Column13 &&
                                               s.Column14 == visitor.Column14 &&
                                               s.Column15 == visitor.Column15 &&
                                               s.CurrentStatus == visitor.CurrentStatus)
                                               .FirstOrDefault();
        }
        public IEnumerable<Visitor> GetAllVisitors()
        {
            var v = context.Visitors.ToList();
            return v;
        }
        public Visitor GetVisitorById(int Id)
        {
            return context.Visitors.Where(c => c.Id == Id).FirstOrDefault();
        }
        public void RemoveVisitor(Visitor visitor)
        {
            context.Visitors.Remove(visitor);
            context.SaveChanges();
        }
        public void RemoveVisitorById(int Id)
        {
            context.Visitors.Remove(context.Visitors.Where(c => c.Id == Id).FirstOrDefault());
            context.SaveChanges();
        }
        public void saveVisitorsTofile()
        {
            ExelData exelData = new ExelData();
            var visitors = context.Visitors;
            exelData.saveVisitorsTofile(visitors);
        }


  //      public event Action<Progress_Bar> progressChanged;

    }
}
