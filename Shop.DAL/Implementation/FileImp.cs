using CsvHelper;
using Shop.DAL.Entities;
using System.Globalization;
using System.Text;

namespace Shop.DAL.Implementation
{
    public class FileImp : IImplementation
    {
        private string _shops;
        private string _products;
        private string _assortments;

        private static int _shopId;
        private static int _productId;
        private static int _assortmentId;

        public FileImp(string path)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _shopId = 0;
            _productId = 0;
            _assortmentId = 0;

            if (!File.Exists(Path.Combine(path, "Shops.csv")))
            {
                using (var writer = new StreamWriter(Path.Combine(path, "Shops.csv"), true, Encoding.GetEncoding(1251)))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteHeader(typeof(Shop.DAL.Entities.Shop));
                    csv.NextRecord();
                }
            }
            _shops = Path.Combine(path, "Shops.csv");
            if (!File.Exists(Path.Combine(path, "Products.csv")))
            {
                using (var writer = new StreamWriter(Path.Combine(path, "Products.csv"), true, Encoding.GetEncoding(1251)))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteHeader(typeof(Product));
                    csv.NextRecord();
                }
            }
            _products = Path.Combine(path, "Products.csv");
            if (!File.Exists(Path.Combine(path, "Assortments.csv")))
            {
                using (var writer = new StreamWriter(Path.Combine(path, "Assortments.csv"), true, Encoding.GetEncoding(1251)))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteHeader(typeof(Assortment));
                    csv.NextRecord();
                }
            }
            _assortments = Path.Combine(path, "Assortments.csv");
        }

        public Task CreateNewAssortment(Shop.DAL.Entities.Shop shop, Product product, int count = 1)
        {
            return Task.Run(() =>
            {
                using (var writer = new StreamWriter(_assortments, true, Encoding.GetEncoding(1251)))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    var assortment = new Assortment(shop, product, count)
                    {
                        AssortmentId = _assortmentId++,
                    };
                    csv.WriteRecord(assortment);
                    csv.NextRecord();
                }
            });
        }

        public Task CreateNewProduct(string title, double price)
        {
            return Task.Run(() =>
            {
                using (var writer = new StreamWriter(_products, true, Encoding.GetEncoding(1251)))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    var product = new Product(title, price)
                    {
                        Id = _productId++
                    };
                    csv.WriteRecord(product);
                    csv.NextRecord();
                }
            });
        }

        public Task CreateNewProduct(Product product)
        {
            return Task.Run(() =>
            {
                using (var writer = new StreamWriter(_products, true, Encoding.GetEncoding(1251)))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    product.Id = _productId++;
                    csv.WriteRecord(product);
                    csv.NextRecord();
                }
            });
        }

        public Task CreateNewShop(string title, string address)
        {
            return Task.Run(() =>
            {
                using (var writer = new StreamWriter(_shops, true, Encoding.GetEncoding(1251)))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    var shop = new Shop.DAL.Entities.Shop(title, address)
                    {
                        Id = _shopId++,
                    };
                    csv.WriteRecord<Shop.DAL.Entities.Shop>(shop);
                    csv.NextRecord();
                }
            });
        }

        public List<Assortment> GetAssortments()
        {
            using (var reader = new StreamReader(_assortments, Encoding.GetEncoding(1251)))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var list = csv.GetRecords<Assortment>().ToList();
                _assortmentId = list.Count;
                return list;
            }
        }

        public List<Product> GetProducts()
        {
            using (var reader = new StreamReader(_products, Encoding.GetEncoding(1251)))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var list = csv.GetRecords<Product>().ToList();
                _productId = list.Count;
                return list;
            }
        }

        public List<Shop.DAL.Entities.Shop> GetShops()
        {
            using (var reader = new StreamReader(_shops, Encoding.GetEncoding(1251)))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var list = csv.GetRecords<Shop.DAL.Entities.Shop>().ToList();
                _shopId = list.Count;
                return list;
            }
        }
    }
}
