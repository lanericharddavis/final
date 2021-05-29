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

    internal Keep GetById(int id)
    {
      Keep keep = _keepsRepo.GetById(id);
      if (keep == null)
      {
        throw new Exception("Invalid Id");
      }
      return keep;
    }

    internal Keep Create(Keep keep)
    {
      return _keepsRepo.Create(keep);
    }

    internal Keep Update(Keep update, Account userInfo)
    {
      Keep original = GetById(update.Id);
      if (original.CreatorId == userInfo.Id)
      {
        original.Name = update.Name != null ? update.Name : original.Name;
        original.Description = update.Description != null ? update.Description : original.Description;
        original.Img = update.Img != null ? update.Img : original.Img;
        if (_keepsRepo.Update(original) != null)
        {
          return original;
        }
        throw new Exception("Error: Review KeepsService Update");
      }
      throw new Exception("Edit Not Permitted: You do not own this Keep.");
    }

    internal void Remove(int id, string creatorId)
    {
      Keep keep = GetById(id);
      if (keep.CreatorId != creatorId)
      {
        throw new Exception("Delete Not Permitted: You do not own this Keep");
      }
      if (!_keepsRepo.Remove(id))
      {
        throw new Exception("Error: Review KeepsService Delete");
      }
    }
  }
}