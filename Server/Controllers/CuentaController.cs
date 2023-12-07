using BakokiWeb.Server.Data;
using BakokiWeb.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
		public async Task<ActionResult<List<Cuenta?>>> GetAllCuentas()
		{
			try
			{
				var list = await _context.Cuentas.ToListAsync();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("{email}")]
		public async Task<ActionResult<List<Cuenta?>>> GetAllCuentasByCliente(string email)
		{
			try
			{
				var list = await _context.Cuentas.Where
				(
					cnt => cnt.IsOpen &&
					cnt.Cliente.LoggedIn &&
					cnt.Cliente.Email.Equals(email)
				).ToListAsync();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("{accountNumber}")]
		public async Task<ActionResult<List<Cuenta?>>> GetCuentaByAccountNumber(string accountNumber)
		{
			try
			{
				var list = await _context.Cuentas.Where
				(
					cnt => cnt.IsOpen &&
					cnt.Cliente.LoggedIn &&
					cnt.AccountNumber.Equals(accountNumber)
				).ToListAsync();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost]
		public async Task<ActionResult<List<Cuenta?>>> PostCuenta(Cuenta cuenta)
		{
			_context.Cuentas.Add(cuenta);
			await _context.SaveChangesAsync();
			return Ok(new List<Cuenta?>() { cuenta });
		}
		[HttpPut("{accountNumber}/{password}")]
		public async Task<ActionResult<List<bool>>> PutCloseAccount(string accountNumber, string password) 
		{
			var list = new List<Cuenta?> { };
			var cuenta = new Cuenta();
			var wrapper= await GetCuentaByAccountNumber(accountNumber);
			if (wrapper != null && wrapper.Value != null)
			{
				list=wrapper.Value.ToList();
				if (list.Any())
				{
					cuenta = list.FirstOrDefault();
				}
			}
			
			if (cuenta != null)
			{
				if (cuenta.Cliente.Password.Equals(password))
				{
					cuenta.IsOpen = false;
					await _context.SaveChangesAsync();
					return new List<bool>() { true };
				}
				else
				{
					return new List<bool> { };
				}
			}
		 	
			return BadRequest("No such account."); 
		}

	}
}
