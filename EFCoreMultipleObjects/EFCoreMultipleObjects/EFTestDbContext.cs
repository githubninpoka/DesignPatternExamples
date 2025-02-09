using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreMultipleObjects;

public class EFTestDbContext : DbContext
{
    private readonly IConfiguration config;

    public DbSet<Person> Persons { get; set; }
    public DbSet<Planet> Planets { get; set; }

    IConfiguration _config;

    public EFTestDbContext()
    {
    }

    public EFTestDbContext(IConfiguration config)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_config.GetConnectionString("default"));
        //optionsBuilder.UseSqlServer("Data Source=NINLAPTOP\\MYSQLEXPRESS;Initial Catalog=EFTestDb;User ID=<someuser>;Password=<somepassword>;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }
}
