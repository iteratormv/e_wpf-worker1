using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using EX.Model.DbLayer;
using EX.Model.DTO;
using EX.Model.Repositories;
using EX.Model.Repositories.Administration;
using EX.Model.Repositories.ForVisitor;

namespace EX.Service.VisitorService
{

    public class VisitorContract : IVisitorContract
    {
        VisitorRepositoryDTO visitorRepositoryDTO;
        StatusRepositoryDTO statusRepositoryDTO;
        UserInRoleRepositoryDTO userInRoleRepositoryDTO;
        UserRepositoryDTO userRepositoryDTO;

        public VisitorContract()
        {
            visitorRepositoryDTO = new VisitorRepositoryDTO();
            statusRepositoryDTO = new StatusRepositoryDTO();
            userInRoleRepositoryDTO = new UserInRoleRepositoryDTO();
        }
        public VisitorDTO AddOrUpdateVisitor(VisitorDTO visitor)
        {
            return visitorRepositoryDTO.AddOrUpdateVisitor(visitor);
        }
        public IEnumerable<VisitorDTO> GetAllVisitors()
        {
            return visitorRepositoryDTO.GetAllVisitors();
        }
        public VisitorDTO GetVisitorById(int Id)
        {
            return visitorRepositoryDTO.GetVisitorById(Id);
        }
        public void RemoveVisitor(VisitorDTO visitor)
        {
            visitorRepositoryDTO.RemoveVisitorDTO(visitor);
        }
        public void RemoveVisitorById(int Id)
        {
            visitorRepositoryDTO.RemoveVisitorById(Id);
        }

        public void AddStatus(StatusDTO status)
        {
            statusRepositoryDTO.Add(status);
        }
        public void RemoveStatus(StatusDTO status)
        {
            statusRepositoryDTO.RemoveStatusDTO(status);
        }
        public IEnumerable<StatusDTO> GetAllStatuses()
        {
            return statusRepositoryDTO.GetAllStatuses();
        }

        public void AddUserInRole(UserInRoleDTO userInRoleDTO)
        {
            userInRoleRepositoryDTO.AddOrUpdate(userInRoleDTO);
        }
        public void RemoveUserInRole(UserInRoleDTO userInRoleDTO)
        {
            userInRoleRepositoryDTO.RemoveUserInRoleDTO(userInRoleDTO);
        }
        public IEnumerable<UserInRoleDTO> GetAllUserInRole()
        {
            return userInRoleRepositoryDTO.GetAllUserInRolesDTOs();
        }

        public void AddUser(UserDTO userDTO)
        {
            userRepositoryDTO.AddOrUpdate(userDTO);
        }
        public void RemoveUser(UserDTO userDTO)
        {
            userRepositoryDTO.RemoveUserDTO(userDTO);
        }
        public IEnumerable<UserDTO> GetAllUsers()
        {
            return userRepositoryDTO.GetUserDTOs();
        }
    }
}
