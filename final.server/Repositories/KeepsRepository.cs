using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using final.server.Models;

namespace final.server.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
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
  }
}