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

    internal List<Vault> GetAll()
    {
      string sql = @"
      SELECT
            v.*,
            a.*
        FROM vaults v
        JOIN accounts a ON a.id = v.creatorId
       ";
      return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
      {
        v.Creator = p;
        return v;
      }, splitOn: "id").ToList();
    }

    internal Vault GetById(int id)
    {
      string sql = @"
        SELECT
            v.*,
            a.*
        FROM vaults v
        JOIN accounts a ON a.id = v.creatorId
        WHERE v.Id = @id;
      ";
      return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
      {
        v.Creator = p;
        return v;
      }, new { id }).FirstOrDefault();
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

    internal Vault Create(Vault vault)
    {
      string sql = @"
      INSERT INTO
      vaults (name, creatorId, description, isPrivate)
      VALUES (@Name, @CreatorId, @Description, @IsPrivate);
      SELECT LAST_INSERT_ID();
      ";
      vault.Id = _db.ExecuteScalar<int>(sql, vault);
      return vault;
    }

    internal Vault Update(Vault vault)
    {
      string sql = @"
      UPDATE vaults
      SET
        name = @Name,
        creatorId = @CreatorId,
        description = @Description,
        isPrivate = @IsPrivate
      WHERE id = @Id;
      ";
      _db.Execute(sql, vault);
      return vault;
    }

    internal bool Remove(int id)
    {
      string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1";
      int affectedRows = _db.Execute(sql, new { id });
      return affectedRows == 1;
    }
  }
}