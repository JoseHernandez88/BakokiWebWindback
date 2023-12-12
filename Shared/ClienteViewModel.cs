using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakokiWeb.Shared
{
    public class ClienteViewModel
    {
        public string Email { get; set; }
            = "";
        public string Password { get; set; }
            = "";
        public string FirstName { get; set; }
            = "";
        public string LastName { get; set; }
            = "";
        public bool LoggedIn { get; set; }
            = false;
        public string PhoneNumber { get; set; }
            = "";
        public string AddressLine1 { get; set; }
            = "";
        public string AddressLine2 { get; set; }
            = "";
        public List<CuentaViewModel> Cuentas { get; set; }
            = new List<CuentaViewModel>();
        public ClienteViewModel() { }
        public ClienteViewModel(Cliente cli) 
        {
            Email=cli.Email;
            Password=cli.Password;
            FirstName=cli.FirstName;
            LastName=cli.LastName;
            AddressLine1 = cli.AddressLine1;
            AddressLine2 = cli.AddressLine2;
            LoggedIn = cli.LoggedIn;
            PhoneNumber = cli.PhoneNumber;
            foreach(var cue in cli.Cuentas)
            {
                Cuentas.Add(new CuentaViewModel(cue));
            }
            
        }
    }
    
}
