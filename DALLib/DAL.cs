using DALLib.Entities;
using DALLib.Implementation;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace DALLib
{
    public class DAL
    {
        private static TypeImplementation _currentImplementation;
        private static IConfiguration _configuration;
        private static IImplementation _implementation;


        public DAL(IConfiguration configuration)
        {
            _configuration = configuration;
            var db = _configuration.GetSection("ConnectionStrings")["CurrentImplementation"];
            if(db=="True")
            {
                new DAL(TypeImplementation.DB);
            }
            else
            {
                new DAL(TypeImplementation.File);
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

        private DAL(TypeImplementation typeImplementation)
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

        public void CreateNewAssortment(Shop shop, Product product)
        {
            _implementation.CreateNewAssortment(shop, product);
        }

        public List<Shop> GetShops()
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
