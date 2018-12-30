using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scorebored.API.Helpers;
using Scorebored.API.Models;

namespace Scorebored.API.Services
{
    public interface IUserService
    {
        (User user, string accessToken) Authenticate(string username, string password);
        (User user, string accessToken) Register(string email, string password);
        User UpdateUserCurrentGroup(long userId, long groupId);
        User GetUser(long userId);
    }

    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly ScoreboredContext _dbContext;

        public UserService(IOptions<AppSettings> appSettings, ScoreboredContext dbContext)
        {
            _appSettings = appSettings.Value;
            _dbContext = dbContext;
        }

        public (User user, string accessToken) Authenticate(string email, string password)
        {
            var existingUser = _dbContext.Users.Where(user => user.Email == email).FirstOrDefault();

            if (existingUser == null)
            {
                return (null, null);
            }

            if (string.IsNullOrEmpty(password) || HashPassword(password, existingUser.Salt) != existingUser.Password)
            {
                return (existingUser, null);
            }

            existingUser.Password = null;

            return (existingUser, GetAccessToken(existingUser.UserId, existingUser.Email));
        }

        public (User user, string accessToken) Register(string email, string password)
        {
            var userCount = _dbContext.Users.Where(user => user.Email == email).Count();

            if (userCount > 0)
            {
                return Authenticate(email, password);
            }

            var newUser = new User
            {
                Email = email,
                Salt = GetSalt(),
                CreatedAt = DateTime.Now
            };

            newUser.Password = HashPassword(password, newUser.Salt);

            string accessToken = GetAccessToken(newUser.UserId, email);

            _dbContext.Add(newUser);
            _dbContext.SaveChanges();

            return (newUser, accessToken);
        }

        private string GetAccessToken(long userId, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Name, email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            return accessToken;
        }

        private string HashPassword(string password, string salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: password,
                        salt: Encoding.ASCII.GetBytes(salt),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 100,
                        numBytesRequested: 256 / 8));
        }

        private static string GetSalt()
        {
            char[] chars = new char[62];
            const int size = 256;

            chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];

            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[size];
                crypto.GetNonZeroBytes(data);
            }

            StringBuilder result = new StringBuilder(size);

            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }

            return result.ToString();
        }

        public User UpdateUserCurrentGroup(long userId, long groupId)
        {
            var user = _dbContext.Users.Find(userId);
            user.CurrentGroupId = groupId;
            _dbContext.SaveChanges();

            return user;
        }

        public User GetUser(long userId)
        {
            return _dbContext.Users.Find(userId);
        }
    }
}
