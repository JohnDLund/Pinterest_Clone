using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;
using System.Security.Claims;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }



    internal IEnumerable<VaultKeep> Get()
    {
      string sql = "SELECT * FROM vaultkeeps;";
      return _db.Query<VaultKeep>(sql);
    }

    internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int vaultId, string userId)
    {

      string sql = @"
            SELECT k.*,
            vk.id as vaultKeepId
            FROM vaultkeeps vk
            INNER JOIN keeps k ON k.id = vk.keepId 
            WHERE (vaultId = @vaultId AND vk.userId = @userId);";
      return _db.Query<VaultKeepViewModel>(sql, new { vaultId, userId });
    }

    internal VaultKeep GetById(int vaultKeepId)
    {
      string sql = "SELECT * FROM vaultkeeps WHERE id = @vaultKeepId;";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { vaultKeepId });
    }



    internal int Create(VaultKeep newVaultKeep)
    {
      string sql = @"
        Update keeps
        SET
        keeps = keeps + 1
        WHERE id = @keepId;

        INSERT INTO vaultkeeps
        (keepId, vaultId, userId)
        VALUES
        (@KeepId, @VaultId, @UserId);
        SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newVaultKeep);
    }


    internal bool Delete(int id, string userId)
    {
      string sql = "DELETE FROM vaultkeeps WHERE id = @id AND userId = @userId;";
      int deleted = _db.Execute(sql, new { id, userId });
      return deleted == 1;
    }


  }
}