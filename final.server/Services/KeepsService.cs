using System;
using System.Collections.Generic;
using final.server.Models;
using final.server.Repositories;

namespace final.server.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _keepsRepo;

    public KeepsService(KeepsRepository keepsRepo)
    {
      _keepsRepo = keepsRepo;
    }

    internal List<Keep> GetAll()
    {
      return _keepsRepo.GetAll();
    }
  }
}