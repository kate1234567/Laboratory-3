namespace Shop.DAL.Entities
{
    public class Product: BaseIdEntity
    {
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
