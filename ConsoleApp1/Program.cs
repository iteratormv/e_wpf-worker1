using ConsoleApp1.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            StatusDTO status = new StatusDTO
            {
                Name = " ddd",
                UserId = 1,
                VisitorId = 111,
                ActionTime = DateTime.Now.ToString()
            };

            VisitorContractClient client = new VisitorContractClient();
            client.AddStatus(status);
        }
    }
}
