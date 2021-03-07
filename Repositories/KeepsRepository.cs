using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Keep> Get()
    {
      string sql = "SELECT * FROM keeps WHERE isPrivate = 0;";
      return _db.Query<Keep>(sql);
    }


    internal Keep GetById(int keepId)
    {
      string sql = @"
      Update keeps
      SET
      views = views + 1
      WHERE id = @keepId;
      SELECT* FROM keeps WHERE id = @keepId;";
      return _db.QueryFirstOrDefault<Keep>(sql, new { keepId });
    }



    internal IEnumerable<Keep> GetByUserId(string userId)
    {
      string sql = "SELECT * FROM keeps WHERE userId = @userId;";
      return _db.Query<Keep>(sql, new { userId });
    }




    internal int Create(Keep newKeep)
    {
      string sql = @"
        INSERT INTO keeps
        (name, description, userId, img, isPrivate, views, shares, keeps)
        VALUES
        (@Name, @Description, @UserId, @Img, @IsPrivate, @Views, @Shares, @Keeps);
        SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newKeep);
    }


    internal bool Delete(int id, string userId)
    {
      string sql = "DELETE FROM keeps WHERE id = @id AND userId = @userId;";
      int deleted = _db.Execute(sql, new { id, userId });
      return deleted == 1;
    }



    internal Keep Edit(Keep original)
    {
      string sql = @"
        UPDATE keeps
        SET
            name = @Name,
            description = @Description,
            img = @Img,
            isPrivate = @IsPrivate,
            views = @Views,
            shares = @Shares,
            keeps = @Keeps
        WHERE id = @Id;
        SELECT * FROM keeps WHERE id = @Id;";
      return _db.QueryFirstOrDefault<Keep>(sql, original);
    }


  }
}