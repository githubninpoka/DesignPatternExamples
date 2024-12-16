using System.ComponentModel.DataAnnotations;

namespace BlazorStatePersistency;

public class MainLayoutPerson
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
}
