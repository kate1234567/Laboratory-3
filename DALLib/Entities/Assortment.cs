using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALLib.Entities
{
    public class Assortment
    {
        [Key]
        public int AssortmentId { get; set; }       
        public Shop Shop { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }

        public Assortment()
        {
            
        }

        public Assortment(Shop shop, Product product, int count)
        {
            Shop = shop;
            Product = product;
            Count = count;
        }

        public override string ToString()
        {
            return $"{Product.TitleProduct} {Product.Price} р. - {Shop.TitleShop} {Shop.Address}";
        }
    }
}
