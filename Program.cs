using Microsoft . AspNetCore . Authentication . JwtBearer;
using Microsoft . IdentityModel . Tokens;

var builder = WebApplication.CreateBuilder(args);

builder . Services . AddControllers ( );
builder . Services . AddSingleton ( new restSharp . Auth . JWTAuth ( restSharp . Auth . Global . JWTKey ) );
builder . Services . AddAuthentication ( options => {
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

var app = builder.Build();

app . UseDeveloperExceptionPage ( );

app . UseAuthentication ( );
app . UseRouting ( );
app . UseAuthorization ( );

app . UseEndpoints ( endpoints =>
{
  endpoints . MapControllers ( );
} );

app . Run ( );
