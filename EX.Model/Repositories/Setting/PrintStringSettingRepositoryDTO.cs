using AutoMapper;
using EX.Model.DbLayer.Settings;
using EX.Model.DTO.Setting;
using System.Collections.Generic;
using System.Linq;

namespace EX.Model.Repositories.Setting
{
    public class PrintStringSettingRepositoryDTO
    {
        IMapper mapper;
        PrintStringSettingRepository printStringSettingRepository;

        public PrintStringSettingRepositoryDTO()
        {
            printStringSettingRepository = new PrintStringSettingRepository();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PrintStringSetting, PrintStringSettingDTO>();
                cfg.CreateMap<PrintStringSettingDTO, PrintStringSetting>();
            });
            mapper = config.CreateMapper();
        }
        public PrintStringSettingDTO AddOrUpdate
            (PrintStringSettingDTO printStringSettingDTO)
        {
            PrintStringSetting printStringSetting =
                mapper.Map<PrintStringSetting>(printStringSettingDTO);
            if (printStringSetting != null)
            {
                printStringSettingDTO = mapper.Map<PrintStringSettingDTO>
                    (printStringSettingRepository.AddOrUpdatePrintStringSetting
                    (printStringSetting));
            }
            return printStringSettingDTO;
        }
        public IEnumerable<PrintStringSettingDTO> GetAllPrintStringSettingDTOs()
        {
            var result = printStringSettingRepository.GetAllPrintStringSettings().
                Select(p => mapper.Map<PrintStringSettingDTO>(p)).ToList();
            return result;
        }
        public bool RemovePrintStringSettingDTO
            (PrintStringSettingDTO printStringSettingDTO)
        {
            bool result;
            try
            {
                printStringSettingRepository.RemovePrintStringSetting
                    (mapper.Map<PrintStringSetting>(printStringSettingDTO));
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
