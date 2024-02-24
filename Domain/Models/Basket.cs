using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models;

public class Basket
{
    public Guid Id { get; set; }

    [ForeignKey("account_id")]
    [JsonIgnore]
    public Account Account { get; set; }

    public IList<Product>? Products { get; set; }
}
