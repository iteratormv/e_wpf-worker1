using AutoMapper;
using EX.Model.DbLayer;
using EX.Model.DTO;
using EX.Model.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EX.Model.Repositories.ForVisitor
{
    public class StatusRepositoryDTO
    {
        IMapper mapper;
        StatusRepository statusRepository;

        public StatusRepositoryDTO()
        {
            statusRepository = new StatusRepository();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Status, StatusDTO>();
                cfg.CreateMap<StatusDTO, Status>();
            });
            mapper = config.CreateMapper();
        }
        public IEnumerable<StatusDTO> GetAllStatuses()
        {
            var st = statusRepository.GetAllStatuses().
                Select(s => mapper.Map<StatusDTO>(s));
            return st;
        }
        public void RemoveStatusDTO(StatusDTO statusDTO)
        {
            statusRepository.RemoveStatus(
                mapper.Map<Status>(statusDTO));
        }


        public void ImportStatusRepositoryFromFileWithId(string filemame)
        {
            statusRepository.importStatusRepositoryFromFileWithId(filemame, progressChanged);
        }



        public StatusDTO Add(StatusDTO statusDTO)
        {
            var status = mapper.Map<Status>(statusDTO);
            if (status != null)
            {
                statusDTO = mapper.Map<StatusDTO>
                    (statusRepository.Add
                    (status));
            }
            return statusDTO;
        }
        public void SaveStatusestoFile()
        {
            statusRepository.SaveStatusesToFile();
        }


        public event Action<Progress_Bar> progressChanged;

    }
}
