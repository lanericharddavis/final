using System;
using System.Collections.Generic;
using final.server.Models;
using final.server.Repositories;

namespace final.server.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vksRepo;

    public VaultKeepsService(VaultKeepsRepository vksRepo)
    {
      _vksRepo = vksRepo;
    }

    internal List<VaultKeep> GetAll()
    {
      return _vksRepo.GetAll();
    }

    internal VaultKeep GetById(int id)
    {
      VaultKeep blog = _vksRepo.GetById(id);
      if (blog == null)
      {
        throw new Exception("Invalid VaultKeep Id");
      }
      return blog;
    }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      return _vksRepo.Create(newVaultKeep);
    }

    internal void Delete(int id, string creatorId)
    {
      VaultKeep comment = GetById(id);
      if (comment.CreatorId != creatorId)
      {
        throw new Exception("You cannot delete another users VaultKeep");
      }
      if (!_vksRepo.Delete(id))
      {
        throw new Exception("Error: Review VaultKeepsService Delete");
      }
    }
  }
}
