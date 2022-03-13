using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Email needs to be filled in.")]
    [EmailAddress(ErrorMessage = "Email is in the incorrect format")]
    [StringLength(50, ErrorMessage = "Email exceeds 50 character limit.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password must be filled in.")]
    public string Password { get; set; }
}