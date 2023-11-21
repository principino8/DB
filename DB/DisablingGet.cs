namespace DB;

public partial class DisablingGet
{
    public int Id { get; set; }

    public string DisablingOption { get; set; } = null!;

    public virtual ICollection<KKS> KKSes { get; set; } = new List<KKS>();
}
