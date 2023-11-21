namespace DB;

public partial class ProtocolDate
{
    public int Id { get; set; }

    public int? ProtocolId { get; set; }

    public int? MeasurementDateId { get; set; }

    public virtual MeasurementDate? MeasurementDate { get; set; }

    public virtual Protocol? Protocol { get; set; }
}
