
namespace KeeprCheckPoint.Repositories;
public class KeepsRepository
{
    private readonly IDbConnection _db;
    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    public Keep create(Keep keepData)
    {
        string sql = @"
    INSERT INTO Keep
    (creatorId, name, description, img)
    VALUES
    (@creatorId, @name, @description, @img);
    SELECT LAST_INSERT_ID();
    ";
        int id = _db.ExecuteScalar<int>(sql, keepData);
        keepData.id = id;
        return keepData;
    }

    internal int DeleteKeep(int id)
    {
        string sql = @"
        DELETE FROM Keep
        WHERE id = @id;
        ";
        int rows = _db.Execute(sql, new { id });
        return rows;
    }

    internal List<Keep> getKeeps()
    {
        string sql = @"
        SELECT
        k.*,
        COUNT(vk.id) AS kept,
        acct.*
        FROM Keep k 
        LEFT JOIN VaultKeep vk ON k.id = vk.keepId
        JOIN accounts acct ON k.creatorId = acct.id
        GROUP BY k.id;
        ";
        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, creator) =>
        {
            keep.creator = creator;
            keep.creatorId = creator.Id;
            return keep;
        }).ToList();
        return keeps;
    }

    internal Keep GetOneKeep(int id)
    {
        string sql = @"
        SELECT
        k.*,
        COUNT(vk.id) AS kept,
        acct.*
        FROM Keep k
        LEFT JOIN VaultKeep vk ON k.id = vk.keepId
        JOIN accounts acct ON k.creatorId = acct.id
        WHERE k.id = @id
        GROUP BY k.id;
        ";
        Keep keep = _db.Query<Keep, Profile, Keep>(sql, (k, profile) =>
        {
            k.creator = profile;
            return k;
        }, new { id }).FirstOrDefault();
        return keep;
    }

    internal List<Keep> getProfilesKeeps(string id)
    {
        string sql = @"
        SELECT
        k.*,
        COUNT(vk.id) AS kept,
        acct.*
        FROM Keep k
        LEFT JOIN VaultKeep vk ON k.id = vk.keepId
        JOIN accounts acct ON k.creatorId = acct.id
        WHERE k.creatorId = @id
        GROUP BY k.id;
        ";
        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, creator) =>
        {
            keep.creator = creator;
            keep.creatorId = creator.Id;
            return keep;
        }, new { id }).ToList();
        return keeps;
    }

    internal int UpdateKeep(Keep keepData)
    {
        string sql = @"
        UPDATE Keep
        SET
        name = @name,
        description = @description,
        img = @img
        WHERE id = @id;
        ";
        int rows = _db.Execute(sql, keepData);
        return rows;
    }
}