﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure.Core;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HR.LeaveManagement.Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtSettings _jwtSettings;
    
    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings.Value;
    }
    
    public async Task<AuthResponse> Login(AuthRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null) throw new NotFoundException($"User with {request.Email} not found!", request.Email);

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded) throw new BadRequestException($"Credentails for {request.Email} not valid");

        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

        var response = new AuthResponse()
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email,
            UserName = user.UserName
        };

        return response;
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest registrationRequest)
    {
        var user = new ApplicationUser()
        {
            UserName = registrationRequest.UserName,
            Email = registrationRequest.Email,
            FirstName = registrationRequest.FirstName,
            LastName = registrationRequest.LastName,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, registrationRequest.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Employee");
            return new RegistrationResponse() { UserId = user.Id };
        }
        else
        {
            StringBuilder str = new StringBuilder();
            foreach (var err in result .Errors)
            {
                str.AppendFormat("{0}\n", err.Description);
            }
            throw new BadRequestException($"{str}");
        }

    }
    
    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }.Union(userClaims)
            .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentails = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.DurationMinutes),
            signingCredentials: signingCredentails);

        return jwtSecurityToken;
    }
}