﻿using BJ.BLL.Configrutions.Options;
using BJ.BLL.Providers.Interfaces;
using BJ.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BJ.BLL.Providers
{
    public class JwtTokenProvider : ITokenProvider
    {
        private readonly JwtOptions _options;

        public JwtTokenProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;

        }

        public string GenerateJwtToken(string email, User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_options.JwtExpireDays));

            var token = new JwtSecurityToken(
                _options.JwtIssuer,
                _options.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );
            var result = new JwtSecurityTokenHandler().WriteToken(token);
            return result;
        }
    }
}
