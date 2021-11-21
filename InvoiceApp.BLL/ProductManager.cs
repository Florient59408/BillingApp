using InvoiceApp.BO;
using InvoiceApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.BLL
{
    public class ProductManager
    {

        Repository productRepository;
        public ProductManager(string clientName, int phoneNumber)
        {
            productRepository = new Repository(clientName, phoneNumber);
        }

        public void CreateProduct(Product product)
        {
            productRepository.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            productRepository.Delete(product);
        }

        public List<Product> GetAllProducts()
        {
            return productRepository.GetAll();
        }
    }
}
