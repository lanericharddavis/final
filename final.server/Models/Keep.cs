using System.ComponentModel.DataAnnotations;

namespace final.server.Models
{
  public class Keep
  {
    public int Id { get; set; }
    [Required]
    [MinLength(1)]
    public string Name { get; set; }
    public string CreatorId { get; set; }
    public string Description { get; set; }
    public string Img { get; set; }
    public int Views { get; set; }
    public int Shares { get; set; }
    public int Keeps { get; set; }
    public Profile Creator { get; set; }
  }
}