using AutoMapper;
using EX.Model.DbLayer.Settings;
using EX.Model.DTO.Setting;
using System.Collections.Generic;
using System.Linq;

namespace EX.Model.Repositories.Setting
{
    public class DSCollumnSettingDTORepository
    {
        IMapper mapper;
        DSCollumnSettingRepository dSCollumnSettingRepository;

        public DSCollumnSettingDTORepository()
        {
            dSCollumnSettingRepository = new DSCollumnSettingRepository();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DSCollumnSetting, DSCollumnSettingDTO>();
                cfg.CreateMap<DSCollumnSettingDTO, DSCollumnSetting>();
            });
            mapper = config.CreateMapper();
        }

        public DSCollumnSettingDTO AddOrUpdate(DSCollumnSettingDTO dSCollumnSettingDTO)
        {
            DSCollumnSetting dSCollumnSetting = mapper.Map<DSCollumnSetting>(dSCollumnSettingDTO);
            if(dSCollumnSetting != null)
            {
                dSCollumnSettingDTO = mapper.Map<DSCollumnSettingDTO>
                    (dSCollumnSettingRepository.AddOrUpdateDSCollumnSetting(dSCollumnSetting));
            }
            return dSCollumnSettingDTO;
        }

        public IEnumerable<DSCollumnSettingDTO> GetAllDSCollumnSettingDTOs()
        {
            var result = dSCollumnSettingRepository.GetAllDSCollumnSettings().
                Select(d => mapper.Map<DSCollumnSettingDTO>(d)).ToList();
            return result;
        }

        public bool RemoveDSCollumnSettingDTO(DSCollumnSettingDTO dSCollumnSettingDTO)
        {
            bool result;
            try
            {
                dSCollumnSettingRepository.RemoveDSCollumnSetting
                    (mapper.Map<DSCollumnSetting>(dSCollumnSettingDTO));
                result = true;
            }catch { result = false; }
            return result;
        }

    }
}
