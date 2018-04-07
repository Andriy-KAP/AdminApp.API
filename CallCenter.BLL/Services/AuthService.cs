using CallCenter.BLL.Core;
using CallCenter.DAL.Core;
using CallCenter.DAL.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.BLL.DTO
{
    public class AuthService : IAuthService
    {
        private IEntityRepository<User> userRepository;
        private ICryptoService cryptoService;

        public AuthService(IEntityRepository<User> userRepository, ICryptoService cryptoService)
        {
            this.userRepository = userRepository;
            this.cryptoService = cryptoService;
        }

        public async Task<string> Login(UserDTO userDTO)
        {
            User user = await userRepository.FindBy(_=>_.Email == userDTO.Email).FirstOrDefaultAsync();
            string hashedPassword = cryptoService.EncryptPassword(userDTO.Password);
            if(user != null && user.HashedPassword == hashedPassword)
            {
                string token = CreateToken(user.Email);
                return token;
            }
            return null;
        }

        public bool IsUserExist(string userEmail)
        {
            var targetUser = userRepository.FindBy(_ => _.Email == userEmail).FirstOrDefault();

            return targetUser != null ? true : false;
        }

        private string CreateToken(string userEmail)
        {
            //Set issued at date
            DateTime issueDate = DateTime.UtcNow;
            //set the time when expores
            DateTime expires = DateTime.UtcNow.AddDays(7);
            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userEmail)
            });
            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            var securituKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(sec));
            var signingCredentials = new SigningCredentials(securituKey, SecurityAlgorithms.HmacSha256Signature);

            //create jwt
            var token = (JwtSecurityToken)
                tokenHandler.CreateJwtSecurityToken(issuer: "http://localhost:55484", audience: "http://localhost:55484",
                        subject: claimsIdentity, notBefore: issueDate, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
