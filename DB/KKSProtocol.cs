namespace DB;

public partial class KKSProtocol
{
    public int Id { get; set; }

    public string KKSId { get; set; } = null!;

    public int ProtocolDateId { get; set; }

    public virtual KKS KKS { get; set; } = null!;

    public virtual ProtocolDate ProtocolDate { get; set; } = null!;
}
