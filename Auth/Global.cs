using System . Text;

namespace restSharp . Auth
{
  public class Global
  {
    public static string JWTKey
    {
      get
      {
        return "this-is-my-strongest-key";
      }
    }

    public static byte[] JWTKeyBytes
    {
      get
      {
        return Encoding . ASCII . GetBytes ( JWTKey );
      }
    }
  }
}
