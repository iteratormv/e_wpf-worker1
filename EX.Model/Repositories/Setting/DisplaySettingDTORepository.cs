using AutoMapper;
using EX.Model.DbLayer.Settings;
using EX.Model.DTO.Setting;
using System.Collections.Generic;
using System.Linq;

namespace EX.Model.Repositories.Setting
{
    public class DisplaySettingDTORepository
    {
        IMapper mapper;
        DisplaySettingRepository displaySettingRepository;

        public DisplaySettingDTORepository()
        {
            displaySettingRepository = new DisplaySettingRepository();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DisplaySetting, DisplaySettingDTO>();
                cfg.CreateMap<DisplaySettingDTO, DisplaySetting>();
            });
            mapper = config.CreateMapper();
        }

        public DisplaySettingDTO AddOrUpdate(DisplaySettingDTO displaySettingDTO)
        {
            DisplaySetting displaySetting = mapper.Map<DisplaySetting>(displaySettingDTO);
            if(displaySetting != null)
            {
                displaySettingDTO = mapper.Map<DisplaySettingDTO>
                    (displaySettingRepository.AddOrUpdateDisplaySetting(displaySetting));
            }
            return displaySettingDTO;
        }

        public IEnumerable<DisplaySettingDTO> GetAllDisplaySettingDTOs()
        {
            var result = displaySettingRepository.GetAllDisplaySettings().
                Select(d => mapper.Map<DisplaySettingDTO>(d)).ToList();
            return result;
        }

        public bool RemoveDisplaySettingDTO(DisplaySettingDTO displaySettingDTO)
        {
            bool result;
            try
            {
                displaySettingRepository.RemoveDisplaySetting
                    (mapper.Map<DisplaySetting>(displaySettingDTO));
                result = true;
            } catch { result = false; }
            return result;
        }
    }
}
