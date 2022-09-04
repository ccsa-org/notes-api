using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSANoteApp.Auth
{
    public interface IAuthService
    {
        TokenModel GetTokenModel(UserIdentityModel model);
        TokenModel GetTokenModel(string refreshToken);
    }
}
