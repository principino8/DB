namespace DB;

public partial class ElectricDrive
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Manufacturer { get; set; }

    public virtual ICollection<ArmatureDrive> ArmaturesDrives { get; set; } = new List<ArmatureDrive>();
}
