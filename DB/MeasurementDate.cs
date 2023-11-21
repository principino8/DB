namespace DB;

public partial class MeasurementDate
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public virtual ICollection<ProtocolDate> ProtocolDates { get; set; } = new List<ProtocolDate>();
}
