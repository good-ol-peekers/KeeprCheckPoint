
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
        (creatorId, name, description, img, isPrivate)
        VALUES
        (@creatorId, @name, @description, @img, @isPrivate);
        SELECT LAST_INSERT_ID();
        ";
        int id = _db.ExecuteScalar<int>(sql, vaultData);
        vaultData.id = id;
        return vaultData;
    }

    internal int DeleteVault(int id)
    {
        string sql = @"
        DELETE
        FROM Vault
        WHERE id = @id;
        ";
        int rows = _db.Execute(sql, new { id });
        return rows;
    }

    internal List<KeepInVault> getAllKeepsInAVault(int id)
    {
        string sql = @"
        SELECT 
        vk.*,
        acct.*,
        k.*
        FROM VaultKeep vk
        JOIN accounts acct ON vk.creatorId = acct.id
        JOIN Keep k ON vk.keepId = k.id
        WHERE vk.vaultId = @id;
        ";
        List<KeepInVault> keeps = _db.Query<VaultKeep, Account, KeepInVault, KeepInVault>(sql, (vk, prof, k) =>
        {
            k.creator = prof;
            k.vaultKeepId = vk.id;
            return k;
        }, new { id }).ToList();
        return keeps;
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