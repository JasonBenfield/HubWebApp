﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XTI_HubWebAppApi;
using XTI_WebApp.Api;

namespace HubWebApp.Extensions;

public sealed class JwtAccess : AccessForAuthenticate
{
    private readonly DefaultWebAppOptions options;

    public JwtAccess(DefaultWebAppOptions options)
    {
        this.options = options;
    }

    protected override Task<string> _GenerateToken(IEnumerable<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(options.XtiAuthentication.JwtSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity
            (
                claims
            ),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials
            (
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(token);
        return Task.FromResult(accessToken);
    }
}