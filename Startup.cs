using System . Configuration;

using Microsoft . AspNetCore . Authentication . JwtBearer;
using Microsoft . AspNetCore . Builder;
using Microsoft . AspNetCore . Hosting;
using Microsoft . Extensions . Configuration;
using Microsoft . Extensions . DependencyInjection;
using Microsoft . IdentityModel . Tokens;
using Microsoft . OpenApi . Models;

namespace restSharp
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup ( IConfiguration configuration )
    {
      Configuration = configuration;
    }

    public void ConfigureServices ( IServiceCollection services )
    {

      services . AddControllers ( );
      services . AddSingleton ( new restSharp . Auth . JWTAuth ( restSharp . Auth . Global . JWTKey ) );
      services . AddAuthentication ( options => {
        options . DefaultAuthenticateScheme = JwtBearerDefaults . AuthenticationScheme;
        options . DefaultChallengeScheme = JwtBearerDefaults . AuthenticationScheme;
      } ) . AddJwtBearer ( options => {
        //options . Authority = builder . Configuration . GetValue<string> ( "AppSettings:Auth:ServerUrl" );
        options . RequireHttpsMetadata = false;
        options . SaveToken = true;
        options . TokenValidationParameters = new TokenValidationParameters ( )
        {
          ValidateIssuerSigningKey = true ,
          IssuerSigningKey = new SymmetricSecurityKey ( restSharp . Auth . Global . JWTKeyBytes ) ,
          ValidateIssuer = false ,
          ValidateAudience = false
        };
      } );
    }

    public void Configure ( IApplicationBuilder app )
    {
      app . UseDeveloperExceptionPage ( );

      app . UseAuthentication ( );
      app . UseRouting ( );
      app . UseAuthorization ( );

      app . UseEndpoints ( endpoints =>
      {
        endpoints . MapControllers ( );
      } );
    }
  }
}
