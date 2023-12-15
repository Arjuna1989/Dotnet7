using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using API.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> RegisterUser(RegisterDto register)
        {
            if (await UserExist(register.username)) return BadRequest("User Name already exists !");


            using var hmac = new HMACSHA512();

            var user = new User
            {
                UserName = register.username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.username.ToLower());
            if (user == null) return Unauthorized("invalid user name");
            byte[] ComputedPasswordHash;

            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {

                ComputedPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));

            }

            for (int i = 0; i < ComputedPasswordHash.Length; i++)
            {
                if (ComputedPasswordHash[i] != user.PasswordHash[i]) return Unauthorized("invalid password");
            }

            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExist(string username)
        {

            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}