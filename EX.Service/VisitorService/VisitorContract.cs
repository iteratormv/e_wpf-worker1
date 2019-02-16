using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using EX.Model.DbLayer;
using EX.Model.DTO;
using EX.Model.Repositories;

namespace EX.Service.VisitorService
{

    public class VisitorContract : IVisitorContract
    {
        VisitorRepositoryDTO visitorRepositoryDTO;

        public VisitorContract()
        {
            visitorRepositoryDTO = new VisitorRepositoryDTO();
        }

        public void AddOrUpdateVisitor(VisitorDTO visitor)
        {
            visitorRepositoryDTO.AddOrUpdateVisitor(visitor);
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
    }
}
