using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Image
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        [ForeignKey("product_id")]
        public Product? Product { get; set; }

        [ForeignKey("account_id")]
        public Account? Account { get; set; }

        [ForeignKey("story_id")]
        public Story? Story { get; set; }
    }
}
