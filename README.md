# recipe-builder
Team WebChefs:
  Courtney Ward
  Alex Rodriguez
  Brody Kerr
  Chase Trower
  Jared Holston
  Hailey Thomas

This project is built using the dotnet 8 SDK for asp.net core.
Instructions for configuring a server for this project are not provided beyond a basic overview here.
It is recommended to execute the recipe-builder.dll using the dotnet CLI command (this comes from the dotnet runtime.) e.g.  'dotnet recipe-builder.dll'
This may be scripted and the script set as a service in whatever init system is available.
The default behavior is to listen at http://localhost:5000

Requirements:
  A host server with a dotnet core 8.x runtime.
  A functioning Neo4j database accessible to the host server.
  A settings.xml file at the application's working directory with the following format where the elements within ServerCredentials are populated and correct:
    <ServerCredentials>
      <URI></URI>
      <dbUser></dbUser>
      <dbPassword></dbPassword>
    </ServerCredentials>

The URI is the socket for the neo4j database, and dbUser and dbPassword are the credentials for a user on the neo4j database with adequate permissions to create, delete, and modify the contents of the database.
