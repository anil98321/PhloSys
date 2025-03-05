namespace SWProductWebApi.Models
{
    public class Product
    {
        public int Id { get; set; }=  new Random().Next();
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string[] Sizes { get; set; }
        public string Description { get; set; }
    }
}
