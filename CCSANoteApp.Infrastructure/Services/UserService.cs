using CCSANoteApp.DB.Repositories;
using CCSANoteApp.Domain;
using CCSANoteApp.Domain.DTOs;

namespace CCSANoteApp.Infrastructure
{
    public class UserService : IUserService
    {
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        private readonly UserRepository _userRepository;

        public User FetchUserByLogin(string email, string password)
            {
              var profile = GetUserByMail(email);
               if (profile != null)
               {
                    if (profile.Password == password)
                    {
                        return profile;
                    }
                    else
                    {
                        throw new NullReferenceException("Password does not match");
                    }
                }
                throw new NullReferenceException("Invalid User");
            }
        
        public void CreateUser(string username, string email, string password)
        {
            _userRepository.Add(new User
            {
                Email = email,
                Username = username,
                Password = password
            });
        }

        public void CreateUser(User user)
        {
            _userRepository.Add(user);
        }

        public void DeleteUser(Guid id)
        {
            _userRepository.DeleteById(id);
        }

        User GetUserByMail(string email)
        {

            var user = _userRepository.GetByEmail(email);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            return user;
        }

        public FetchUserDto GetUser(Guid userId)
        {
            
            var user = _userRepository.GetById(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found", userId.ToString());
            }

            var result = new FetchUserDto
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email
            };
            result.UserNotes = CreateNoteList(user);
            return result;
        }

        private List<FetchNoteDto> CreateNoteList(User user)
        {
            List<FetchNoteDto> result = new();
            foreach (var note in user.Notes)
            {
                result.Add(new FetchNoteDto()
                {
                    NoteId = note.Id,
                    Title = note.Title,
                    Content = note.Content,
                    NoteCreatorUserName = note.NoteCreator.Username,
                    GroupName = note.GroupName,
                    CreatedDate = note.CreatedDate,
                    UpdatedDate = note.UpdatedDate
                });
            }
            return result;
        }

        public List<FetchUserDto> GetUsers()
        {
            
            var users = _userRepository.GetAll();
            List<FetchUserDto> _users = new();
            var _user = new FetchUserDto();
            foreach (var user in users)
            {
                _user = new FetchUserDto() { Email = user.Email, Username = user.Username, UserId = user.Id };
                _user.UserNotes = CreateNoteList(user);
                _users.Add(_user);
            }
            return _users;
        }

        public void UpdateUserEmail(Guid id, string email)
        {
            var user = _userRepository.GetById(id);
            if (user != null)
            {
                user.Email = email;
                _userRepository.Update(user);
            }
        }

        public void UpdateUserName(Guid id, string username)
        {
            var user = _userRepository.GetById(id);
            if (user != null)
            {
                user.Username = username;
                _userRepository.Update(user);
            }
        }

        
    }
}
