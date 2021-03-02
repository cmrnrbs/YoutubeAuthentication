using System;
using System.IdentityModel.Tokens.Jwt;

namespace YoutubeAuthentication.Helpers
{
    public sealed class JwtToken
    {
        private JwtSecurityToken token;
        internal JwtToken(JwtSecurityToken token)
        {
            this.token = token;
        }

        public DateTime ValidTo => this.token.ValidTo;
        public String Value => new JwtSecurityTokenHandler().WriteToken(this.token);
    }
}
