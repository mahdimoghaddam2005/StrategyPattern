using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using StrategyPattern.Entities;

namespace StrategyPattern.Infrastructure;
public class SmsContext : DbContext
{
    public SmsContext(DbContextOptions<SmsContext> options) : base(options)
    {
        
    }
    public DbSet<ShortMessageService> ShortMessageServices { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ShortMessageService>().HasKey(p => p.Id);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // For Use Sql Server And Get Connection Strings
        options.UseSqlServer(new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build()
        .GetConnectionString("SqlServer"));
    }
}