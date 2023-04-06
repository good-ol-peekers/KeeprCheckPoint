
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
        k.*,
        v.*
        FROM VaultKeep vk
        JOIN accounts acct ON vk.creatorId = acct.id
        JOIN Keep k ON vk.keepId = k.id
        JOIN Vault v ON vk.vaultId = v.id
        WHERE vk.vaultId = @id;
        ";
        List<KeepInVault> keeps = _db.Query<VaultKeep, Account, KeepInVault, Vault, KeepInVault>(sql, (vk, prof, k, v) =>
        {
            k.creator = prof;
            k.vaultKeepId = vk.id;
            k.isPrivate = v.isPrivate;
            return k;
        }, new { id }).ToList();
        return keeps;
    }

    internal List<Vault> getMyVaults(string id)
    {
        string sql = @"
        SELECT
        v.*,
        acct.*
        FROM Vault v
        JOIN accounts acct ON v.creatorId = acct.id
        WHERE acct.id = @id;
        ";
        List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, (vault, prof) =>
        {
            vault.creatorId = prof.Id;
            return vault;
        }, new { id }).ToList();
        return vaults;
    }

    internal List<Vault> getProfilesVaults(string id, string userId)
    {
        string sql = @"
        SELECT
        v.*,
        acct.*
        FROM Vault v AND account acct
        WHERE v.creatorId = @id AND acct.id = &userId;
        ";
        return _db.Query<Vault>(sql, new { id, userId }).ToList();
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
        Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, creator) =>
        {
            vault.creatorId = creator.Id;
            vault.creator = creator;
            return vault;
        }, new { id }).FirstOrDefault();
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