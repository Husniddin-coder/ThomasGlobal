using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models;

public class Story
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Topic { get; set; }

    public string Story_Image { get; set; }

    public int Like { get; set; }

    public Image Image { get; set; }

    [ForeignKey("account_id")]
    [JsonIgnore]
    public Account Account { get; set; }

}
