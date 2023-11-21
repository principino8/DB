using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DB;

public partial class EpaContext : DbContext
{
    private readonly StreamWriter _logStream = new("logging.txt", true);

    public override void Dispose()
    {
        base.Dispose();
        _logStream.Dispose();
    }

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        await _logStream.DisposeAsync();
    }

    public virtual DbSet<ArmatureDrive> ArmaturesDrives { get; set; }

    public virtual DbSet<ArmatureType> ArmaturesTypes { get; set; }

    public virtual DbSet<DisablingGet> DisablingGetArmatures { get; set; }

    public virtual DbSet<ElectricDrive> ElectricDrives { get; set; }

    public virtual DbSet<IcAct> IcActs { get; set; }

    public virtual DbSet<KKS> KKSes { get; set; }

    public virtual DbSet<KKSProtocol> KKSProtocols { get; set; }

    public virtual DbSet<MeasurementDate> MeasurementsDates { get; set; }

    public virtual DbSet<Protocol> Protocols { get; set; }

    public virtual DbSet<ProtocolDate> ProtocolsDates { get; set; }

    public virtual DbSet<TechnicalCondition> TechnicalConditions { get; set; }

    public virtual DbSet<TestResult> TestsResults { get; set; }

    public virtual DbSet<Workshop> Workshops { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false).Build();

        if (optionsBuilder.IsConfigured == false)
        {
            optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            optionsBuilder.LogTo(_logStream.WriteLine, LogLevel.Warning);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArmatureDrive>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("armaturedrive_pkey");

            entity.ToTable("armaturedrive");

            entity.HasIndex(e => e.ArmatureTypeId, "armaturetype_fkey");

            entity.HasIndex(e => e.ElectricDriveId, "electricdrive_fkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArmatureTypeId).HasColumnName("armaturetype_id");
            entity.Property(e => e.ControlRange).HasColumnName("control_range");
            entity.Property(e => e.ElectricDriveId).HasColumnName("electricdrive_id");
            entity.Property(e => e.MaxTime).HasColumnName("max_time");
            entity.Property(e => e.MinTime).HasColumnName("min_time");
            entity.Property(e => e.NominalCurrent).HasColumnName("nominal_current");
            entity.Property(e => e.NominalPower).HasColumnName("nominal_power");
            entity.Property(e => e.OutputShaftRevolutionsQuantity).HasColumnName("output_shaft_revolutions_quantity");
            entity.Property(e => e.Resource).HasColumnName("resource");
            entity.Property(e => e.RotationSpeedPerMinute).HasColumnName("rotation_speed_per_minute");

            entity.HasOne(d => d.ArmatureType).WithMany(p => p.ArmaturesDrives)
                .HasForeignKey(d => d.ArmatureTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("armaturedrive_armaturetype_id_fkey");

            entity.HasOne(d => d.ElectricDrive).WithMany(p => p.ArmaturesDrives)
                .HasForeignKey(d => d.ElectricDriveId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("armaturedrive_electricdrive_id_fkey");
        });

        modelBuilder.Entity<ArmatureType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("armaturetype_pkey");

            entity.ToTable("armaturetype");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('armaturetype_armature_name_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.TechnicalConditionsId).HasColumnName("technicalconditions_id");

            entity.HasOne(d => d.TechnicalConditions).WithMany(p => p.ArmaturesTypes)
                .HasForeignKey(d => d.TechnicalConditionsId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("armaturetype_technicalconditions_id_fkey");
        });

        modelBuilder.Entity<DisablingGet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("disabling_get_pkey");

            entity.ToTable("disabling_get");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('disabling_get_disabling_option_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.DisablingOption).HasColumnName("disabling_option");
        });

        modelBuilder.Entity<ElectricDrive>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("electricdrive_pkey");

            entity.ToTable("electricdrive");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('electricdrive_drive_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Manufacturer).HasColumnName("manufacturer");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<IcAct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ic_act_pkey");

            entity.ToTable("ic_act");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Act).HasColumnName("act");
        });

        modelBuilder.Entity<KKS>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("kks_pkey");

            entity.ToTable("kks");

            entity.HasIndex(e => e.ArmatureDriveId, "armaturedrive_fkey");

            entity.HasIndex(e => e.IcActId, "ic_act_fkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActiveSealingPower).HasColumnName("active_sealing_power");
            entity.Property(e => e.ActualRunningTimeCl).HasColumnName("actual_running_time_cl");
            entity.Property(e => e.ActualRunningTimeOp).HasColumnName("actual_running_time_op");
            entity.Property(e => e.ArmatureDriveId).HasColumnName("armaturedrive_id");
            entity.Property(e => e.CabinetLocation).HasColumnName("cabinet_location");
            entity.Property(e => e.CorrelationCoefficientCl).HasColumnName("correlation_coefficient_cl");
            entity.Property(e => e.CorrelationCoefficientOp).HasColumnName("correlation_coefficient_op");
            entity.Property(e => e.DisablingGetId).HasColumnName("disabling_get_id");
            entity.Property(e => e.DriveWeight).HasColumnName("drive_weight");
            entity.Property(e => e.EquipmentName).HasColumnName("equipment_name");
            entity.Property(e => e.EquipmentRoom).HasColumnName("equipment_room");
            entity.Property(e => e.IcActId).HasColumnName("ic_act_id");
            entity.Property(e => e.Limbs).HasColumnName("limbs");
            entity.Property(e => e.LocationKruza).HasColumnName("location_kruza");
            entity.Property(e => e.MeasurementNotes).HasColumnName("measurement_notes");
            entity.Property(e => e.OnmzMeasurement).HasColumnName("onmz_measurement");
            entity.Property(e => e.PassportNumber).HasColumnName("passport_number");
            entity.Property(e => e.PassportOnmz).HasColumnName("passport_onmz");
            entity.Property(e => e.Recommendations).HasColumnName("recommendations");
            entity.Property(e => e.SafetyClass).HasColumnName("safety_class");
            entity.Property(e => e.SmoothnessPercentageCl).HasColumnName("smoothness_percentage_cl");
            entity.Property(e => e.SmoothnessPercentageOp).HasColumnName("smoothness_percentage_op");
            entity.Property(e => e.StoClass).HasColumnName("sto_class");
            entity.Property(e => e.SvbuTime).HasColumnName("svbu_time");
            entity.Property(e => e.SystemName).HasColumnName("system_name");
            entity.Property(e => e.TestResultId).HasColumnName("testresult_id");
            entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

            entity.HasOne(d => d.ArmatureDrive).WithMany(p => p.KKSes)
                .HasForeignKey(d => d.ArmatureDriveId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("kks_armaturedrive_id_fkey");

            entity.HasOne(d => d.DisablingGet).WithMany(p => p.KKSes)
                .HasForeignKey(d => d.DisablingGetId)
                .HasConstraintName("kks_disabling_get_id_fkey");

            entity.HasOne(d => d.IcAct).WithMany(p => p.KKSes)
                .HasForeignKey(d => d.IcActId)
                .HasConstraintName("kks_ic_act_id_fkey");

            entity.HasOne(d => d.TestResult).WithMany(p => p.KKSes)
                .HasForeignKey(d => d.TestResultId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("kks_testresult_id_fkey");

            entity.HasOne(d => d.Workshop).WithMany(p => p.KKSes)
                .HasForeignKey(d => d.WorkshopId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("kks_workshop_id_fkey");
        });

        modelBuilder.Entity<KKSProtocol>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("kksprotocol");

            entity.HasIndex(e => e.KKSId, "kks_fkey");

            entity.HasIndex(e => e.ProtocolDateId, "protocoldate_fkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('kksprotocol_kksprotocol_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.KKSId).HasColumnName("kks_id");
            entity.Property(e => e.ProtocolDateId).HasColumnName("protocoldate_id");

            entity.HasOne(d => d.KKS).WithMany()
                .HasForeignKey(d => d.KKSId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("kksprotocol_kks_id_fkey");

            entity.HasOne(d => d.ProtocolDate).WithMany()
                .HasForeignKey(d => d.ProtocolDateId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("kksprotocol_protocoldate_id_fkey");
        });

        modelBuilder.Entity<MeasurementDate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("measurementdate_pkey");

            entity.ToTable("measurementdate");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('measurementdate_measurement_date_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Date).HasColumnName("measurement_date");
        });

        modelBuilder.Entity<Protocol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("protocol_pkey");

            entity.ToTable("protocol");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('protocol_protocol_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<ProtocolDate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("protocoldate_pkey");

            entity.ToTable("protocoldate");

            entity.HasIndex(e => e.MeasurementDateId, "measurementdate_fkey");

            entity.HasIndex(e => e.ProtocolId, "protocol_fkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('protocoldate_protocoldate_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.MeasurementDateId).HasColumnName("measurementdate_id");
            entity.Property(e => e.ProtocolId).HasColumnName("protocol_id");

            entity.HasOne(d => d.MeasurementDate).WithMany(p => p.ProtocolDates)
                .HasForeignKey(d => d.MeasurementDateId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("protocoldate_measurementdate_id_fkey");

            entity.HasOne(d => d.Protocol).WithMany(p => p.ProtocolsDates)
                .HasForeignKey(d => d.ProtocolId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("protocol_id_fkey");
        });

        modelBuilder.Entity<TechnicalCondition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("technicalconditions_pkey");

            entity.ToTable("technicalconditions");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('technicalconditions_tc_name_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.ValveManufacturer).HasColumnName("valve_manufacturer");
        });

        modelBuilder.Entity<TestResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("testresult_pkey");

            entity.ToTable("testresult");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('testresult_test_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Result).HasColumnName("result");
        });

        modelBuilder.Entity<Workshop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workshop_pkey");

            entity.ToTable("workshop");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('workshop_workshop_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });
    }
}
