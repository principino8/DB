namespace DB;

public partial class KKS
{
    public string Id { get; set; } = null!;

    public string? EquipmentName { get; set; }

    public int? ArmatureDriveId { get; set; }

    public string? SafetyClass { get; set; }

    public string? EquipmentRoom { get; set; }

    public string? SystemName { get; set; }

    public int? WorkshopId { get; set; }

    public short? StoClass { get; set; }

    public string? MeasurementNotes { get; set; }

    public int? TestResultId { get; set; }

    public string? Recommendations { get; set; }

    public float? SmoothnessPercentageOp { get; set; }

    public float? SmoothnessPercentageCl { get; set; }

    public float? ActualRunningTimeOp { get; set; }

    public float? ActualRunningTimeCl { get; set; }

    public float? SvbuTime { get; set; }

    public float? ActiveSealingPower { get; set; }

    public float? OnmzMeasurement { get; set; }

    public int? DisablingGetId { get; set; }

    public int? IcActId { get; set; }

    public string? PassportNumber { get; set; }

    public float? CorrelationCoefficientCl { get; set; }

    public float? CorrelationCoefficientOp { get; set; }

    public string? Limbs { get; set; }

    public string? PassportOnmz { get; set; }

    public string? DriveWeight { get; set; }

    public string? LocationKruza { get; set; }

    public string? CabinetLocation { get; set; }

    public virtual ArmatureDrive? ArmatureDrive { get; set; }

    public virtual DisablingGet? DisablingGet { get; set; }

    public virtual IcAct? IcAct { get; set; }

    public virtual TestResult? TestResult { get; set; }

    public virtual Workshop? Workshop { get; set; }
}
