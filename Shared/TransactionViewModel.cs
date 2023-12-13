using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakokiWeb.Shared
{
    public class TransactionViewModel
    {
        public Int64 TransactionID { get; set; }
        public Int64 Amount { get; set; }
        public bool IsCredit { get; set; }
        public DateTime FilledAt { get; set; }
        public string Origin { get; set; }
            = "";
        public TransactionViewModel() { }
        public TransactionViewModel(Transaccion tran)
        {
            TransactionID = tran.TransactionID;
            Amount = tran.Amount;
            IsCredit = tran.IsCredit;
            FilledAt = tran.FilledAt;
            Origin = tran.Origin;
        }
        public double SignedDollarAmount()
        {
            return (-1 * Amount * Math.Pow(-1, Convert.ToDouble(IsCredit)));
        }
    }
}
