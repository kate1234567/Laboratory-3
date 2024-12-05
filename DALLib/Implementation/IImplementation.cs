using DALLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALLib.Implementation
{
    public interface IImplementation
    {
        public Task CreateNewShop(string title, string address);
        public Task CreateNewProduct(string title, double price);
        public Task CreateNewProduct(Product product);
        public Task CreateNewAssortment(Shop shop, Product product, int count = 1);

        public List<Shop> GetShops();
        public List<Product> GetProducts();
        public List<Assortment> GetAssortments();
    }
}
