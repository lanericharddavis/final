using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using final.server.Models;

namespace final.server.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<VaultKeep> GetAll()
    {
      throw new NotImplementedException();
      // string sql = @"
      // SELECT
      //   vk.*,
      //   v.*,
      //   k.*,
      //   a.*,
      //   FROM vaultkeeps vk
      //   JOIN accounts a ON a.id = vk.creatorId
      //   JOIN 
      //   JOIN
      //   JOIN
      // ";
    }

    internal VaultKeep GetById(int id)
    {
      throw new NotImplementedException();
    }

    internal List<VaultKeepViewModel> GetKeepsByVaultId(int vaultId)
    {
      string sql = @"
      SELECT
      k.*,
      v.*,
      vk.id as vaultKeepId,
      vk.vaultId as vaultId,
      vk.keepId as keepId
      FROM
      vaultkeeps vk
      JOIN keeps k ON k.id = vk.keepId
      JOIN vaults v ON v.id = vk.vaultId
      WHERE
      vk.vaultId = @vaultId;
      ";
      return _db.Query<VaultKeepViewModel>(sql, new { vaultId }).ToList();
    }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      string sql = @"
      INSERT INTO
      vaultkeeps(vaultId, keepId)
      VALUES (@VaultId, @KeepId);
      SELECT LAST_INSERT_ID();
      ";
      newVaultKeep.Id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      return newVaultKeep;
    }

    internal bool Remove(int id)
    {
      string sql = "DELETE FROM vaultkeeps WHERE id = @id LIMIT 1";
      int affectedRows = _db.Execute(sql, new { id });
      return affectedRows == 1;
    }

  }
}