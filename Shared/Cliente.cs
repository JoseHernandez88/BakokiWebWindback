using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GemBox.Email;
using GemBox.Email.Smtp;

namespace BakokiWeb.Shared
{
	public class Cliente
	{
		[Key]
		public string Email { get; set; }
			= "";
		public string Password { get; set; } 
			= "";
		public string FirstName { get; set; }
			= "";
		public string LastName { get; set; }
			= "";
		public bool LoggedIn { get; set; }
			=false;
		public string PhoneNumber { get; set; }
			= "";
		public string AddressLine1 { get; set; } 
			= "";
		public string AddressLine2 { get; set; }
			= "";
		public ICollection<Cuenta> Cuentas { get; set; }
			= new List<Cuenta>();
		public Cliente() { }
        public void OrderBook(Cuenta cuenta,string Reason)
        {
            MailMessage message = new MailMessage(
				new MailAddress("bankoki.services@gmail.com", "Sender"),
				new MailAddress("bankoki.services@gmail.com",
				"First receiver"));
            message.Subject = "How to send an Email programmatically in C#  ASP.NET";
            message.BodyText = "Hi,\n" +
             $"Client {FirstName} {LastName}, {Email}.\n" +
			 $"wishes to order a new check book for their {cuenta.AccountName} account " +
			 $"with account number {cuenta.AccountNumber}. For the following reason: " +
			 $"{Reason}. Please mail the book to: \n" +
			 $"{FirstName} {LastName}\n" +
			 $"{AddressLine1}\n" +
			 $"{AddressLine2}\n" +
			 $"And contat them to {PhoneNumber} in case of any isssues." +
			 $"Best Wishes,\n" +
			 $"Your banking app alway Bankoki." +
             "Read more about it on https://www.gemboxsoftware.com/email";

            using (SmtpClient smtp = new SmtpClient("bankoki.services@gmail.com"))
            {
                smtp.Connect();
                smtp.Authenticate("bankoki.services@gmail.com", "<Algobuenoyfacil01>");
                smtp.SendMessage(message);
            }
        }
    }


}
