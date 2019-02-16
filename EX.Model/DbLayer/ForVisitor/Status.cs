using System;

namespace EX.Model.DbLayer
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? ActionTime { get; set; }

        public int UserId { get; set; }
        public int VisitorId { get; set; }

        public User User { get; set; }
        public Visitor Visitor { get; set; }
    }
}
