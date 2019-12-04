using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Ugovor koji ATM pruza klijentu
    /// </summary>
    [ServiceContract]
    public interface IATMClientService
    {
        [OperationContract]
        void CashIn();

        [OperationContract]
        double CashOut();

        [OperationContract]
        List<string> ListAllActiveUsers();
    }
}
