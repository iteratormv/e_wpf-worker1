using AutoMapper;
using EX.Model.DbLayer;
using EX.Model.DTO;
using System.Collections.Generic;
using System.Linq;

namespace EX.Model.Repositories.Administration
{
    public class RoleRepositoryDTO
    {
        IMapper mapper;
        RoleRepository roleRepository;

        public RoleRepositoryDTO()
        {
            roleRepository = new RoleRepository();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Role, RoleDTO>();
                cfg.CreateMap<RoleDTO, Role>();
            });
            mapper = config.CreateMapper();
        }

        public RoleDTO AddOrUpdate(RoleDTO roleDTO)
        {
            Role role = mapper.Map<Role>(roleDTO);
            if(role != null)
            {
                roleDTO = mapper.Map<RoleDTO>(roleRepository.AddOrUpdate(role));
            }
            return roleDTO;
        }

        public IEnumerable<RoleDTO> GetAllRoles()
        {
            var result = roleRepository.GetAllRoles().Select(r => mapper.Map<RoleDTO>(r)).ToList();
            return result;
        }

        public bool RemoveRoleDTO(RoleDTO roleDTO)
        {
            bool result;
            try
            {
                roleRepository.RemoveRole(mapper.Map<Role>(roleDTO));
                result = true;
            } catch { result = false; }
            return result;
        }

    }
}
