using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Comment
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public int Starts_count { get; set; }

        [ForeignKey("account_id")]
        [JsonIgnore]
        public Account? Account { get; set; }

        [ForeignKey("product_id")]
        public Product Product { get; set; }
    }
}
