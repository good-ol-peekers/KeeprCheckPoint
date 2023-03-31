
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
    (creatorId, name, description, img, views, kept)
    VALUES
    (@creatorId, @name, @description, @img, 1, 1);
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
        acct.*
        FROM Keep k 
        JOIN accounts acct ON k.creatorId = acct.id;
        ";
        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, creator) => {
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
        acct.*
        FROM Keep k
        JOIN accounts acct ON k.creatorId = acct.id
        WHERE k.id = @id;
        ";
        Keep keep = _db.Query<Keep, Profile, Keep>(sql, (k, profile) =>
        {
            k.creator = profile;
            return k;
        }, new { id }).FirstOrDefault();
        return keep;
    }

    internal int UpdateKeep(Keep keepData)
    {
        string sql = @"
        UPDATE Keep
        SET
        name = @name,
        description = @description,
        img = @img,
        kept = @kept
        WHERE id = @id;
        ";
        int rows = _db.Execute(sql, keepData);
        return rows;
    }
}