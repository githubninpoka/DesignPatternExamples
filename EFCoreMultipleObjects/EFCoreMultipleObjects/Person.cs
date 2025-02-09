using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreMultipleObjects;

public class Person
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Guid PlanetId { get; set; }

    public Planet? Planet { get; set; }
}
