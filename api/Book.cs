using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookApi
{
  public class Book
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonIgnore] // TODO: How to hide the id in Swwagger from the POST and PUT calls?
    public int Id { get; set; }

    public required string Title { get; set; }
  }
}