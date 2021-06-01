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


    internal VaultKeepViewModel GetById(int id)
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
      vk.Id = @id;
      ";
      return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (vk, p) =>
{
  vk.Creator = p;
  return vk;
}, new { id }).FirstOrDefault();
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
      vaultkeeps(creatorId, vaultId, keepId)
      VALUES (@CreatorId, @VaultId, @KeepId);
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