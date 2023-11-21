namespace DB;

public partial class ArmatureDrive
{
    public int Id { get; set; }

    public int? ArmatureTypeId { get; set; }

    public int? ElectricDriveId { get; set; }

    public float? MinTime { get; set; }

    public float? MaxTime { get; set; }

    public string? Resource { get; set; }

    public string? RotationSpeedPerMinute { get; set; }

    public string? OutputShaftRevolutionsQuantity { get; set; }

    public string? ControlRange { get; set; }

    public float? NominalCurrent { get; set; }

    public float? NominalPower { get; set; }

    public virtual ArmatureType? ArmatureType { get; set; }

    public virtual ElectricDrive? ElectricDrive { get; set; }

    public virtual ICollection<KKS> KKSes { get; set; } = new List<KKS>();
}
