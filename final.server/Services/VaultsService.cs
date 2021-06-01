using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using final.server.Models;
using final.server.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace final.server.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vaultsRepo;
    private readonly VaultKeepsRepository _vksRepo;

    public VaultsService(VaultsRepository vaultsRepo, VaultKeepsRepository vksRepo)
    {
      _vaultsRepo = vaultsRepo;
      _vksRepo = vksRepo;
    }

    internal List<Vault> GetAll()
    {
      return _vaultsRepo.GetAll();
    }

    internal Vault GetById(int id, string userId)
    {

      Vault vault = _vaultsRepo.GetById(id);
      if (vault == null)
      {
        throw new Exception("Invalid Id");
      }
      if (vault.IsPrivate == true && userId != vault.CreatorId)
      {
        throw new Exception("Access Denied: Vault is set as Private");
      }
      return vault;
    }

    internal Vault Create(Vault vault)
    {
      return _vaultsRepo.Create(vault);
    }

    internal Vault Update(Vault update, Account userInfo)
    {
      Vault original = GetById(update.Id, userInfo.Id);
      if (original.CreatorId == userInfo.Id)
      {
        original.Name = update.Name != null ? update.Name : original.Name;
        original.Description = update.Description != null ? update.Description : original.Description;
        // NOTE What is a bool state if it not changed/updated/edited
        // Editing of the bool is still functional, not sure why...
        original.IsPrivate = update.IsPrivate != null ? update.IsPrivate : original.IsPrivate;
        if (_vaultsRepo.Update(original) != null)
        {
          return original;
        }
        throw new Exception("Error: Review VaultsService Update");
      }
      throw new Exception("Edit Not Permitted: You do not own this Vault.");
    }

    internal List<VaultKeepViewModel> GetKeepsByVaultId(int vaultId)
    {
      return _vksRepo.GetKeepsByVaultId(vaultId);
    }

    internal void Remove(int id, string creatorId)
    {
      Vault vault = GetById(id, creatorId);
      if (vault.CreatorId != creatorId)
      {
        throw new Exception("Delete Not Permitted: You do not own this Vault");
      }
      if (!_vaultsRepo.Remove(id))
      {
        throw new Exception("Error: Review VaultsService Delete");
      }
    }

  }
}