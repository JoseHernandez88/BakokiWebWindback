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
		public async Task<ActionResult<List<TransactionViewModel>>> GetAlltransaccion()
		{
			try
			{
				var transacciones = await _context.Transacciones.ToListAsync();
				var transactionViewModels= new List<TransactionViewModel>();
				foreach(var tran in transacciones)
				{
					transactionViewModels.Add(new TransactionViewModel(tran));
				}
				return Ok(transactionViewModels);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("{accountNumber}")]
		public async Task<ActionResult<List<TransactionViewModel>>> GetAlltransaccionByCuenta(string accountNumber)
		{
			try
			{
				var transactionViewModels=new List<TransactionViewModel>();
				var transaciones = await _context.Transacciones.Where
					(
						tran=> 
							tran.Cuenta.AccountNumber.Equals(accountNumber) &&
							tran.Cuenta.IsOpen
					).ToListAsync();
				foreach(var tran in transaciones)
				{
					transactionViewModels.Add(new TransactionViewModel(tran));
				}
				return Ok(transactionViewModels);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost]
		public async Task<ActionResult<List<TransactionViewModel>>> PostCuenta(Transaccion tran)
		{
			_context.Transacciones.Add(tran);
			await _context.SaveChangesAsync();
			return Ok(new List<TransactionViewModel>() { new TransactionViewModel(tran) });
		}

	}
	

}
