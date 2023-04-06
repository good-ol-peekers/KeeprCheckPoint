namespace KeeprCheckPoint.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _repo;
    private readonly VaultsService _vaultsService;
    public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService)
    {
        _repo = repo;
        _vaultsService = vaultsService;
    }

    internal VaultKeep Create(VaultKeep vaultKeepData)
    {
        Vault vault = _vaultsService.GetVaultById(vaultKeepData.vaultId, vaultKeepData.creatorId);
        if (vault.creatorId != vaultKeepData.creatorId) throw new Exception($"thats not yours to edit");

        VaultKeep vaultKeep = _repo.Create(vaultKeepData);
        return vaultKeep;
    }

    internal string Delete(int vaultKeepId, string userId)
    {
        Vault vault = _repo.GetVaultKeepById(vaultKeepId, userId); //FIXME nope dont worry about the vault here.... you dont need it
        if (vault.creatorId != userId) throw new Exception($"thats not yours to delete");

        int rows = _repo.Delete(vaultKeepId, userId);
        return $"that vaultkeep has been deleted";
    }
}