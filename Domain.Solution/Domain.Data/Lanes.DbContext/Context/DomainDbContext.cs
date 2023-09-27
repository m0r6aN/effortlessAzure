#nullable disable

namespace MF.Libraries.Data.Lanes.Context;

public partial class DomainDbContext : DbContext
{
    public virtual DbSet<RateConDetailRate> RateConDetailRate { get; set; }

    public virtual DbSet<RateConInfo> RateConInfo { get; set; }

    public virtual DbSet<RateConInvoiceId> RateConInvoiceId { get; set; }

    public virtual DbSet<RateConReceiver> RateConReceiver { get; set; }

    public virtual DbSet<RateConShipper> RateConShipper { get; set; }

    public virtual DbSet<RateTypes> RateTypes { get; set; }

    public DomainDbContext(DbContextOptions<DomainDbContext> options)
                                : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Configurations.RateConDetailRateConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.RateConInfoConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.RateConInvoiceIdConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.RateConReceiverConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.RateConShipperConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.RateTypesConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}