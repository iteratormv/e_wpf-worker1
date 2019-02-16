using AutoMapper;
using EX.Model.DbLayer;
using EX.Model.DTO;
using System.Collections.Generic;
using System.Linq;

namespace EX.Model.Repositories.Administration
{
    public class UserInRoleRepositoryDTO
    {
        IMapper mapper;
        UserInRoleRepository userInRoleRepository;

        public UserInRoleRepositoryDTO()
        {
            userInRoleRepository = new UserInRoleRepository();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserInRole, UserInRoleDTO>();
                cfg.CreateMap<UserInRoleDTO, UserInRole>();
            });
            mapper = config.CreateMapper();
        }

        public UserInRoleDTO AddOrUpdate(UserInRoleDTO userInRoleDTO)
        {
            UserInRole userInRole = mapper.Map<UserInRole>(userInRoleDTO);
            if(userInRole != null)
            {
                userInRoleDTO = mapper.Map<UserInRoleDTO>
                    (userInRoleRepository.AddOrUpdateUserInRole(userInRole));
            }
            return userInRoleDTO;
        }

        public IEnumerable<UserInRoleDTO> GetAllUserInRolesDTOs()
        {
            var result = userInRoleRepository.GetAllUserInRoles().Select(ur =>
            mapper.Map<UserInRoleDTO>(ur)).ToList();
            return result;
        }

        public bool RemoveUserInRoleDTO(UserInRoleDTO userInRoleDTO)
        {
            bool result = false;
            //try
            //{
            //    userInRoleRepository.RemoveUserInRoles
            //        (mapper.Map<UserInRole>(userInRoleDTO));
            //    result = true;
            //} catch { result = false; }
            userInRoleRepository.RemoveUserInRoles
                    (mapper.Map<UserInRole>(userInRoleDTO));
            return result;
        }
    }
}
