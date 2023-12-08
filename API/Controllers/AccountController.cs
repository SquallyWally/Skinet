﻿using API.Dtos;
using API.Errors;

using Core.Entities.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;

    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(
        UserManager<AppUser>   userManager,
        SignInManager<AppUser> signInManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(
        LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if ( user == null )
            return Unauthorized(new ApiResponse(401));

        var result = await _signInManager.CheckPasswordSignInAsync(
            user,
            loginDto.Password,
            false);

        if ( !result.Succeeded )
            return Unauthorized(new ApiResponse(401));

        return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = "some token",
            };
    }
}
