using AutoMapper;
using EX.Model.DbLayer.Settings;
using EX.Model.DTO.Setting;
using System.Collections.Generic;
using System.Linq;

namespace EX.Model.Repositories.Setting
{
    public class PrintSettingRepositoryDTO
    {
        IMapper mapper;
        PrintSettingRepository printSettingRepository;

        public PrintSettingRepositoryDTO()
        {
            printSettingRepository = new PrintSettingRepository();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PrintSetting, PrintSettingDTO>();
                cfg.CreateMap<PrintSettingDTO, PrintSetting>();
            });
            mapper = config.CreateMapper();
        }
        public PrintSettingDTO AddOrUpdate(PrintSettingDTO printSettingDTO)
        {
            PrintSetting printSetting = mapper.Map<PrintSetting>(printSettingDTO);
            if (printSetting != null)
            {
                printSettingDTO = mapper.Map<PrintSettingDTO>
                    (printSettingRepository.AddOrUpdatePrintSetting(printSetting));
            }
            return printSettingDTO;
        }
        public IEnumerable<PrintSettingDTO> GetAllPrintSettingDTOs()
        {
            var result = printSettingRepository.GetAllPrintSetting().
                Select(p => mapper.Map<PrintSettingDTO>(p)).ToList();
            return result;
        }
        public bool RemovePrintSettingDTO(PrintSettingDTO printSettingDTO)
        {
            bool result;
            try
            {
                printSettingRepository.RemovePrintSetting
                    (mapper.Map<PrintSetting>(printSettingDTO));
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
