using Shop.DAL.Entities;

namespace Shop.BLL.Entities
{
    public class PackProduct
    {
        public Product Product { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public Shop.DAL.Entities.Shop Shop { get; set; }
    }
}
