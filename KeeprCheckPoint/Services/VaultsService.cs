
namespace KeeprCheckPoint.Services;


    public class VaultsService
{

private readonly VaultsRepository _repo;
public VaultsService(VaultsRepository repo)
    {
        _repo = repo;
    }

    internal Vault create(Vault vaultData)
    {
        Vault vault = _repo.create(vaultData);
        return vault;
    }

    internal string DeleteVault(int id, string userId)
    {
        Vault vault = _repo.GetVaultById(id);
        if (vault.creatorId != userId) throw new Exception($"thats not your vault to delete");
        _repo.DeleteVault(id);
        return $"that vault has been deleted!";
    }

    internal List<KeepInVault> getAllKeepsInAVault(int id, string userId)
    {   Vault vault = _repo.GetVaultById(id);
        if (vault.creatorId != userId && vault.isPrivate == true) throw new Exception ("thats not your vault to be peepin");
        List<KeepInVault> keeps = _repo.getAllKeepsInAVault(id);
        // if (keeps.isPrivate == true && keeps.creatorId != userId) throw new Exception("thats not your Vault to be peepin in yo!");
        return keeps;
    }

    internal List<Vault> getMyVaults(string id)
    {
        List<Vault> vaults = _repo.getMyVaults(id);
        return vaults;
    }

    internal List<Vault> getProfilesVaults(string id)
    {   
        List<Vault> vaults = _repo.getProfilesVaults(id);
        if (vaults == null) throw new Exception("this profile has no vaults!");
        return vaults;
    }

    internal Vault GetVaultById(int id, string userId)
    {
        Vault vault = _repo.GetVaultById(id);
        if (vault == null) throw new Exception($"there is no vault at ID: {id}");
        if (vault.isPrivate == true && vault.creatorId != userId) throw new Exception ($"hey that vaults private get out outta here at {id}");
        return vault;
    }

    internal Vault UpdateVault(Vault vaultData, int id, string userId)
    {
        Vault original = _repo.GetVaultById(id);
        if (original.creatorId != userId) throw new Exception($"thats not yours to edit");
        original.description = vaultData.description != null ? vaultData.description : original.description;
        original.img = vaultData.img != null ? vaultData.img : original.img;
        original.isPrivate = vaultData.isPrivate != null ? vaultData.isPrivate : original.isPrivate;
        original.name = vaultData.name != null ? vaultData.name : original.name;
        _repo.UpdateVault(original);
        return original;

    }
}