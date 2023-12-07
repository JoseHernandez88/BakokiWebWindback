using BakokiWeb.Server.Data;
using BakokiWeb.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BakokiWeb.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClienteController : ControllerBase
	{
		private readonly DataContext _context;

		public ClienteController(DataContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<List<Cliente?>>> GetAllClient()
		{
			try
			{
				var list = await _context.Clientes.ToListAsync();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("{email}")]
		public async Task<ActionResult<List<Cliente?>>> GetClienteByEmail(string email)
		{
			var cli = await _context.Clientes.FindAsync(email);

			if (cli == null)
			{
				//Console.WriteLine("CetCli by email was null.");
				return Ok(new List<Cliente?>() { null });
			}
			else
			{
				//Console.WriteLine(cli.ToString());
				return Ok(new List<Cliente?>() { cli });
			}
			
		}
		[HttpGet("{password}/{email}")]
        public async Task<ActionResult<List<Cliente?>>> GetClienteVerifyEmail(string email,string password)
        {
            
            var cli = await _context.Clientes.FindAsync(email);


            if (cli != null)
            {
                if (cli.Password.Equals(password))
                {
                    //Console.WriteLine(cli.ToString());
                    return Ok(new List<Cliente?>() { cli });
                }    
            }
			return Ok(new List<Cliente?>() { null });
        }
      
        [HttpPut("put/1/{email}")]
		public async Task<ActionResult<Cliente?>> LoggedIn(string email)
		{
            var cli = await _context.Clientes.FindAsync(email);

			if (cli != null)
			{
				Console.WriteLine(cli.Email);
				cli.LoggedIn = true;
				await _context.SaveChangesAsync();
				return Ok(cli);
			}
			else
			{
                Console.WriteLine("email not found");
                return BadRequest($"Cliente Controller:Could not find {email}");
			}
		}
		[HttpPut("put/0/{email}")]
		public async Task<ActionResult<Cliente?>> Loggedoff(string email)
		{
            var cli = await _context.Clientes.FindAsync(email);

            
			if (cli != null)
			{
				cli.LoggedIn = false;
				await _context.SaveChangesAsync();
				return Ok(cli);
			}
			else
			{
				return BadRequest($"Cliente Controller:Could not find {email}");
			}
		}
		[HttpPost]
		public async Task<ActionResult<Cliente?>> PostCuenta(Cliente cliente)
		{
			_context.Clientes.Add(cliente);
			await _context.SaveChangesAsync();

			return Ok(cliente);
		}

	}
}
