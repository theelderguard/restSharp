using Microsoft . AspNetCore . Builder;
using Microsoft . AspNetCore . Authentication . JwtBearer;
using Microsoft . IdentityModel . Tokens;
using Microsoft . Extensions . Hosting;
using Microsoft . AspNetCore . Hosting;

namespace restSharp
{
  public class Program
  {
    public static void Main ( string [ ] args )
    {
      CreateHostBuilder ( args ) . Build ( ) . Run ( );
    }

    public static IHostBuilder CreateHostBuilder ( string [ ] args ) =>
        Host . CreateDefaultBuilder ( args )
            . ConfigureWebHostDefaults ( webBuilder =>
            {
              webBuilder . UseStartup<Startup> ( );
            } );
  }
}
