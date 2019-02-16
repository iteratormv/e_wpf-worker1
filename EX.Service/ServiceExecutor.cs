using EX.Service.VisitorService;
using System;
using System.ServiceModel;

namespace EX.Service
{
    public class ServiceExecutor
    {
        ServiceHost host;
        public string status;
        public event Action<string> statusChanged;

        public void Start()
        {
            host = new ServiceHost(typeof(VisitorContract));
            host.Open();
            status = "listerning....";
            statusChanged(status);
        }

        public void Stop()
        {
            host.Close();
            status = "close" + "\n";
            statusChanged(status);
        }
    }
}
