using InvoiceApp.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.WinForms
{
    public class Invoice
    {
        public string CompanyName { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyLocation { get; set; }
        public string Date { get; set; }
        public string ClientName { get; set; }
        public string ClientPhoneNumber { get; set; }
        public int ClientNumber { get; set; }

        public List<Product> Products { get; set; }

        public Invoice(string clientName, string clientPhoneNumber, List<Product> products)
        {
            
            CompanyName = Program.CompanyName;
            CompanyEmail = Program.CompanyEmail;
            CompanyPhone = Program.CompanyPhone;
            CompanyLocation = Program.CompanyLocation;
            ClientName = clientName;
            ClientPhoneNumber = clientPhoneNumber;
            ClientNumber = Program.ClientNumber;
            Products = products;
            Date = DateTime.Now.ToString("dd-MM-yyyy H:mm:ss");
        }
    }
}
