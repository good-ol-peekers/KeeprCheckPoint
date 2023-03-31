
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

    internal Vault GetVaultById(int id)
    {
        Vault vault = _repo.GetVaultById(id);
        if (vault == null) throw new Exception($"there is no vault at ID: {id}");
        return vault;
    }

    internal Vault UpdateVault(Vault vaultData)
    {
        Vault original = this.GetVaultById(vaultData.id);
        original.description = vaultData.description != null ? vaultData.description : original.description;
        original.img = vaultData.img != null ? vaultData.img : original.img;
        original.isPrivate = vaultData.isPrivate != null ? vaultData.isPrivate : original.isPrivate;
        original.name = vaultData.name != null ? vaultData.name : original.name;
        _repo.UpdateVault(original);
        return original;

    }
}