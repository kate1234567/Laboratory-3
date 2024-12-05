using DALLib.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALLib.Implementation
{
    public class DB : DbContext, IImplementation
    {
        public DB():base()
        {

        }
        public DB(string connectionString):base(connectionString)
        {
            Database.CreateIfNotExists();
        }

        public DbSet<Shop> Shops { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Assortment> Assortments { get; set; }

        public async Task CreateNewAssortment(Shop shop, Product product, int count = 1)
        {
            Assortment assortment = new Assortment(shop, product, count);
            Assortments.Add(assortment);
            await SaveChangesAsync();
        }

        public async Task CreateNewProduct(string title, double price)
        {
            Product product = new Product(title, price);
            Products.Add(product);
            await SaveChangesAsync();
        }

        public async Task CreateNewProduct(Product product)
        {
            Products.Add(product);
            await SaveChangesAsync();
        }

        public async Task CreateNewShop(string title, string address)
        {
            Shop shop = new Shop(title, address);
            Shops.Add(shop);
            await SaveChangesAsync();
        }

        public List<Assortment> GetAssortments()
        {
            return Assortments.ToList();
        }

        public List<Product> GetProducts()
        {
            return Products.ToList();
        }

        public List<Shop> GetShops()
        {
            return Shops.ToList();
        }
    }
}
