using Microsoft . AspNetCore . Authorization;
using Microsoft . AspNetCore . Mvc;

namespace restSharp . Controllers
{
  [Authorize]
  [Route ( "api/[controller]" )]
  [ApiController]
  public class AuthController : ControllerBase
  {
    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody] Tables . Credentials credentials)
    {
      string jwtToken = new Auth . JWTAuth ( Auth . Global . JWTKey  ) . GetToken ( credentials . Name, credentials . Password );

      if(String.IsNullOrEmpty(jwtToken))
      {
        return Unauthorized ();
      }

      return Ok (jwtToken);
    }
  }
}
