namespace DB;

public partial class TestResult
{
    public int Id { get; set; }

    public string Result { get; set; } = null!;

    public virtual ICollection<KKS> KKSes { get; set; } = new List<KKS>();
}
