using ApplicationCore.Entities;

namespace ApplicationCore.Models;

public class LoginResponseModel
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<RoleModel> Roles { get; set; }
}