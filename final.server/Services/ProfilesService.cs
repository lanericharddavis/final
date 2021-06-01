using System;
using System.Collections.Generic;
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

    internal List<Keep> GetKeepsByProfileId(int profileId)
    {
      return _profilesRepo.GetKeepsByProfileId(profileId);
    }

    internal List<Vault> GetVaultsByProfileId(int profileId)
    {
      return _profilesRepo.GetVaultsByProfileId(profileId);
    }
  }
}