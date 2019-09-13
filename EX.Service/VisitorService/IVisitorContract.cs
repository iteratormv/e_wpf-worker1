using EX.Model.DbLayer;
using EX.Model.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace EX.Service.VisitorService
{
    [ServiceContract]
    interface IVisitorContract
    {
        [OperationContract]
        IEnumerable<VisitorDTO> GetAllVisitors();

        [OperationContract]
        VisitorDTO GetVisitorById(int Id);

        [OperationContract]
        VisitorDTO AddOrUpdateVisitor(VisitorDTO visitor);

        [OperationContract]
        void RemoveVisitor(VisitorDTO visitor);

        [OperationContract]
        void RemoveVisitorById(int Id);


        [OperationContract]
        void AddStatus(StatusDTO status);

        [OperationContract]
        void RemoveStatus(StatusDTO status);

        [OperationContract]
        IEnumerable<StatusDTO> GetAllStatuses();


        [OperationContract]
        void AddUserInRole(UserInRoleDTO userInRoleDTO);

        [OperationContract]
        void RemoveUserInRole(UserInRoleDTO userInRoleDTO);

        [OperationContract]
        IEnumerable<UserInRoleDTO> GetAllUserInRole();


        [OperationContract]
        void AddUser(UserDTO userDTO);

        [OperationContract]
        void RemoveUser(UserDTO userDTO);

        [OperationContract]
        IEnumerable<UserDTO> GetAllUsers();
    }
}
