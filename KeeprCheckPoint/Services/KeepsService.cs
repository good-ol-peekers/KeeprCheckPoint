
namespace KeeprCheckPoint.Services;
    public class KeepsService
{
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo)
    {
        _repo = repo;
    }


    internal Keep create(Keep keepData)
    {
        Keep keep = _repo.create(keepData);
        return keep;

    }

    internal List<Keep> Get(string userId)
    {
        List<Keep> keeps = _repo.getKeeps();
        return keeps;
    }

    internal Keep getKeepById(int id1, string id2)
    {
        throw new NotImplementedException();
    }

    internal Keep UpdateKeep(Keep keepData)
    {
        Keep original = this.GetOne(keepData.id, keepData.creatorId);
        original.name = keepData.name == null ? original.name : keepData.name;
        original.description = keepData.description != null ? keepData.description : original.description;
        original.img = keepData.img != null ? keepData.img : original.img;
        original.kept = keepData.kept != null ? keepData.kept : original.kept;
        _repo.UpdateKeep(original);
        return original;

    }

    internal Keep GetOne(int id, string creatorId)
    {
        Keep keep = _repo.GetOneKeep(id);
        if (keep == null) throw new Exception($"no keep at the id; {id}");
        if (keep.creatorId != creatorId)
        keep.views++;
        _repo.UpdateKeep(keep);
        return keep;
    }

    internal string DeleteKeep(int id, string userId)
    {
        Keep keep = _repo.GetOneKeep(id);
        if (keep.creatorId != userId) throw new Exception("not your keep to delete.");
        _repo.DeleteKeep(id);
        return $"you have deleted your keep.";
    }
}