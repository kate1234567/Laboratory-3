namespace Shop.DAL.Entities
{
    public class Shop: BaseIdEntity
    {
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
