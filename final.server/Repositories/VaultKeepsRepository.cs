using System;
using System.Collections.Generic;
using System.Data;
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
    }

    internal VaultKeep GetById(int id)
    {
      throw new NotImplementedException();
    }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      throw new NotImplementedException();
    }

    internal bool Delete(int id)
    {
      throw new NotImplementedException();
    }
  }
}