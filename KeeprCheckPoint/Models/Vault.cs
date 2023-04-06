
namespace KeeprCheckPoint.Models;


public class Vault
{
    public int id { get; set; }
    public string creatorId { get; set; }
    public string name { get; set; }
    public string description { get; set; } = "default value";
    public string img { get; set; } = "https://images.unsplash.com/photo-1519681393784-d120267933ba?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80";
    public bool? isPrivate { get; set; } = false;
    public Profile creator { get; set; }
}