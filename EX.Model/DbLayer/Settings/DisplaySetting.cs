using System.Collections.Generic;
namespace EX.Model.DbLayer.Settings

{
    public class DisplaySetting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Intendant { get; set; }
        public bool IsSelected { get; set; }

        public virtual List<DSCollumnSetting> DSCollumnSettings { get; set; }
    }
}