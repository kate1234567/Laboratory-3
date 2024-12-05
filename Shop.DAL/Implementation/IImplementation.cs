using Shop.DAL.Entities;

namespace Shop.DAL.Implementation
{
    public interface IImplementation
    {
        public Task CreateNewShop(string title, string address);
        public Task CreateNewProduct(string title, double price);
        public Task CreateNewProduct(Product product);
        public Task CreateNewAssortment(Shop.DAL.Entities.Shop shop, Product product, int count = 1);

        public List<Shop.DAL.Entities.Shop> GetShops();
        public List<Product> GetProducts();
        public List<Assortment> GetAssortments();
    }
}
