namespace KeeprCheckPoint.Controllers;

    [ApiController]
    [Route("api/[controller]")]

    public class ProfilesController : ControllerBase
{
    private readonly AccountService _accountService;
    private readonly Auth0Provider _auth0Provider;
    private readonly VaultsService _vaultsService;
    private readonly KeepsService _keepsService;
 
    public ProfilesController(AccountService accountService, Auth0Provider auth0Provider, VaultsService vaultsService, KeepsService keepsService)
    {
        _accountService = accountService;
        _auth0Provider = auth0Provider;
        _vaultsService = vaultsService;
        _keepsService = keepsService;
    }

    [HttpGet("{id}/vaults")]
    async public Task<ActionResult<List<Vault>>> getProfilesVaults(string id)
    {
        try 
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

            List<Vault> vault = _vaultsService.getProfilesVaults(id, userInfo.Id);
        return Ok(vault);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}/")]
    public ActionResult<Account> getProfile(string id)
    {
        try 
        {
        Account account = _accountService.GetProfile(id);
        return Ok(account);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
    }

[HttpGet("{id}/keeps")]
public ActionResult<List<Keep>> getProfilesKeeps(string id)
{
    try 
    {
      List<Keep> keeps = _keepsService.getProflesKeeps(id);
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
}
}