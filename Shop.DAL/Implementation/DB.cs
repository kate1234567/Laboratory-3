using Shop.DAL.Entities;
using System.Data.Entity;

namespace Shop.DAL.Implementation
{
    public class DB : DbContext, IImplementation
    {
        public DB() : base()
        {
        }
        public DB(string connectionString) : base(connectionString)
        {
            Database.CreateIfNotExists();
        }

        public DbSet<Shop.DAL.Entities.Shop> Shops { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Assortment> Assortments { get; set; }

        public async Task CreateNewAssortment(Shop.DAL.Entities.Shop shop, Product product, int count = 1)
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
            Shop.DAL.Entities.Shop shop = new Shop.DAL.Entities.Shop(title, address);
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

        public List<Shop.DAL.Entities.Shop> GetShops()
        {
            return Shops.ToList();
        }
    }
}
