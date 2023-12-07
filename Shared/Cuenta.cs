using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BakokiWeb.Shared
{
	public class Cuenta
	{
		[Key]
		public string AccountNumber { get; set; }
			= "";
		public string AccountName { get; set; }
			= "";
		public bool IsOpen { get; set; }
			= true;
		public Cliente Cliente { get; set; }
			= new Cliente();
		public ICollection<Transacion> Transaciones { get; set; }
			= new List<Transacion>();

		public Cuenta() { }
		public double Balance()
		{
			Int64 sum = 0;
			if (Transaciones.Any())
			{
				foreach (var t in Transaciones)
				{
					sum = t.Sum(sum);
				}
			}

			return sum / 100.0;
		}
		public async Task<bool> TransferFrom(Cuenta cuentaFrom, Int64 signedCentAmount, HttpClient Http)
		{
			if
			(
				cuentaFrom.Balance() >= signedCentAmount / 100.0 &&
				cuentaFrom.Cliente.Email.Equals(this.Cliente.Email)
			)
			{
				var too = new Transacion()
				{
					Amount = Math.Abs(signedCentAmount),
					Cuenta = this,
					Origin = $"Transfer from {this.AccountNumber} {this.AccountName} account.",
					FilledAt = DateTime.Now,
					IsCredit = signedCentAmount >= 0
				};
				var from = new Transacion()
				{
					Amount = Math.Abs(signedCentAmount),
					Cuenta = this,
					Origin = $"Transfer too {cuentaFrom.AccountNumber} {this.AccountName} account.",
					FilledAt = DateTime.Now,
					IsCredit = signedCentAmount < 0,
				};
				if (from != null && too != null)
				{
					await Http.PostAsJsonAsync<Transacion>("Trancsacion", too);
					await Http.PostAsJsonAsync<Transacion>("Trancsacion", from);
					return true;
				}	
				return false;
			}
			return false;
		}
		
	}
}
		