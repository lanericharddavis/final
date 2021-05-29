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
    internal List<Keep> GetAll()
    {
      string sql = @"
      SELECT
            k.*,
            a.*
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId
       ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
      {
        k.Creator = p;
        return k;
      }, splitOn: "id").ToList();
    }

    internal Keep GetById(int id)
    {
      string sql = @"
        SELECT
            k.*,
            a.*
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId
        WHERE k.Id = @id;
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
      {
        k.Creator = p;
        return k;
      }, new { id }).FirstOrDefault();
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

    internal Keep Create(Keep keep)
    {
      string sql = @"
      INSERT INTO
      keeps (name, creatorId, description, img, views, shares, keeps)
      VALUES (@Name, @CreatorId, @Description, @Img, @Views, @Shares, @Keeps);
      SELECT LAST_INSERT_ID();
      ";
      keep.Id = _db.ExecuteScalar<int>(sql, keep);
      return keep;
    }

    internal Keep Update(Keep keep)
    {
      string sql = @"
      UPDATE keeps
      SET
        name = @Name,
        creatorId = @CreatorId,
        description = @Description,
        img = @Img,
        views = @Views,
        shares = @Shares,
        keeps = @Keeps
      WHERE id = @Id;
      ";
      _db.Execute(sql, keep);
      return keep;
    }

    internal bool Remove(int id)
    {
      string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1";
      int affectedRows = _db.Execute(sql, new { id });
      return affectedRows == 1;
    }
  }
}