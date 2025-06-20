using ESG.Common.Data;
using ESG.Common.Identity.Abstractions;
using ESG.IDMS.Core.IDMS;
using Microsoft.EntityFrameworkCore;

namespace ESG.IDMS.Infrastructure.Data;

public class ApplicationContext(DbContextOptions<ApplicationContext> options,
                          IAuthenticatedUser authenticatedUser) : AuditableDbContext<ApplicationContext>(options, authenticatedUser)
{
    private readonly IAuthenticatedUser _authenticatedUser = authenticatedUser;
    public DbSet<ReportState> Report { get; set; } = default!;  
    public DbSet<ReportQueryFilterState> ReportQueryFilter { get; set; } = default!;
    public DbSet<ReportRoleAssignmentState> ReportRoleAssignment { get; set; } = default!;
	public DbSet<ReportAIIntegrationState> ReportAIIntegration { get; set; } = default!;
	public DbSet<UploadProcessorState> UploadProcessor { get; set; } = default!;
	public DbSet<ApprovalState> Approval { get; set; } = default!;
	public DbSet<ApproverSetupState> ApproverSetup { get; set; } = default!;
	public DbSet<ApproverAssignmentState> ApproverAssignment { get; set; } = default!;
	public DbSet<ApprovalRecordState> ApprovalRecord { get; set; } = default!;
 
    public DbSet<FireArmsState> FireArms { get; set; } = default!;
	public DbSet<RadioEquipmentState> RadioEquipment { get; set; } = default!;
	public DbSet<VehicleState> Vehicle { get; set; } = default!;
	public DbSet<OtherEquipmentState> OtherEquipment { get; set; } = default!;
	public DbSet<FurnitureAndFixtureState> FurnitureAndFixture { get; set; } = default!;
	public DbSet<LocationState> Location { get; set; } = default!;
	public DbSet<StatusState> Status { get; set; } = default!;
	public DbSet<FireArmsBrandModelState> FireArmsBrandModel { get; set; } = default!;
	public DbSet<RadioEquipmentBrandModelState> RadioEquipmentBrandModel { get; set; } = default!;
	public DbSet<VehicleBrandModelState> VehicleBrandModel { get; set; } = default!;
	public DbSet<OtherEquipmentBrandModelState> OtherEquipmentBrandModel { get; set; } = default!;
	public DbSet<FurnitureAndFixtureBrandModelState> FurnitureAndFixtureBrandModel { get; set; } = default!;
	public DbSet<RemarksState> Remarks { get; set; } = default!;
	
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                                                   .SelectMany(t => t.GetProperties())
                                                   .Where(p => p.ClrType == typeof(decimal)
                                                               || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,6)");
        }
		foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties()
                                               .Where(p => p.Name.Equals("CreatedBy", StringComparison.OrdinalIgnoreCase)
                                               || p.Name.Equals("LastModifiedBy", StringComparison.OrdinalIgnoreCase)
                                               || p.Name.Equals("Entity", StringComparison.OrdinalIgnoreCase)
                                               || p.Name.Equals("LastModifiedDate", StringComparison.OrdinalIgnoreCase)
                                               || p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase)))
            {
                if (!property.Name.Equals("LastModifiedDate", StringComparison.OrdinalIgnoreCase))
                {
                    property.SetMaxLength(36);
                }
                entityType.AddIndex(property);
            }
        }
        #region Disable Cascade Delete
        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
        .SelectMany(t => t.GetForeignKeys())
        .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
        foreach (var fk in cascadeFKs)
        {
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
        #endregion
        modelBuilder.Entity<Audit>().Property(e => e.PrimaryKey).HasMaxLength(120);
        modelBuilder.Entity<Audit>().HasIndex(p => p.PrimaryKey);
		modelBuilder.Entity<Audit>().HasIndex(p => p.TraceId);
        modelBuilder.Entity<Audit>().HasIndex(p => p.DateTime);
		modelBuilder.Entity<UploadProcessorState>().HasQueryFilter(e => _authenticatedUser.Entity!.Equals(Core.Constants.Entities.Default, StringComparison.CurrentCultureIgnoreCase) || e.Entity == _authenticatedUser.Entity);      
        // NOTE: DO NOT CREATE EXTENSION METHOD FOR QUERY FILTER!!!
        // It causes filter to be evaluated before user has signed in
        modelBuilder.Entity<FireArmsState>().HasQueryFilter(e => _authenticatedUser.Entity!.Equals(Core.Constants.Entities.Default, StringComparison.CurrentCultureIgnoreCase) || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<RadioEquipmentState>().HasQueryFilter(e => _authenticatedUser.Entity!.Equals(Core.Constants.Entities.Default, StringComparison.CurrentCultureIgnoreCase) || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<VehicleState>().HasQueryFilter(e => _authenticatedUser.Entity!.Equals(Core.Constants.Entities.Default, StringComparison.CurrentCultureIgnoreCase) || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<OtherEquipmentState>().HasQueryFilter(e => _authenticatedUser.Entity!.Equals(Core.Constants.Entities.Default, StringComparison.CurrentCultureIgnoreCase) || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<FurnitureAndFixtureState>().HasQueryFilter(e => _authenticatedUser.Entity!.Equals(Core.Constants.Entities.Default, StringComparison.CurrentCultureIgnoreCase) || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<LocationState>().HasQueryFilter(e => _authenticatedUser.Entity!.Equals(Core.Constants.Entities.Default, StringComparison.CurrentCultureIgnoreCase) || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<StatusState>().HasQueryFilter(e => _authenticatedUser.Entity!.Equals(Core.Constants.Entities.Default, StringComparison.CurrentCultureIgnoreCase) || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<FireArmsBrandModelState>().HasQueryFilter(e => _authenticatedUser.Entity!.Equals(Core.Constants.Entities.Default, StringComparison.CurrentCultureIgnoreCase) || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<RadioEquipmentBrandModelState>().HasQueryFilter(e => _authenticatedUser.Entity!.Equals(Core.Constants.Entities.Default, StringComparison.CurrentCultureIgnoreCase) || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<VehicleBrandModelState>().HasQueryFilter(e => _authenticatedUser.Entity!.Equals(Core.Constants.Entities.Default, StringComparison.CurrentCultureIgnoreCase) || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<OtherEquipmentBrandModelState>().HasQueryFilter(e => _authenticatedUser.Entity!.Equals(Core.Constants.Entities.Default, StringComparison.CurrentCultureIgnoreCase) || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<FurnitureAndFixtureBrandModelState>().HasQueryFilter(e => _authenticatedUser.Entity!.Equals(Core.Constants.Entities.Default, StringComparison.CurrentCultureIgnoreCase) || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<RemarksState>().HasQueryFilter(e => _authenticatedUser.Entity!.Equals(Core.Constants.Entities.Default, StringComparison.CurrentCultureIgnoreCase) || e.Entity == _authenticatedUser.Entity);
		
        
        modelBuilder.Entity<FireArmsState>().Property(e => e.ItemDescription).HasMaxLength(255);
		modelBuilder.Entity<FireArmsState>().Property(e => e.SerialNo).HasMaxLength(255);
		modelBuilder.Entity<FireArmsState>().Property(e => e.FreeTextRemarks).HasMaxLength(255);
		modelBuilder.Entity<FireArmsState>().Property(e => e.IssuedBy).HasMaxLength(255);
		modelBuilder.Entity<FireArmsState>().Property(e => e.IssuedTo).HasMaxLength(255);
		modelBuilder.Entity<RadioEquipmentState>().Property(e => e.ItemDescription).HasMaxLength(255);
		modelBuilder.Entity<RadioEquipmentState>().Property(e => e.SerialNo).HasMaxLength(255);
		modelBuilder.Entity<RadioEquipmentState>().Property(e => e.FreeTextRemarks).HasMaxLength(255);
		modelBuilder.Entity<RadioEquipmentState>().Property(e => e.IssuedBy).HasMaxLength(255);
		modelBuilder.Entity<RadioEquipmentState>().Property(e => e.IssuedTo).HasMaxLength(255);
		modelBuilder.Entity<VehicleState>().Property(e => e.ItemDescription).HasMaxLength(255);
		modelBuilder.Entity<VehicleState>().Property(e => e.SerialNo).HasMaxLength(255);
		modelBuilder.Entity<VehicleState>().Property(e => e.FreeTextRemarks).HasMaxLength(255);
		modelBuilder.Entity<VehicleState>().Property(e => e.IssuedBy).HasMaxLength(255);
		modelBuilder.Entity<VehicleState>().Property(e => e.IssuedTo).HasMaxLength(255);
		modelBuilder.Entity<OtherEquipmentState>().Property(e => e.ItemDescription).HasMaxLength(255);
		modelBuilder.Entity<OtherEquipmentState>().Property(e => e.SerialNo).HasMaxLength(255);
		modelBuilder.Entity<OtherEquipmentState>().Property(e => e.FreeTextRemarks).HasMaxLength(255);
		modelBuilder.Entity<OtherEquipmentState>().Property(e => e.IssuedBy).HasMaxLength(255);
		modelBuilder.Entity<OtherEquipmentState>().Property(e => e.IssuedTo).HasMaxLength(255);
		modelBuilder.Entity<FurnitureAndFixtureState>().Property(e => e.ItemDescription).HasMaxLength(255);
		modelBuilder.Entity<FurnitureAndFixtureState>().Property(e => e.SerialNo).HasMaxLength(255);
		modelBuilder.Entity<FurnitureAndFixtureState>().Property(e => e.FreeTextRemarks).HasMaxLength(255);
		modelBuilder.Entity<FurnitureAndFixtureState>().Property(e => e.IssuedBy).HasMaxLength(255);
		modelBuilder.Entity<FurnitureAndFixtureState>().Property(e => e.IssuedTo).HasMaxLength(255);
		modelBuilder.Entity<LocationState>().Property(e => e.Description).HasMaxLength(255);
		modelBuilder.Entity<StatusState>().Property(e => e.Description).HasMaxLength(255);
		modelBuilder.Entity<FireArmsBrandModelState>().Property(e => e.Description).HasMaxLength(255);
		modelBuilder.Entity<RadioEquipmentBrandModelState>().Property(e => e.Description).HasMaxLength(255);
		modelBuilder.Entity<VehicleBrandModelState>().Property(e => e.Description).HasMaxLength(255);
		modelBuilder.Entity<OtherEquipmentBrandModelState>().Property(e => e.Description).HasMaxLength(255);
		modelBuilder.Entity<FurnitureAndFixtureBrandModelState>().Property(e => e.Description).HasMaxLength(255);
		modelBuilder.Entity<RemarksState>().Property(e => e.Description).HasMaxLength(255);
		
        modelBuilder.Entity<FireArmsBrandModelState>().HasMany(t => t.FireArmsList).WithOne(l => l.FireArmsBrandModel).HasForeignKey(t => t.FireArmsBrandModelId);
		modelBuilder.Entity<LocationState>().HasMany(t => t.FireArmsList).WithOne(l => l.Location).HasForeignKey(t => t.LocationId);
		modelBuilder.Entity<StatusState>().HasMany(t => t.FireArmsList).WithOne(l => l.Status).HasForeignKey(t => t.StatusId);
		modelBuilder.Entity<RemarksState>().HasMany(t => t.FireArmsList).WithOne(l => l.Remarks).HasForeignKey(t => t.RemarksId);
		modelBuilder.Entity<RadioEquipmentBrandModelState>().HasMany(t => t.RadioEquipmentList).WithOne(l => l.RadioEquipmentBrandModel).HasForeignKey(t => t.RadioEquipmentBrandModelId);
		modelBuilder.Entity<LocationState>().HasMany(t => t.RadioEquipmentList).WithOne(l => l.Location).HasForeignKey(t => t.LocationId);
		modelBuilder.Entity<StatusState>().HasMany(t => t.RadioEquipmentList).WithOne(l => l.Status).HasForeignKey(t => t.StatusId);
		modelBuilder.Entity<RemarksState>().HasMany(t => t.RadioEquipmentList).WithOne(l => l.Remarks).HasForeignKey(t => t.RemarksId);
		modelBuilder.Entity<VehicleBrandModelState>().HasMany(t => t.VehicleList).WithOne(l => l.VehicleBrandModel).HasForeignKey(t => t.VehicleBrandModelId);
		modelBuilder.Entity<LocationState>().HasMany(t => t.VehicleList).WithOne(l => l.Location).HasForeignKey(t => t.LocationId);
		modelBuilder.Entity<StatusState>().HasMany(t => t.VehicleList).WithOne(l => l.Status).HasForeignKey(t => t.StatusId);
		modelBuilder.Entity<RemarksState>().HasMany(t => t.VehicleList).WithOne(l => l.Remarks).HasForeignKey(t => t.RemarksId);
		modelBuilder.Entity<OtherEquipmentBrandModelState>().HasMany(t => t.OtherEquipmentList).WithOne(l => l.OtherEquipmentBrandModel).HasForeignKey(t => t.OtherEquipmentBrandModelId);
		modelBuilder.Entity<LocationState>().HasMany(t => t.OtherEquipmentList).WithOne(l => l.Location).HasForeignKey(t => t.LocationId);
		modelBuilder.Entity<StatusState>().HasMany(t => t.OtherEquipmentList).WithOne(l => l.Status).HasForeignKey(t => t.StatusId);
		modelBuilder.Entity<RemarksState>().HasMany(t => t.OtherEquipmentList).WithOne(l => l.Remarks).HasForeignKey(t => t.RemarksId);
		modelBuilder.Entity<FurnitureAndFixtureBrandModelState>().HasMany(t => t.FurnitureAndFixtureList).WithOne(l => l.FurnitureAndFixtureBrandModel).HasForeignKey(t => t.FurnitureAndFixtureBrandModelId);
		modelBuilder.Entity<LocationState>().HasMany(t => t.FurnitureAndFixtureList).WithOne(l => l.Location).HasForeignKey(t => t.LocationId);
		modelBuilder.Entity<StatusState>().HasMany(t => t.FurnitureAndFixtureList).WithOne(l => l.Status).HasForeignKey(t => t.StatusId);
		modelBuilder.Entity<RemarksState>().HasMany(t => t.FurnitureAndFixtureList).WithOne(l => l.Remarks).HasForeignKey(t => t.RemarksId);
		
		
		modelBuilder.Entity<UploadProcessorState>().Property(e => e.FileType).HasMaxLength(20);
        modelBuilder.Entity<UploadProcessorState>().Property(e => e.Path).HasMaxLength(450);
        modelBuilder.Entity<UploadProcessorState>().Property(e => e.Status).HasMaxLength(20);
        modelBuilder.Entity<UploadProcessorState>().Property(e => e.Module).HasMaxLength(50);
        modelBuilder.Entity<UploadProcessorState>().Property(e => e.UploadType).HasMaxLength(50);      
        base.OnModelCreating(modelBuilder);
    }
}
