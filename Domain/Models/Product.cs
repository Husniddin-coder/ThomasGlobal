using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public double Amount { get; set; }

        public int Availability_count { get; set; }

        public string Code { get; set; }

        [Range(1, 14)]
        public int Region { get; set; }

        public string Company { get; set; }

        public double Discount_price { get; set; }

        public double Price { get; set; }

        public int Sales_count { get; set; }

        public string Count_massa { get; set; }

        public bool Product_accept { get; set; }

        public Accept Accept { get; set; }

        [ForeignKey("basket_id")]
        public Basket? Basket { get; set; }

        [JsonIgnore]
        [ForeignKey("account_id")]
        public Account Account { get; set; }

        public IList<Comment> Comments { get; set; }

        public IList<Image> Images { get; set; }
    }
}
