using System;
using System . Collections . Generic;
using System . IdentityModel . Tokens . Jwt;
using System . Linq;
using System . Security . Claims;
using System . Text;

using Microsoft . IdentityModel . Tokens;

namespace restSharp . Auth
{
  public class JWTAuth
  {
    private readonly IDictionary<string, string> _users = new Dictionary<string, string>()
    {
      { "sergei_pogrebniak", "1234" }
    };

    private readonly string _key;

    public JWTAuth(string key)
    {
      _key = key;
    }

    private bool CredentialsExists(string name, string password)
    {
      return _users.Any(pair => pair.Key == name && pair.Value == password);
    }

    public string GetToken(string name, string password)
    {
      if(!CredentialsExists(name, password))
      {
        return String.Empty;
      }

      byte[] jwtKey = Encoding.ASCII.GetBytes(_key);

      JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
      SecurityTokenDescriptor jwtDescriptor = new SecurityTokenDescriptor()
      {
        Subject = new ClaimsIdentity(
          new Claim[]
          {
            new Claim(ClaimTypes.Name, name)
          }
        ),
        Expires = DateTime.UtcNow.AddMinutes(10),
        SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(jwtKey),
          SecurityAlgorithms.HmacSha256Signature
        )
      };

      SecurityToken jwtToken = jwtHandler.CreateToken(jwtDescriptor);

      return jwtHandler.WriteToken(jwtToken);
    }
  }
}
