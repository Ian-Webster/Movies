# Movies.Ux.Mvc

## Introduction
This project is an MVC (with razor views) implementation of the movies UI.

The project uses .net 8 and runs in Visual Studio 2022.

The project requires an MS SQL database, setup instructions for this can be found in the Movies.Database project

## Setup
Make sure Movies.Ux.Mvc is the startup project.

You'll need to set some [user secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=windows#use-visual-studio) for the database connection string, your user secrets should look like;

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=Movies.Database;User Id=sa;Password=HelloWorld!!1234;MultipleActiveResultSets=true;TrustServerCertificate=Yes;"
  }
}
```

## Routes
The project has a menu which will take you to most routes but there is a full list of routes in the project;
