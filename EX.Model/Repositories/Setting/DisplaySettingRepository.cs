﻿using EX.Model.DbLayer;
using EX.Model.DbLayer.Settings;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX.Model.Repositories.Setting
{
    public class DisplaySettingRepository
    {
        EContext context;

        public DisplaySettingRepository()
        {
            context = new EContext();
        }

        public DisplaySetting AddOrUpdateDisplaySetting(DisplaySetting displaySetting)
        {
            context.DisplaySettings.AddOrUpdate(displaySetting);
            context.SaveChanges();
            //if (context.DisplaySettings.Where(s =>
            // s.Name == displaySetting.Name &&
            // s.Intendant == displaySetting.Intendant).Count() == 0)
            //{
            //    var _displaySetting = context.DisplaySettings.
            //        Where(s => s.Id == displaySetting.Id).FirstOrDefault();
            //    context.DisplaySettings.AddOrUpdate(_displaySetting);
            //    context.SaveChanges();
            //}
            return context.DisplaySettings.Where(s =>
            s.Name == displaySetting.Name &&
            s.Intendant == displaySetting.Intendant).FirstOrDefault();
        }

        public IEnumerable<DisplaySetting> GetAllDisplaySettings()
        {
            var s = context.DisplaySettings.ToList();
            return s;
        }

        public bool RemoveDisplaySetting(DisplaySetting displaySetting)
        {
            bool result;
            try
            {
                var _displaySetting = context.DisplaySettings.Where(d =>
                d.Id == displaySetting.Id).FirstOrDefault();
                context.DisplaySettings.Remove(_displaySetting);
                context.SaveChanges();
                result = true;
            }
            catch { result = false; }
            return result;
        }
    }
}
