using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IAccountService
{
    Task<bool> ValidateUser(string email, string password);
    Task<int> CreateUser(RegisterModel model);
    
}