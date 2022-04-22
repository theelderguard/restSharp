using Microsoft . AspNetCore . Authorization;
using Microsoft . AspNetCore . Mvc;

namespace restSharp . Controllers
{
  [Authorize]
  [Route ( "api/[controller]" )]
  [ApiController]
  public class CommandsController : ControllerBase
  {
    private readonly IConfiguration _configuration;

    private Models.Commands commands;

    public CommandsController ( IConfiguration configuration )
    {
      _configuration = configuration;

      commands = new Models.Commands( _configuration . GetConnectionString ( "DefaultConnection" ) );
    }

    [HttpGet ( "add" )]
    public ActionResult<int> Add ( string id, string key, string description )
    {
      return commands.AddCommand ( id, key, description ) ? 1 : 0;
    }

    [HttpGet ( "remove" )]
    public ActionResult<int> Remove ( string id )
    {
      return commands.RemoveCommand ( id ) ? 1 : 0;
    }

    [HttpGet ( "list" )]
    public IEnumerable<Tables.Commands> List ( )
    {
      return commands.GetAllCommands ();
    }

    [HttpGet ( "get" )]
    public ActionResult<Tables . Commands> Get ( string id )
    {
      return commands . GetCommand ( id );
    }
  }
}
