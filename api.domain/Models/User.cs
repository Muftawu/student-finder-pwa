using Microsoft.AspNetCore.Identity;

namespace api.domain;

public class User : IdentityUser
{

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public string UserId { get; set; } = Utilities.GenerateUserId();

    public string ReferenceNumber { get; set; } = Utilities.GenerateReferenceNumber();

    public string? Programme { get; set; }

    public string? Gender { get; set;}

    public string? UserRole { get; set; }
    
    public DateTime? DateCreated { get; set; } =  DateTime.Now;


    public User()
    {
    }

    public User (string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = Utilities.GenerateEmail(FirstName, LastName);
    }

}