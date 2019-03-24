using EX.Model.DbLayer;
using EX.Model.DbLayer.Settings;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace EX.Model.Repositories.Setting
{
    class PrintStringSettingRpository
    {
        EContext context;

        public PrintStringSettingRpository()
        {
            context = new EContext();
        }
        public PrintStringSetting AddOrUpdatePrintStringSetting(PrintStringSetting printStringSetting)
        {
            context.PrintStringSettings.AddOrUpdate(printStringSetting);
            context.SaveChanges();
            return context.PrintStringSettings.Where(p =>
            p.Id == printStringSetting.Id &&
            p.Name == printStringSetting.Name &&
            p.ToUpper == printStringSetting.ToUpper &&
            p.Visible == printStringSetting.Visible &&
            p.PrintSettingId == printStringSetting.PrintSettingId).FirstOrDefault();
        }
        public IEnumerable<PrintStringSetting> GetAllStringSettings()
        {
            var p = context.PrintStringSettings.ToList();
            return p;
        }
        public bool RemovePrintStringSetting(PrintStringSetting printStringSetting)
        {
            bool result;
            try
            {
                var _printStringSetting =
                    context.PrintStringSettings.Where(p =>
                    p.Id == printStringSetting.Id).FirstOrDefault();
                context.PrintStringSettings.Remove(_printStringSetting);
                context.SaveChanges();
                result = true;
            }
            catch
            {
                result = true;
            }
            return result;
        }
    }
}
