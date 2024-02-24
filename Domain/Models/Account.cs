using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("account")]
    public class Account
    {
        [Key]
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public bool Delivery { get; set; }

        public string Region { get; set; }

        public string Company_name { get; set; }

        [Column("account_image")]
        public string Account_image { get; set; }

        public Image Image { get; set; }

        public IList<Story>? Stories { get; set; }

        public Basket? Basket { get; set; }

        public IList<Product>? Products { get; set; }
    }
}
