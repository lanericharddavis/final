using System;
using System.Collections.Generic;
using System.Linq;
using final.server.Models;
using final.server.Repositories;

namespace final.server.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _profilesRepo;
    private readonly KeepsRepository _keepsRepo;
    private readonly VaultsRepository _vaultsRepo;

    public ProfilesService(ProfilesRepository profilesRepo, KeepsRepository keepsRepo, VaultsRepository vaultsRepo)
    {
      _profilesRepo = profilesRepo;
      _keepsRepo = keepsRepo;
      _vaultsRepo = vaultsRepo;
    }

    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
      return _profilesRepo.GetKeepsByProfileId(profileId);
    }

    internal List<Vault> GetVaultsByProfileId(string profileId, string userId)
    {
      List<Vault> vaults = _profilesRepo.GetVaultsByProfileId(profileId);
      var publicVaults = new List<Vault>();
      var privateVaults = new List<Vault>();
      foreach (var vault in vaults)
      {
        if (vault.IsPrivate == false)
        {
          publicVaults.Add(vault);
        }
        else
        {
          privateVaults.Add(vault);
        }

      }
      if (profileId != userId)
      {
        return publicVaults;
      }
      if (profileId == userId)
      {
        return vaults;
      }
      return vaults;
    }
  }
}