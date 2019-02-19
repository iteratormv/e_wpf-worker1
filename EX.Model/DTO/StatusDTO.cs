using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EX.Model.DTO
{
    [DataContract(Namespace = "http://iterator.org")]
    public class StatusDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string ActionTime { get; set; }

        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int VisitorId { get; set; }
    }
}
