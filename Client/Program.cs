using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            /// Define the expected service certificate. 
            string srvCertCN = "wcfservice";

            /// Define the expected certificate for signing ("<username>_sign" is the expected subject name).
            /// .NET WindowsIdentity class provides information about Windows user running the given process
            string signCertCN = String.Empty;

            /// Define subjectName for certificate used for signing which is not as expected by the service
            string wrongCertCN = String.Empty;

            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/CardService";

            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            

        }
    }
}
