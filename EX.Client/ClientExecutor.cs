using EX.Client.ServiceReference1;

namespace EX.Client
{
    public class ClientExecutor
    {
        VisitorContractClient client;

        public ClientExecutor()
        {
            client = new VisitorContractClient();
        }

        public VisitorContractClient GetClient() { return client; }
    }
}
