namespace DB;

public partial class Workshop
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<KKS> KKSes { get; set; } = new List<KKS>();
}
