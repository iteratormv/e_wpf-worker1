using EX.Model.DbLayer;
using EX.Model.DbLayer.Settings;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace EX.Model.Repositories.Setting
{
    public class DSCollumnSettingRepository
    {
        EContext context;

        public DSCollumnSettingRepository()
        {
            context = new EContext();
        }

        public DSCollumnSetting 
            AddOrUpdateDSCollumnSetting(DSCollumnSetting dSCollumnSetting)
        {
            context.DSCollumnSettings.AddOrUpdate(dSCollumnSetting);
            context.SaveChanges();
            //if (context.DSCollumnSettings.Where(d=>
            //d.Name == dSCollumnSetting.Name&&
            //d.Visible == dSCollumnSetting.Visible&&
            //d.Width == dSCollumnSetting.Width).Count() == 0)
            //{
            //    context.DSCollumnSettings.AddOrUpdate(dSCollumnSetting);
            //    context.SaveChanges();
            //}
            return context.DSCollumnSettings.Where(d =>
            d.Name == dSCollumnSetting.Name &&
            d.Visible == dSCollumnSetting.Visible &&
            d.Width == dSCollumnSetting.Width).FirstOrDefault();
        }

        public IEnumerable<DSCollumnSetting> GetAllDSCollumnSettings()
        {
            var result = context.DSCollumnSettings.ToList();
            return result;
        }

        public bool RemoveDSCollumnSetting(DSCollumnSetting dSCollumnSetting)
        {
            bool result;
            try
            {
                var _dSCollumnSetting = context.DSCollumnSettings.
                    Where(s => s.Id == dSCollumnSetting.Id).FirstOrDefault();
                context.DSCollumnSettings.Remove(_dSCollumnSetting);
                context.SaveChanges();
                result = true;
            } catch { result = false; }
            return result;
        }
    }
}
