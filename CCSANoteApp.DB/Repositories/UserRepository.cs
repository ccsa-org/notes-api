using CCSANoteApp.Domain;

namespace CCSANoteApp.DB.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(SessionFactory sessionFactory) : base(sessionFactory)
        {
        }
        public User? GetByEmail(string email)
        {
            var model = _session.Query<User>().FirstOrDefault(x => x.Email.Equals(email));
            return model;
        }

    }

    public class TokenRepository : Repository<TokenData>
    {
        public TokenRepository(SessionFactory sessionFactory) : base(sessionFactory)
        {
        }
        public TokenData? GetTokenByRefreshToken(string refreshToken)
        {
            var model = _session.Query<TokenData>().FirstOrDefault(x => x.RefreshToken.Equals(refreshToken));
            return model;
        }
    }
}
