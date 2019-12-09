using Common;
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
    public class CardServiceProxy : ChannelFactory<IATMClientService>, IATMClientService, IDisposable
    {

        IATMClientService factory;

        public CardServiceProxy(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            /// cltCertCN.SubjectName should be set to the client's username. .
            /// NET WindowsIdentity class provides information about Windows user running the given process
			string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            /// Set appropriate client's certificate on the channel. Use CertManager class to obtain the certificate based on the "cltCertCN"
			this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);

            factory = this.CreateChannel();

        }

        /// <summary>
        /// Here we implement our interface
        /// </summary>

        public void CashIn()
        {
            try
            {
                factory.CashIn();
            }catch(Exception e)
            {
                Console.WriteLine("[FAILED] to Cash in... Error = {0}", e.Message);
            }
        }

        public double CashOut()
        {
            try
            {
                return factory.CashOut();
            }catch(Exception e)
            {
                Console.WriteLine("[FAILED] to Cash out... Error = {0}", e.Message);
                return -1;
            }
        }

        

        public List<string> ListAllActiveUsers()
        {
            try
            {
                return factory.ListAllActiveUsers();
            }catch(Exception e)
            {
                Console.WriteLine("[FAILED] to list all active users... Error = {0}", e.Message);
                return new List<string>() { };
            }
        }


        public void Dispose()
        {
            if (factory != null)
            {
                factory = null;
            }

            this.Close();
        }
    }
}
