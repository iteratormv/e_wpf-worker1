using AutoMapper;
using EX.Model.DbLayer;
using EX.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX.Model.Repositories.Administration
{
    public class CommandRepositoryDTO
    {
        IMapper mapper;
        CommandRepository commandRepository;

        public CommandRepositoryDTO()
        {
            commandRepository = new CommandRepository();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Command, CommandDTO>();
                cfg.CreateMap<CommandDTO, Command>();
            });
            mapper = configuration.CreateMapper();
        }

        public void AddCommandDTOsForCurrentRoles(int roleId)
        {
            commandRepository.AddCommandForCurrentRole(roleId);
        }

        public void RemoveCommandRepository(int roleId)
        {
            commandRepository.RemoveCommandRepository(roleId);
        }

        public IEnumerable<CommandDTO> GetAllCommands()
        {
            return commandRepository.GetAllCommands().Select(c => mapper.Map<CommandDTO>(c));
        }

        public void UpdateCommandRepository(IEnumerable<CommandDTO> commandDTOs)
        {
            List<Command> commands = commandDTOs.Select(c => mapper.Map<Command>(c)).ToList();
            commandRepository.UpdateCommandRepository(commands);
        }
    }
}
