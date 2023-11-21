namespace DB;

public partial class IcAct
{
    public int Id { get; set; }

    public string Act { get; set; } = null!;

    public virtual ICollection<KKS> KKSes { get; set; } = new List<KKS>();
}
