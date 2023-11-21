namespace DB;

public partial class TechnicalCondition
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? ValveManufacturer { get; set; }

    public virtual ICollection<ArmatureType> ArmaturesTypes { get; set; } = new List<ArmatureType>();
}
