using System;
using System . Collections . Generic;

using MySql . Data . MySqlClient;

namespace restSharp . Models
{
  public class Commands
  {
    public string ConnectionString { get; set; }

    public Commands ( string connectionString )
    {
      this . ConnectionString = connectionString;
    }

    private MySqlConnection GetConnection ( )
    {
      return new MySqlConnection ( ConnectionString );
    }

    private bool HasDuplicate(string id)
    {
      bool executedSuccessfully = false;

      using ( MySqlConnection conn = GetConnection ( ) )
      {
        conn . Open ( );

        MySqlCommand cmd = new MySqlCommand($"SELECT commands.id FROM commands WHERE commands.id='{id}'", conn);

        executedSuccessfully = cmd . ExecuteReader ( ).HasRows;
      }

      return executedSuccessfully;
    }

    public bool AddCommand(string id, string key, string description)
    {
      bool executedSuccessfully = HasDuplicate ( id );

      if( executedSuccessfully )
      {
        return false;
      }

      using ( MySqlConnection conn = GetConnection ( ) )
      {
        conn . Open ( );

        MySqlCommand cmd = new MySqlCommand($"INSERT INTO commands VALUES ('{id}', '{key}', '{description}')", conn);

        executedSuccessfully = cmd . ExecuteNonQuery ( ) != -1;
      }

      return executedSuccessfully;
    }

    public bool RemoveCommand(string id)
    {
      bool executedSuccessfully = false;

      using ( MySqlConnection conn = GetConnection ( ) )
      {
        conn . Open ( );

        MySqlCommand cmd = new MySqlCommand($"DELETE FROM commands WHERE commands.id='{id}'", conn);

        executedSuccessfully = cmd . ExecuteNonQuery ( ) > 0;
      }

      return executedSuccessfully;
    }

    public List<Tables.Commands> GetAllCommands ( )
    {
      List<Tables.Commands> list = new List<Tables.Commands>();

      using ( MySqlConnection conn = GetConnection ( ) )
      {
        conn . Open ( );

        MySqlCommand cmd = new MySqlCommand("SELECT * FROM commands", conn);

        using ( MySqlDataReader reader = cmd . ExecuteReader ( ) )
        {
          while ( reader . Read ( ) )
          {
            list . Add ( new Tables . Commands ( )
            {
              Id = new Guid(reader . GetString ( "id" )) ,
              Key = reader . GetString ( "key" ) ,
              Description = reader . GetString ( "description" )
            } );
          }
        }
      }

      return list;
    }

    public Tables.Commands GetCommand(string id)
    {
      Tables.Commands command = new Tables.Commands()
      {
        Id = Guid.Empty,
        Key = String.Empty,
        Description = String.Empty
      };

      using ( MySqlConnection conn = GetConnection ( ) )
      {
        conn . Open ( );

        MySqlCommand cmd = new MySqlCommand($"SELECT commands.key, commands.description FROM commands WHERE id='{id}'", conn);

        using ( MySqlDataReader reader = cmd . ExecuteReader ( ) )
        {
          if ( reader . Read ( ) )
          {
            command . Id = new Guid( id );
            command . Key = reader . GetString ( "key" );
            command . Description = reader . GetString ( "description" );
          }
        }
      }

      return command;
    }
  }
}
