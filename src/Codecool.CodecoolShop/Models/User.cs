using System;

namespace Codecool.CodecoolShop.Models
{
    public class User : BaseModel
    {
        public PersonDetails personDetails { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
}
