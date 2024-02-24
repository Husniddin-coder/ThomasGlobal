using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models;

public class Accept
{
    public Guid Id { get; set; }

    public string Title_accept { get; set; }

    public string Title_no_accept { get; set; }

    [ForeignKey("product_id")]
    [JsonIgnore]
    public Product Product { get; set; }
}
