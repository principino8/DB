namespace DB;

public partial class Protocol
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ProtocolDate> ProtocolsDates { get; set; } = new List<ProtocolDate>();
}
