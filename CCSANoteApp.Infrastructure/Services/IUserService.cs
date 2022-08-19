using CCSANoteApp.Domain;
using CCSANoteApp.Domain.DTOs;

namespace CCSANoteApp.Infrastructure
{
    public interface IUserService
    {
        void CreateUser(string username, string email, string password);
        void CreateUser(User user);
        void UpdateUserName(Guid id, string name);
        void UpdateUserEmail(Guid id, string email);
        void DeleteUser(Guid id);
        FetchUserDto GetUser(Guid id);
        List<FetchUserDto> GetUsers();
    }
}
