namespace Codecool.CodecoolShop.Models
{
    public class Item : BaseModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
