﻿using APISYMBOL.Dto.Account;
using APISYMBOL.Interface;
using APISYMBOL.Model;
using APISYMBOL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APISYMBOL.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController:ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenServices _tokenServices;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> appusser,ITokenServices tokenServices, SignInManager<AppUser> signInManager)
        {
            _userManager = appusser;
            _tokenServices = tokenServices;
            _signInManager = signInManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    //var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    //if (roleResult.Succeeded)
                    //{
                       return Ok(
                    new NewUserDto
                    {
                        UserName = appUser.UserName,
                        Email = appUser.Email,
                        Token = _tokenServices.CreateToken(appUser)
                    }
                       );
                    //}
                    //else
                    //{
                    //    return StatusCode(500, roleResult.Errors);
                    //}
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto logindto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == logindto.Username.ToLower());
            if (user == null) return Unauthorized("Invalid username");
            var result = await _signInManager.CheckPasswordSignInAsync(user, logindto.Password, false);
            if (!result.Succeeded) return Unauthorized("Username not found and/or password");
            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenServices.CreateToken(user)
                }

                );
        }
    }
}

