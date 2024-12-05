using DALLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLLib.Entities
{
    public class BuyPackProductInShop
    {
        public int Count { get; set; }
        public Product Product { get; set; }
        public Shop Shop { get; set; }
    }
}
