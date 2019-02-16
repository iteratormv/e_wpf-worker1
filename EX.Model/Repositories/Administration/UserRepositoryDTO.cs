using AutoMapper;
using EX.Model.DbLayer;
using EX.Model.DTO;
using System.Collections.Generic;
using System.Linq;

namespace EX.Model.Repositories.Administration
{
    public class UserRepositoryDTO
    {
        IMapper mapper;
        UserRepository userRepository;

        public UserRepositoryDTO()
        {
            userRepository = new UserRepository();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
            });
            mapper = config.CreateMapper();
        }

        public UserDTO AddOrUpdate(UserDTO userDTO)
        {
            User user = mapper.Map<User>(userDTO);
            if(user != null){ userDTO = mapper.Map<UserDTO>
                    (userRepository.AddOrUpdateUser(user)); }
            return userDTO;
        }

        public IEnumerable<UserDTO> GetUserDTOs()
        {
            var result = userRepository.GetAllUsers().Select(u => mapper.Map<UserDTO>(u)).ToList();
            return result;
        }

        public bool RemoveUserDTO(UserDTO userDTO)
        {
            bool result;
            try
            {
                userRepository.RemoveUser(mapper.Map<User>(userDTO));
                result = true;
            } catch { result = false; }
            return result;
        }
    }
}
