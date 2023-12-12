using BakokiWeb.Server.Data;
using BakokiWeb.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BakokiWeb.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CuentaController : ControllerBase
	{
		private readonly DataContext _context;
		public CuentaController(DataContext context)
		{
			_context = context;
		}
		[HttpGet]
		public async Task<ActionResult<List<CuentaViewModel?>>> GetAllCuentas()
		{
			try
			{
				
				var cuentaViewModels= new List<CuentaViewModel>();
				var cuentas = await _context.Cuentas.ToListAsync();
				
				foreach(var cuenta in cuentas)
				{
					var transaciones = await _context.Transacciones.Where(t => t.Cuenta==cuenta).ToListAsync();
					var transaccionesViewModels = new List<TransactionViewModel>();
					foreach(var tran in transaciones)
					{
						transaccionesViewModels.Add(new TransactionViewModel(tran));
					}

					cuentaViewModels.Add(new CuentaViewModel(cuenta)
					{
						Transacciones = transaccionesViewModels
						
					}
					) ;
					
				}

				return Ok(cuentaViewModels);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("get/{email}")]
		public async Task<ActionResult<List<CuentaViewModel?>>> GetAllCuentasByCliente(string email)
		{
			try
			{
				var cuentaViewModels=new List<CuentaViewModel>(); 
				var cuentas = await _context.Cuentas.Where
				(
					cnt => cnt.IsOpen &&
					//cnt.Cliente.LoggedIn &&
					cnt.Cliente.Email.Equals(email)
				).ToListAsync();
				foreach (var cue in cuentas)
				{
					cuentaViewModels.Add(new CuentaViewModel(cue));
				}
				return Ok(cuentaViewModels);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("{accountNumber}")]
		public async Task<ActionResult<List<CuentaViewModel?>>> GetCuentaByAccountNumber(string accountNumber)
		{
			try
			{
				var cuentaViewModels= new List<CuentaViewModel>();
				var cuentas = await _context.Cuentas.Where
				(
					cnt => cnt.IsOpen &&
					//cnt.Cliente.LoggedIn &&
					cnt.AccountNumber.Equals(accountNumber)
				).ToListAsync();
				foreach ( var cue in cuentas)
				{
					cuentaViewModels.Add(new CuentaViewModel(cue));
				}

				return Ok(cuentaViewModels);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost]
		public async Task<ActionResult<List<CuentaViewModel?>>> PostCuenta(Cuenta cuenta)
		{
			_context.Cuentas.Add(cuenta);
			await _context.SaveChangesAsync();
			return Ok(new List<CuentaViewModel?>() { new CuentaViewModel(cuenta) });
		}
		[HttpPut("{accountNumber}/{email}")]
		public async Task<ActionResult<List<bool>>> PutCloseAccount(string accountNumber, string email) 
		{

			var cuenta = await _context.Cuentas.FindAsync(accountNumber);
			var cliente =await _context.Clientes.FindAsync(email);
					

			if (cuenta != null)
			{
				if (cliente!=null && cliente.Cuentas.Contains(cuenta))
				{
					cuenta.IsOpen = false;
					await _context.SaveChangesAsync();
					return new List<bool>() { true };
				}
				else
				{
					return new List<bool> { false };
				}
			}
		 	
			return BadRequest("No such account."); 
		}

	}
}
