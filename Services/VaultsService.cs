using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;
        public VaultsService(VaultsRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<Vault> Get()
        {
            return _repo.Get();
        }



        public Vault GetById(int keepId)
        {
            Vault exists = _repo.GetById(keepId);
            if (exists == null) { throw new Exception("Invalid Vault"); }
            return exists;
        }

        public IEnumerable<Vault> GetByUserId(string user)
        {
            IEnumerable<Vault> exists = _repo.GetByUserId(user);
            if (exists == null) { throw new Exception("No Vaults Exist"); }
            return exists;
        }


        public Vault Create(Vault newVault)
        {
            int id = _repo.Create(newVault);
            newVault.Id = id;
            return newVault;
        }


        public String Delete(int id, string userId)
        {
            Vault exists = GetById(id);
            bool vaultDeleted = _repo.Delete(id, userId);
            if (!vaultDeleted)
            {
                throw new Exception("Cannot Delete Vault");
            }
            return "Vault Deleted";
        }
    }
}

