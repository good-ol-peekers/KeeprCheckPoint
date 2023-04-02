namespace KeeprCheckPoint.Repositories;

    public class VaultKeepsRepository
{
    private readonly IDbConnection _db;
    
    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }

public VaultKeep Create(VaultKeep vaultKeepData)
{
    string sql = @"
    INSERT INTO VaultKeep
    (creatorId, vaultId, keepId)
    VALUES
    (@creatorId, @vaultId, @keepId);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, vaultKeepData);
    vaultKeepData.id = id;
    return vaultKeepData;
}

    internal int Delete(int id, string userId)
    {
        string sql = @"
        DELETE FROM
        VaultKeep
        WHERE 
        id = @id AND creatorId = @userId;
        ";
        int rows = _db.Execute(sql, new {id, userId});
        return rows;
    }

    internal VaultKeep GetOne(int id)
    {
        throw new NotImplementedException();
    }
}
