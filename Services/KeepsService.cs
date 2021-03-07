using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }

    public IEnumerable<Keep> Get()
    {
      return _repo.Get();
    }

    public Keep GetById(int keepId)
    {
      Keep foundKeep = _repo.GetById(keepId);
      if (foundKeep == null) { throw new Exception("Invalid keep"); }

      {
        return foundKeep;
      }

    }

    public IEnumerable<Keep> GetByUserId(string user)
    {
      IEnumerable<Keep> exists = _repo.GetByUserId(user);
      if (exists == null) { throw new Exception("No Keep Exists"); }
      return exists;
    }


    public Keep Create(Keep newKeep)
    {
      int id = _repo.Create(newKeep);
      newKeep.Id = id;
      return newKeep;
    }
    public String Delete(int id, string userId)
    {
      Keep exists = GetById(id);
      if (exists == null) { throw new Exception("Keep Does Not Exist"); }
      bool keepDeleted = _repo.Delete(id, userId);
      if (!keepDeleted)
      {
        throw new Exception("Cannot Delete Keep");
      }
      return "Keep Deleted";
    }



    public Keep Edit(Keep newKeep, string UserId)
    {
      Keep original = GetById(newKeep.Id);
      // if (original.UserId != newKeep.UserId) { throw new Exception("The Keep does not belong to you"); }

      original.Name = newKeep.Name == null ? original.Name : newKeep.Name;
      original.Description = newKeep.Description == null ? original.Description : newKeep.Description;
      original.Img = newKeep.Img == null ? original.Img : newKeep.Img;
      original.IsPrivate = newKeep.IsPrivate;
      original.Views = newKeep.Views == 0 ? original.Views : newKeep.Views;
      original.Shares = newKeep.Shares == 0 ? original.Shares : newKeep.Shares;
      original.Keeps = newKeep.Keeps == 0 ? original.Keeps : newKeep.Keeps;
      return _repo.Edit(original);
    }
  }
}

