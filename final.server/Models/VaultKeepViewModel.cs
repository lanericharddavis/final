namespace final.server.Models
{
  public class VaultKeepViewModel : Profile
  {
    public string CreatorId { get; set; }
    public int VaultId { get; set; }
    public int KeepId { get; set; }
    public Profile Creator { get; set; }
  }
}