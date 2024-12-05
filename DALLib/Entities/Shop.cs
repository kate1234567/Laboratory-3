using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALLib.Entities
{
    public class Shop
    {
        public int Id { get; set; }
        public string TitleShop { get; set; }
        public string Address { get; set; }

        public Shop()
        {
            
        }

        public Shop(string title, string address)
        {
            TitleShop = title;
            Address = address;
        }

        public override string ToString()
        {
            return $"{Id} - {TitleShop}";
        }
    }
}
