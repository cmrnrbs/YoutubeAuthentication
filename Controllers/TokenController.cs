using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YoutubeAuthentication.Helpers;

namespace YoutubeAuthentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]

    public class TokenController : ControllerBase
    {

        [HttpPost]
        public IActionResult CreateToken()
        {

            var token = new JwtTokenBuilder()
                .AddSubject("Cemre Onur Baş")
                .AddClaim("MemberShipId", "ill")
                .AddIssuer("Fiver.Security.Bearer")
                .AddAudience("Fiver.Security.Bearer")
                .AddSecurityKey(JwtSecurityKey.Create("fiver-security-key")).AddExprity(5).Build();

            return Ok(token.Value);
        }
    }
}
