using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.BO
{
    [Serializable]
    public class Product
    {
        public string Reference { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Qantity { get; set; }

        public Product()
        {

        }

        public Product(string reference, string name, double price, int qantity)
        {
            Reference = reference;
            Name = name;
            Price = price;
            Qantity = qantity;
        }
        public Product(Product product):this(product.Reference, product.Name, product.Price, product.Qantity)
        {

        }

        public override bool Equals(object obj)
        {
            return obj is Product product &&
                   Reference == product.Reference;
        }

        public override int GetHashCode()
        {
            return -1304721846 + EqualityComparer<string>.Default.GetHashCode(Reference);
        }
    }
}
