namespace EX.Model.DbLayer.Settings
{
    public class DSCollumnSetting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool Visible { get; set; }
        public int Width { get; set; }
        public bool IsSelected { get; set; }
        public string Intendant { get; set; }
        public bool IsRequiredInput { get; set; }

        public int DisplaySettingId { get; set; }
        public virtual DisplaySetting DisplaySetting { get; set; }
    }
}