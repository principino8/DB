namespace DB;

public partial class ArmatureType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? TechnicalConditionsId { get; set; }

    public virtual ICollection<ArmatureDrive> ArmaturesDrives { get; set; } = new List<ArmatureDrive>();

    public virtual TechnicalCondition? TechnicalConditions { get; set; }
}
