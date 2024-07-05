namespace GHotel.Application.Models;

#pragma warning disable CS8618

public class UserResponseModel
{
    public string PersonalNumber { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
}
