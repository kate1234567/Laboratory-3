using Microsoft.Extensions.Configuration;
using Shop.DAL.Entities;
using Shop.DAL.Implementation;

namespace Shop.DAL
{
    public class DataModel
    {
        private static TypeImplementation _currentImplementation;
        private static IConfiguration _configuration;
        private static IImplementation _implementation;


        public DataModel(IConfiguration configuration)
        {
            _configuration = configuration;
            var currentImplementation = _configuration.GetSection("CurrentImplementation")["Implementation"];
            if (currentImplementation == "DB")
            {
                new DataModel(TypeImplementation.DB);
            }
            else
            {
                new DataModel(TypeImplementation.File);
            }
        }

        public enum TypeImplementation
        {
            File,
            DB
        }

        public IImplementation GetImplementation()
        {
            return _implementation;
        }

        private DataModel(TypeImplementation typeImplementation)
        {
            switch (typeImplementation)
            {
                case TypeImplementation.File:
                    _currentImplementation = TypeImplementation.File;
                    var path = _configuration.GetSection("FileImplementation")["FolderFiles"];
                    _implementation = new FileImp(path);
                    break;
                case TypeImplementation.DB:
                    _currentImplementation = TypeImplementation.DB;
                    var connectionString = _configuration.GetConnectionString("DataBase");
                    _implementation = new DBFactory(connectionString).Create();
                    break;
            }
        }

        public void CreateNewShop(string title, string address)
        {
            _implementation.CreateNewShop(title, address);
        }

        public void CreateNewProduct(string title, double price)
        {
            _implementation.CreateNewProduct(title, price);
        }

        public void CreateNewAssortment(Shop.DAL.Entities.Shop shop, Product product)
        {
            _implementation.CreateNewAssortment(shop, product);
        }

        public List<Shop.DAL.Entities.Shop> GetShops()
        {
            return _implementation.GetShops();
        }

        public List<Product> GetProducts()
        {
            return _implementation.GetProducts();
        }

        public List<Assortment> GetAssortments()
        {
            return _implementation.GetAssortments();
        }
    }
}
