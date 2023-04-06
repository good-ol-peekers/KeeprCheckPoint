
namespace KeeprCheckPoint.Models;
    public class Keep 
{
    public int id { get; set; }
    public string creatorId { get; set; }
    public Profile creator { get; set; }
    
    public string description { get; set; }

    public string name { get; set; }


    public string img { get; set; }

    public int? views { get; set; } = 0;

    public int kept { get; set; } = 0;

    // public int? vaultKeepId { get; set; }

}  
public class KeepInVault : Keep {
public int? vaultKeepId { get; set; }
public Boolean? isPrivate { get; set; }
}


