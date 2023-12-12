using BakokiWeb.Server.Data;
using BakokiWeb.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BakokiWeb.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TransaccionController : ControllerBase
	{
		private readonly DataContext _context;
		public TransaccionController(DataContext context)
		{
			_context = context;
		}
		[HttpGet]
		public async Task<ActionResult<List<transaccion>>> GetAlltransaccion()
		{
			try
			{
				var list = await _context.transacciones.ToListAsync();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("{accountNumber}")]
		public async Task<ActionResult<List<transaccion>>> GetAlltransaccionByCuenta(string accountNumber)
		{
			try
			{
				var list = await _context.transacciones.Where
					(
						tran=> 
							tran.Cuenta.AccountNumber.Equals(accountNumber) &&
							tran.Cuenta.IsOpen
					).ToListAsync();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost]
		public async Task<ActionResult<List<transaccion>>> PostCuenta(transaccion tran)
		{
			_context.transacciones.Add(tran);
			await _context.SaveChangesAsync();
			return Ok(new List<transaccion>() { tran });
		}

	}
	

}
