    namespace KeeprCheckPoint.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class VaultKeepsController : ControllerBase
{
    private readonly VaultKeepsService _vaultKeepsService;
    private readonly Auth0Provider _auth;

    public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth)
    {
        _auth = auth;
        _vaultKeepsService = vaultKeepsService;
    }

[HttpDelete("{id}")]
[Authorize]
public async Task<ActionResult<string>> Delete(int id)
{
  try 
  {
    Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
    string message = _vaultKeepsService.Delete(id, userInfo.Id);
    return message;
  }
  catch (Exception e)
  {
    return BadRequest(e.Message);
  }
}


[HttpPost]
[Authorize]
public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vaultKeepData)
{
    try 
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      vaultKeepData.creatorId = userInfo.Id;
      VaultKeep vaultKeep = _vaultKeepsService.Create(vaultKeepData);
      return Ok(vaultKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
}





}