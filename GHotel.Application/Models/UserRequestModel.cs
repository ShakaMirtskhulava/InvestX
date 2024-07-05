namespace GHotel.Application.Models;
#pragma warning disable CS8618

public class UserRequestModel
{
    public string PersonalNumber { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Password { get; set; }
}
