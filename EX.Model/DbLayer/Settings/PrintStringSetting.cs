using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX.Model.DbLayer.Settings
{
    public class PrintStringSetting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FontName { get; set; }
        public string FontStyle { get; set; }
        public int FontSize { get; set; }
        public bool Visible { get; set; }
        public bool ToUpper { get; set; }

        public int PrintSettingId { get; set; }
        public PrintSetting PrintSetting { get; set; }
    }
}
