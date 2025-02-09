using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace EFCoreMultipleObjects;

internal class Program
{
    // 4 packages
    // tools
    // sql server
    // user secrets
    // json config
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        ConfigurationBuilder builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        builder.AddUserSecrets<Program>();

        IConfiguration config = builder.Build();
        Console.WriteLine(config.GetConnectionString("default"));

        // N.B. When creating a project and you want to create the database code-first
        // through entity framework, there are several "designs" that are understood by EF.
        // Not recognized is:
        // - create a DbContextOptionsBuilder with usesqlserver and then
        // - craete options from it (var options = optionsbuilder.Options;
        // - pass those options to the constructor of the DbContext.
        // In that case, EF tools get confused when creating a database
        // What is however recognized is when you configure a dbcontext through a service
        // the way a normal webapi or blazor project is set up. then you can set options
        // through the AddDbContext<type>() configuration.
        // another way that works is by entering the connectionstring directly in the DbContext Onconfiguring
        // in the way I've done now.
        // of course this is unsafe. but apparently unavoidable with my limited knowledge.
        // EF core tools need a parameterless constructor and a direct connectionstring
        // or a services configuration.
        //DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
        //optionsBuilder.UseSqlServer(config.GetConnectionString("default"));
        //DbContextOptions options = optionsBuilder.Options;

        using var _db = new EFTestDbContext(config);

        _db.Persons.RemoveRange(_db.Persons);
        _db.Planets.RemoveRange(_db.Planets);
        _db.SaveChanges();

        
        Planet earth = new Planet() { Name = "earth" };
        _db.Planets.Add(earth);
        _db.SaveChanges();

        Person p1 = new Person() { Name = "Marco" };
        var planet = _db.Planets.FirstOrDefault(x => x.Name == "earth");
        p1.Planet = planet == null ? new Planet() { Name = "earth" } : planet;
        _db.Persons.Add(p1);
        _db.SaveChanges();

        Person p2 = new Person() { Name = "Marlies" };
        p2.Planet = planet == null ? new Planet() { Name = "earth" } : planet;
        _db.Persons.Add(p2);
        _db.SaveChanges();

        // and this, this is what I was all about with EF.
        // I finally figured it out. How to add complex objects with FK's
        // without duplicating those related records.
        // it had to click in my mind.

        Person p3 = new Person() { Name = "Kylian" };
        p3.Planet = _db.Planets.Any(x => x.Name == "Eindhoven") ?
            _db.Planets.First(x => x.Name == "Eindhoven") : 
            new Planet() { Name = "Eindhoven" };
        _db.Persons.Add(p3);
        _db.SaveChanges();
        Guid planetid = _db.Planets.Where(x => x.Name.Equals("Eindhoven")).Select(x => x.Id).FirstOrDefault();
        Planet planetX = _db.Planets.Where(x => x.Name.Equals("Eindhoven")).FirstOrDefault();
        _db.Entry<Planet>(p3.Planet).State = EntityState.Detached;
        planetX.Name = "Den Bosch";
        _db.Update(planetX);

        Person p4 = new Person() { Name = "clau",Planet=planetX };
        //p4.Planet = new Planet() { Id = planetid, Name="den bosch" }; // error because key already exists
        _db.Persons.Add(p4);
        _db.SaveChanges();
        planetX.Name = "Heerhugowaard";
        _db.Update(planetX);
        _db.SaveChanges();

        Person p5 = new Person { Name = "Banaan", PlanetId = planetid }; // when using initiator instead of constructor with fixed parameters, EF core can work with just the planetid.
        _db.Persons.Add(p5);
        _db.SaveChanges();

        var people = _db.Persons
                        .Include(x => x.Planet)
                        .ToList();
        foreach (var person in people)
        {
            Console.WriteLine($"{person.Name} with id: {person.Id} lives at location: {person.Planet.Name}");
        }

        Console.WriteLine("Press enter to end.");
        Console.ReadLine();
    }
}
