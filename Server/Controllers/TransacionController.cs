using BakokiWeb.Server.Data;
using BakokiWeb.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BakokiWeb.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TransacionController : ControllerBase
	{
		private readonly DataContext _context;
		public TransacionController(DataContext context)
		{
			_context = context;
		}
		[HttpGet]
		public async Task<ActionResult<List<Transacion>>> GetAllTransacion()
		{
			try
			{
				var list = await _context.Transaciones.ToListAsync();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("{accountNumber}")]
		public async Task<ActionResult<List<Transacion>>> GetAllTransacionByCuenta(string accountNumber)
		{
			try
			{
				var list = await _context.Transaciones.Where
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
		public async Task<ActionResult<List<Transacion>>> PostCuenta(Transacion tran)
		{
			_context.Transaciones.Add(tran);
			await _context.SaveChangesAsync();
			return Ok(new List<Transacion>() { tran });
		}

	}
	

}
