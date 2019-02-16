using AutoMapper;
using EX.Model.DbLayer;
using EX.Model.DTO;
using System.Collections.Generic;
using System.Linq;

namespace EX.Model.Repositories.Administration
{
    public class TabRepositoryDTO
    {
        IMapper mapper;
        TabRepository tabRepository;

        public TabRepositoryDTO()
        {
            tabRepository = new TabRepository();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Tab, TabDTO>();
                cfg.CreateMap<TabDTO, Tab>();
            });
            mapper = config.CreateMapper();
        }

        public void AddTabDTOsForCurrentRole(int roleId)
        {
            tabRepository.AddTabsForCurrentRole(roleId);
        }

        public bool RomoveCurrentTabRepository(int roleId)
        {
            bool result;
            try
            {
                tabRepository.RemoveCurrentTabRepository(roleId);
                result = true;
            } catch { result = false; }
            return result;
        }

        public IEnumerable<TabDTO> GetTabDTOs()
        {
            var result = tabRepository.GetAllTabs().Select(t => mapper.Map<TabDTO>(t)).ToList();
            return result;
        }

        //public IEnumerable<SubTabDTO> GetSubTabDTOs(TabDTO tabDTO)
        //{
        //    var tab = mapper.Map<Tab>(tabDTO);
        //    var result = tabRepository.GetSubTubs(tab).Select(s => mapper.Map<SubTabDTO>(s)).ToList();
        //    return result;
        //}

        //public IEnumerable<SubTabDTO> GetAllSubTabDTOs()
        //{
        //    var result = tabRepository.GetAllSubTabs().Select(s => mapper.Map<SubTabDTO>(s)).ToList();
        //    return result;
        //}


        //public void UpdateTabRepository(IEnumerable<TabDTO> tabDTOs, IEnumerable<SubTabDTO> subTabDTOs)
        //{
        //    List<Tab> tabs = tabDTOs.Select(t => mapper.Map<Tab>(t)).ToList();
        //    List<SubTab> subTabs = subTabDTOs.Select(s => mapper.Map<SubTab>(s)).ToList();

        //    tabRepository.UpdateTabRepository(tabs, subTabs);
        //}

        public void UpdateTabRepository(IEnumerable<TabDTO> tabDTOs)
        {
            List<Tab> tabs = tabDTOs.Select(t => mapper.Map<Tab>(t)).ToList();
            tabRepository.UpdateTabRepository(tabs);
        }
    }
}
