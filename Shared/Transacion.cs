using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakokiWeb.Shared
{
	public class Transaccion
	{
		[Key]
		public Int64 TransactionID { get; set; }
		public Int64 Amount { get; set; }
		public bool IsCredit { get; set; }
		public DateTime FilledAt { get; set; }
		public string Origin { get; set; } 
			= "";
		public virtual Cuenta Cuenta { get; set; } 
			= new Cuenta();
		public Transaccion() { }
        public Transaccion(TransactionViewModel tran) 
		{
			TransactionID = tran.TransactionID;
			Amount=tran.Amount;
			Origin = tran.Origin;
			IsCredit = tran.IsCredit;
		}
        public Int64 SignedCentValue() 
		{ 
			return Amount * Convert.ToInt64((Math.Pow(-1, Convert.ToDouble(IsCredit)))); 
		}
		public Int64 Sum(Int64 value)
		{
			// This code 
			return value + this.SignedCentValue();
		}
		public Int64 Sum(Transaccion tran)
		{
			return tran.SignedCentValue()+this.SignedCentValue();
		}
		public double SignedDollarValue()
		{
			return this.SignedCentValue() / 100.0;
		}
	}
}
