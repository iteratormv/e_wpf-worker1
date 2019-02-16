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
        void AddOrUpdateVisitor(VisitorDTO visitor);

        [OperationContract]
        void RemoveVisitor(VisitorDTO visitor);

        [OperationContract]
        void RemoveVisitorById(int Id);
    }
}
