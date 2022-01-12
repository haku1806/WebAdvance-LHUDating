using HenHoCore.Dto;
using HenHoCore.Model;
using HenHoCore.Ulti;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace HenHoCore.Service
{
    public class AuthService
    {
        protected readonly MyContext context;

        public AuthService(MyContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Đăng nhập hệ thống admin
        /// </summary>
        /// <param name="user">Thông tin tài khoản có quyền admin</param>
        /// <returns>AccessToken admin</returns>
        public AccessToken LoginAdmin(User user)
        {
            string passCheck = DataHelper.SHA256Hash(user.UserName + "_" + user.Password);

            User userExist = context.Users
                .Where(x => x.UserName.Equals(user.UserName) && x.Password.Equals(passCheck) && x.Role == 1)
                .FirstOrDefault();

            if (userExist == null)
                throw new ArgumentException("Tài khoản hoặc mật khẩu không đúng");

            userExist.LastLogin = DateTime.Now;
            context.SaveChanges();

            DateTime expirationDate = DateTime.Now.Date.AddMinutes(EnviConfig.ExpirationInMinutes);
            long expiresAt = (long)(expirationDate - new DateTime(1970, 1, 1)).TotalSeconds;

            var tokenHandler = new JwtSecurityTokenHandler();
            var SecretKey = EnviConfig.SecretKey;

            var key = Encoding.ASCII.GetBytes(EnviConfig.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Sid, userExist.Code),
                        new Claim(ClaimTypes.Name, userExist.UserName),
                        new Claim(ClaimTypes.Role, userExist.Role.ToString()),
                        new Claim(ClaimTypes.Expiration, expiresAt.ToString())
                }),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AccessToken
            {
                User = userExist.Code,
                FullName = userExist.FullName,
                Role = userExist.Role,
                Token = tokenHandler.WriteToken(token),
            };
        }

        /// <summary>
        /// Đăng nhập hệ thống
        /// </summary>
        /// <param name="user">Thông tin tài khoản người dùng</param>
        /// <returns>AccessToken</returns>
        public AccessToken Login(User user)
        {
            string passCheck = DataHelper.SHA256Hash(user.UserName + "_" + user.Password);

            User userExist = context.Users
                .Where(x => x.UserName.Equals(user.UserName) && x.Password.Equals(passCheck))
                .FirstOrDefault();

            if (userExist == null)
                throw new ArgumentException("Tài khoản hoặc mật khẩu không đúng");

            userExist.LastLogin = DateTime.Now;
            context.SaveChanges();

            DateTime expirationDate = DateTime.Now.Date.AddMinutes(EnviConfig.ExpirationInMinutes);
            long expiresAt = (long)(expirationDate - new DateTime(1970, 1, 1)).TotalSeconds;

            var tokenHandler = new JwtSecurityTokenHandler();
            var SecretKey = EnviConfig.SecretKey;

            var key = Encoding.ASCII.GetBytes(EnviConfig.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Sid, userExist.Code),
                        new Claim(ClaimTypes.Name, userExist.UserName),
                        new Claim(ClaimTypes.Role, userExist.Role.ToString()),
                        new Claim(ClaimTypes.Expiration, expiresAt.ToString())
                }),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AccessToken
            {
                User = userExist.Code,
                FullName = userExist.FullName,
                Role = userExist.Role,
                Token = tokenHandler.WriteToken(token),
            };
        }

        /// <summary>
        /// Đăng ký tài khoản người dùng
        /// </summary>
        /// <param name="user">Thông tin tài khoản</param>
        public void Register(User user)
        {
            if (context.Users.Any(x => x.UserName.Equals(user.UserName)))
                throw new ArgumentException("Tài khoản đã tồn tại");

            User newUser = new User()
            {
                Code = Guid.NewGuid().ToString("N"),
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                Password = DataHelper.SHA256Hash(user.UserName + "_" + user.Password),
                Avatar = Constants.AVATAR_DEFAULT
            };

            context.Users.Add(newUser);
            context.SaveChanges();
        }

        public void PutHubConnection(string userSession, string key)
        {
            User user = this.context.Users
                .FirstOrDefault(x => x.Code.Equals(userSession));

            if (user != null)
            {
                user.CurrentSession = key;
                this.context.SaveChanges();
            }
        }
    }
}
