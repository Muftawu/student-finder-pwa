namespace api.domain;
using System.Text;

public static class Utilities
{
    public static string GenerateReferenceNumber()
    {
        Random rand = new Random(Guid.NewGuid().GetHashCode());
        
        StringBuilder referenceNumber = new StringBuilder();
        
        for (int i = 0; i < 2; i++)
        {
            referenceNumber.Append(rand.Next(1000, 9999));
        }

        return referenceNumber.ToString();
    }

    public static string GenerateEmail(string firstName, string lastName)
    {
        var email = $"{firstName.Trim().ToLower()[0]}.{lastName.Trim().ToLower()[0]}.{Guid.NewGuid().ToString().Substring(0, 6)}@gmail.com";
        return email;
    }

    public static string GenerateUserId()
    {
        Guid userId = Guid.NewGuid();
        return userId.ToString();
    }

}
