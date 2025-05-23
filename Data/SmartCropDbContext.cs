namespace SmartCrop.Data;

using Microsoft.EntityFrameworkCore;
using SmartCrop.Shared.Models;

public class SmartCropDbContext : DbContext
{
    public SmartCropDbContext(DbContextOptions<SmartCropDbContext> options)
        : base(options) { }

    public DbSet<Farmer> Farmers { get; set; }
    public DbSet<Field> Fields { get; set; }
    public DbSet<Crop> Crops { get; set; }
    public DbSet<SoilData> SoilData { get; set; }
    public DbSet<WeatherData> WeatherData { get; set; }
    public DbSet<HealthStatus> HealthStatuses { get; set; }
    public DbSet<Recommendation> Recommendations { get; set; }
}
