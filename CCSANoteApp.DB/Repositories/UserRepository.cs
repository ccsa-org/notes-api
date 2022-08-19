using CCSANoteApp.Domain;

namespace CCSANoteApp.DB.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(SessionFactory sessionFactory) : base(sessionFactory)
        {
        }
    }
}
