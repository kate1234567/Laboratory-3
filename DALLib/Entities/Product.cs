using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALLib.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string TitleProduct { get; set; }
        public double Price { get; set; }

        public Product()
        {
            
        }

        public Product(string title, double price)
        {
            TitleProduct = title;
            Price = price;
        }

        public override string ToString()
        {
            return $"{TitleProduct} - {Price} р.";
        }
    }
}
