using DALLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLLib.Entities
{
    public class PackProduct
    {
        public Product Product { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public Shop Shop { get; set; }
    }
}
