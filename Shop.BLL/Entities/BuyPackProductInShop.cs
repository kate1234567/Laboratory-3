using Shop.DAL.Entities;

namespace Shop.BLL.Entities
{
    public class BuyPackProductInShop
    {
        public int Count { get; set; }
        public Product Product { get; set; }
        public Shop.DAL.Entities.Shop Shop { get; set; }
    }
}
