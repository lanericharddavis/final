using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using final.server.Models;

namespace final.server.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
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