using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:9999/CardService"),
                                      new X509CertificateEndpointIdentity(srvCert));

            using(CardServiceProxy proxy = new CardServiceProxy(binding, address))
            {
                //client-menu
                #region meni
                int choice;
                Console.WriteLine(@"
                                 _____        _                     _           _ _ _ 
                                |  __ \      | |                   | |         | (_) |
                                | |  | | ___ | |__  _ __ ___     __| | ___  ___| |_| |
                                | |  | |/ _ \| '_ \| '__/ _ \   / _` |/ _ \/ __| | | |
                                | |__| | (_) | |_) | | | (_) | | (_| | (_) \__ \ | |_|
                                |_____/ \___/|_.__/|_|  \___/   \__,_|\___/|___/_|_(_)
                ");
                Console.WriteLine("\n\n");
                do
                {
                    choice = 0;
                    Console.WriteLine("Unesite komandu: ");
                    Console.WriteLine(">1 Cash In  ~  2 Cash Out  ~  3 Change PIN");
                    Console.WriteLine("> ");
                    Int32.TryParse(Console.ReadLine(), out choice);

                    switch (choice)
                    {
                        case 1:
                            proxy.CashIn();
                            break;
                        case 2:
                            proxy.CashOut();
                            break;
                        case 3:
                            proxy.ListAllActiveUsers();
                            break;
                        default:
                            Console.WriteLine("Wrong command\n");
                            break;
                    }

                } while (true);

                #endregion
            }

        }
    }
}
