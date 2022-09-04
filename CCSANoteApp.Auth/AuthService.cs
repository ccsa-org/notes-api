using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSANoteApp.Auth
{
    public class AuthService : IAuthService
    {
        public TokenModel GetTokenModel(UserIdentityModel model)
        {
            throw new NotImplementedException();
        }

        public TokenModel GetTokenModel(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
