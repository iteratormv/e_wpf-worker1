using EX.Model.DbLayer;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace EX.Model.Repositories.Administration
{
    public class TabRepository
    {
        EContext context;

        public TabRepository()
        {
            context = new EContext();
        }

        public void AddTabsForCurrentRole(int roleId)
        {
            context.Tabs.Add(new Tab { Name = "Главная", IsChecked = true, /*IsSelected = true,*/ RoleId = roleId });
            context.Tabs.Add(new Tab { Name = "Файл", IsChecked = true, /*IsSelected = false,*/ RoleId = roleId });
            context.Tabs.Add(new Tab { Name = "Поля этикетки (Конфигурации)", IsChecked = true, /*IsSelected = false,*/ RoleId = roleId });
            context.Tabs.Add(new Tab { Name = "Цвет бейджа (Конфигурации)", IsChecked = true, /*IsSelected = false, */RoleId = roleId });
            context.Tabs.Add(new Tab { Name = "Рабочий стол (Конфигурации)", IsChecked = true, /*IsSelected = false, */RoleId = roleId });
            context.Tabs.Add(new Tab { Name = "Отчёты (Конфигурации)", IsChecked = true, /*IsSelected = false,*/ RoleId = roleId });
            context.Tabs.Add(new Tab { Name = "Регистрация (Администрирование)", IsChecked = true,/* IsSelected = false,*/ RoleId = roleId });
            context.Tabs.Add(new Tab { Name = "Авторизация (Администрирование)", IsChecked = true,/* IsSelected = false,*/RoleId = roleId });
            context.Tabs.Add(new Tab { Name = "Управление доступом (Администрирование)", IsChecked = true,/* IsSelected = false,*/ RoleId = roleId });
            context.Tabs.Add(new Tab { Name = "Режим работы (Сеть)", IsChecked = true, /*IsSelected = false,*/ RoleId = roleId });
            context.SaveChanges();
        }

        public void RemoveCurrentTabRepository(int roleId)
        {
            var delTabs = context.Tabs.Where(t => t.RoleId == roleId);
            foreach (var t in delTabs)
            {
                if (t != null) context.Tabs.Remove(t);
            }
            context.SaveChanges();
        }

        public IEnumerable<Tab> GetAllTabs() { return context.Tabs; }

        //public IEnumerable<SubTab> GetSubTubs(Tab tab) { return context.SubTabs.Where(s => s.TabId == tab.Id); }

        //public IEnumerable<SubTab> GetAllSubTabs() { return context.SubTabs; }

        //public void UpdateTabRepository(IEnumerable<Tab> tabs, IEnumerable<SubTab> subTabs)
        //{
        //    foreach (var t in tabs) { context.Tabs.AddOrUpdate(t); }
        //    foreach (var s in subTabs) { context.SubTabs.AddOrUpdate(s); }
        //    context.SaveChanges();
        //}

        public void UpdateTabRepository(IEnumerable<Tab> tabs)
        {
            foreach (var t in tabs) { context.Tabs.AddOrUpdate(t); }
            context.SaveChanges();
        }

        //public void UpdateTabRepository(Tab tab, IEnumerable<SubTab> subTabs)
        //{
        //    context.Tabs.AddOrUpdate(tab); 
        //    foreach (var s in subTabs) { context.SubTabs.AddOrUpdate(s); }
        //    context.SaveChanges();
        //}
    }
}
