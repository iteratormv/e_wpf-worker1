using System.Collections.Generic;

namespace EX.Model.DbLayer.Settings
{
    public class PrintSetting
    {
        public int Id { get;set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }

        public virtual List<PrintStringSetting> PrintStringSettings { get; set; }
    }
}
