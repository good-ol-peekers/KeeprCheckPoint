
namespace KeeprCheckPoint.Controllers;

    [ApiController]
    [Route("api/[controller]")]

    public class VaultsController : ControllerBase
{
    private readonly VaultsService _vaultsService;
    private readonly Auth0Provider _auth;
public VaultsController(VaultsService vaultsService, Auth0Provider auth)
    {
        _vaultsService = vaultsService;
        _auth = auth;
    }
[HttpGet("{id}/keeps")]
public async Task<ActionResult<List<KeepInVault>>> getAllKeepsInAVault(int id)
{
  try 
  {
    Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
    List<KeepInVault> keeps = _vaultsService.getAllKeepsInAVault(id, userInfo.Id);
    return Ok(keeps);
  }
  catch (Exception e)
  {
    return BadRequest(e.Message);
  }
}

[HttpPost]
[Authorize]
public async Task<ActionResult<Vault>> create([FromBody] Vault vaultData)
{
    try 
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      vaultData.creatorId = userInfo.Id;
      Vault vault = _vaultsService.create(vaultData);
      vault.creator = userInfo;
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
}

[HttpGet("{id}")]
public async Task<ActionResult<Vault>> GetVaultById(int id)
{
    try 
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      Vault vault = _vaultsService.GetVaultById(id, userInfo?.Id);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
}

[HttpPut("{id}")]
[Authorize]
public async Task<ActionResult<Vault>> UpdateVault([FromBody] Vault vaultData, int id)
{
  try 
  {
    Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
    // vaultData.creatorId = userInfo.Id;
    // vaultData.id = id;
    Vault vault = _vaultsService.UpdateVault(vaultData, id, userInfo.Id);
    return Ok(vault);
  }
  catch (Exception e)
  {
    return BadRequest(e.Message);
  }
}

[HttpDelete("{id}")]
[Authorize]
public async Task<ActionResult<string>> DeleteVault(int id)
{
  try 
  {
    Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
    string message = _vaultsService.DeleteVault(id, userInfo.Id);
    return Ok(message);
  }
  catch (Exception e)
  {
    return BadRequest(e.Message);
  }
}



}