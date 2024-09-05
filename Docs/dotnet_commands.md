#8/29/2024
#Jared Holston
#
#This document is a list of commands for various operations using the dotnet CLI.
#Please refer to this as needed
#


#initializing asp.net core mvc project with gitignore template.
#Follows the format specified here: https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new
#Requires the .Net Core SDK be installed and on the Path.
dotnet new mvc -lang "C#"
dotnet new gitignore

#Publishing the application to a binary 
#This is used when we want to upload code to the server.
dotnet publish -c Release -o publish


#Running server code.
#Go to the publish directory in the app folder, and run the following statement.
#It runs the application via its dll on the default port 5000
dotnet recipe-builder.dll
