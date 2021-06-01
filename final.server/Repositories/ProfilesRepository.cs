using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using final.server.Models;

namespace final.server.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Keep> GetKeepsByProfileId(int profileId)
    {
      string sql = @"
        SELECT
            k.*,
            a.*
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId
        WHERE k.profileId = @profileId;
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
      {
        k.Creator = p;
        return k;
      }, new { profileId }).ToList();
    }

    internal List<Vault> GetVaultsByProfileId(int profileId)
    {
      string sql = @"
        SELECT
            v.*,
            a.*
        FROM vaults v
        JOIN accounts a ON a.id = v.creatorId
        WHERE v.profileId = @profileId;
      ";
      return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
      {
        v.Creator = p;
        return v;
      }, new { profileId }).ToList();
    }
  }
}