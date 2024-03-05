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
The project has a menu which will take you to most routes, however here is a full list of routes in the project;
* public routes
    * https://localhost:7069/Movie - all movies
    * https://localhost:7069/Movie/Details/some-guid - details for a specific movie
    * https://localhost:7069/MovieRating/TopX/X - top X movies across all users
    * https://localhost:7069/MovieRating/TopX/X/some-guid - top X movies for a specific user
* admin routes
    * https://localhost:7069/Admin/MovieAdmin - all movies
    * https://localhost:7069/Admin/MovieAdmin/Create - create movie
    * https://localhost:7069/Admin/MovieAdmin/Edit/some-guid - edit movie
    * https://localhost:7069/Admin/MovieAdmin/Details/some-guid - movie details
    * https://localhost:7069/Admin/User - users list
    * https://localhost:7069/Admin/User/Create - create user
    * https://localhost:7069/Admin/MovieAdmin/Edit/some-guid - edit user
    * https://localhost:7069/Admin/MovieAdmin/Details/some-guid - user details