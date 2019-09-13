using EX.Model.DbLayer;
using EX.Model.DbLayer.Settings;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace EX.Model.Repositories.Setting
{
    class PrintSettingRepository
    {
        EContext context;

        public PrintSettingRepository()
        {
            context = new EContext();
        }
        public PrintSetting AddOrUpdatePrintSetting(PrintSetting printSetting)
        {
            context.PrintSettings.AddOrUpdate(printSetting);
            context.SaveChanges();
            return context.PrintSettings.Where(s =>
            s.Name == printSetting.Name &&
            s.IsSelected == printSetting.IsSelected).FirstOrDefault();
        }
        public IEnumerable<PrintSetting> GetAllPrintSetting()
        {
            var p = context.PrintSettings.ToList();
            return p;
        }
        public bool RemovePrintSetting(PrintSetting printSetting)
        {
            bool result;
            try
            {
                var _printsetting = context.PrintSettings.Where(p =>
                p.Id == printSetting.Id).FirstOrDefault();
                context.PrintSettings.Remove(_printsetting);
                context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
