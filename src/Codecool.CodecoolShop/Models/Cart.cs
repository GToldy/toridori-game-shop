using System.Collections.Generic;

namespace Codecool.CodecoolShop.Models
{
    public class Cart : BaseModel
    {
        public List<Item> Items { get; set; }

        public Cart()
        {
            Items = new List<Item>();
        }
    }
}
