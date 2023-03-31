
namespace KeeprCheckPoint.Repositories;



    public class VaultsRepository
{
    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Vault create(Vault vaultData)
    {
        string sql = @"
        INSERT INTO Vault
        (creatorId, name, description, img)
        VALUES
        (@creatorId, @name, @description, @img);
        SELECT LAST_INSERT_ID();
        ";
        int id = _db.ExecuteScalar<int>(sql, vaultData);
        vaultData.id = id;
        return vaultData;
    }

    internal Vault GetVaultById(int id)
    {
        String sql = @"
        SELECT 
        v.*,
        acct.*
        FROM Vault v
        JOIN accounts acct ON v.creatorId = acct.id
        WHERE v.id = @id;
        ";
        Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, creator) => {
            vault.creatorId = creator.Id;
            vault.creator = creator;
            return vault;
        }, new {id}).FirstOrDefault();
        return vault;
    }

    internal int UpdateVault(Vault vaultData)
    {
        string sql = @"
        UPDATE Vault
        SET
        name = @name,
        description = @description,
        img = @img,
        isPrivate = @isPrivate
        WHERE id = @id;
        ";
        int rows = _db.Execute(sql, vaultData);
        return rows;
    }
}