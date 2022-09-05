using CCSANoteApp.DB.Repositories;
using CCSANoteApp.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CCSANoteApp.Auth
{
    public class AuthService : IAuthService
    {
        public IHttpContextAccessor HttpContextAccessor { get; }
        public TokenRepository Repository { get; }
        readonly string secretKey;

        public AuthService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, TokenRepository repository)
        {
            HttpContextAccessor = httpContextAccessor;
            secretKey = configuration["AuthSecret"];
            Repository = repository;
        }

        public UserIdentityModel GetUserIdentity()
        {
            var user = HttpContextAccessor.HttpContext.User;
            if (user != null)
            {
                return new UserIdentityModel
                {
                    Email = user.FindFirst(ClaimTypes.Email).Value,
                    Name = user.FindFirst(ClaimTypes.Name).Value,
                    Identifier = user.FindFirst(ClaimTypes.NameIdentifier).Value,
                };

            }
            else
            {
                throw new Exception("Invalid User");
            }
           
        }

        public TokenModel GetTokenModel(UserIdentityModel model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedKey = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.Name,model.Name),
                            new Claim(ClaimTypes.Email,model.Email),
                            new Claim(ClaimTypes.NameIdentifier,model.Identifier),
                            new Claim(ClaimTypes.Role,"Administrator")
                        }
                    ),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(encodedKey), SecurityAlgorithms.HmacSha256),
                Expires = DateTime.UtcNow.AddMinutes(5)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            //Data Transfer Token

            var tokenModel = new TokenModel
            {
                Token = accessToken,
                RefreshToken = refreshToken,
            };

            //Token to be saved in the database

            SaveToken(model, refreshToken);
            return tokenModel;
        }

        public TokenModel GetTokenModel(string refreshToken)
        {
            var tokenData = Repository.GetTokenByRefreshToken(refreshToken);
            if (tokenData != null)
            {
                if (tokenData.TokenExpiry > DateTime.UtcNow)
                {
                    return GetTokenModel(
                        new UserIdentityModel
                        {
                            Email = tokenData.Email,
                            Identifier = tokenData.Identifier,
                            Name = tokenData.Name
                        });
                }
                else
                {
                    throw new UnauthorizedAccessException("Please login again");
                }
            }
            else
            {
                throw new UnauthorizedAccessException("Invalid User Token");
            }
        }

        private void SaveToken(UserIdentityModel model, string refreshToken)
        {
            var tokenData = new TokenData
            {
                Email = model.Email,
                RefreshToken = refreshToken,
                TokenExpiry = DateTime.UtcNow.AddHours(1),
                Name = model.Name,
                Identifier = model.Identifier
            };

            Repository.Add(tokenData);
        }

        static string GenerateRefreshToken()
        {
            var refreshToken = "";
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken = Convert.ToBase64String(randomNumber);
            }
            return refreshToken;
        }
    }
}
