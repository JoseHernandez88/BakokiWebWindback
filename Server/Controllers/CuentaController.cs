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
                    var transaciones = await _context.Transacciones.Where(t => t.Cuenta == cue).ToListAsync();
                    var transaccionesViewModels = new List<TransactionViewModel>();
                    foreach (var tran in transaciones)
                    {
                        transaccionesViewModels.Add(new TransactionViewModel(tran));
                    }

					cuentaViewModels.Add(new CuentaViewModel(cue)
					{
						Transacciones = transaccionesViewModels

					});

                    
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
				foreach ( var cuenta in cuentas)
				{
                    var transaciones = await _context.Transacciones.Where(t => t.Cuenta == cuenta).ToListAsync();
                    var transaccionesViewModels = new List<TransactionViewModel>();
                    foreach (var tran in transaciones)
                    {
                        transaccionesViewModels.Add(new TransactionViewModel(tran));
                    }

					cuentaViewModels.Add(new CuentaViewModel(cuenta)
					{
						Transacciones = transaccionesViewModels

					});

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
		[HttpPut("put/closeAccount")]
		public async Task<ActionResult<CuentaViewModel>> PutCloseAccount([FromBody] Cuenta cuenta) 
		{
			var result = await _context.Cuentas.FindAsync(cuenta.AccountNumber);
			
			if (result != null)
			{
                result.IsOpen = false;
                await _context.SaveChangesAsync();
                return Ok(new CuentaViewModel(result));
            }
		 	
			return BadRequest("No such account."); 
		}

	}
}
