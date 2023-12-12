using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakokiWeb.Shared
{
    public class CuentaViewModel
    {
        public string AccountNumber { get; set; }
            = "";
        public string AccountName { get; set; }
            = "";
        public bool IsOpen { get; set; }
            = true;
        public List<TransactionViewModel> Transacciones { get; set; }
            = new List<TransactionViewModel>();
        public CuentaViewModel() { }
        public CuentaViewModel(Cuenta Cue)
        {
            AccountNumber = Cue.AccountNumber;
            AccountName = Cue.AccountName;
            IsOpen = Cue.IsOpen;
            foreach(var tran in Cue.Transacciones)
            {
                Transacciones.Add(new TransactionViewModel(tran));
            }
        }
    }
}
