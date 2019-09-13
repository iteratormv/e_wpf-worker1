namespace EX.Model.DbLayer
{
    using EX.Model.DbLayer.Settings;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class EContext : DbContext
    {
        // Контекст настроен для использования строки подключения "EContext" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "EX.Model.DbLayer.EContext" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "EContext" 
        // в файле конфигурации приложения.
        public EContext()
            : base("name=EContext")
        {
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Visitor> Visitors { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<UserInRole> UserInRoles { get; set; }
        public virtual DbSet<Command> Commands { get; set; }
        public virtual DbSet<Tab> Tabs { get; set; }
        public virtual DbSet<DisplaySetting> DisplaySettings { get; set; }
        public virtual DbSet<DSCollumnSetting> DSCollumnSettings { get; set; }
        public virtual DbSet<PrintSetting> PrintSettings { get; set; }
        public virtual DbSet<PrintStringSetting> PrintStringSettings { get; set; }
        public virtual DbSet<ColorSetting> ColorSettings { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}