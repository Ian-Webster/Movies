

# Movies

## Introduction
This project was originally a coding challenge I completed a few years ago (you can see the original requirements for the challenge in the [Requirements](#requirements) section) the scope has been expanded to provide a demonstration of backend and frontend technologies.

## Setup
The solution is split into three main parts;
1. A web API and associated supporting libraries
2. A MS SQL database project
3. A collection of UX projects

### Web API
The original brief called for a web API and the bulk of the projects came from my initial implementation, the project structures are as follows;

| Name                  | Notes                                                                                                                                                                  |
|-----------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Movies.API            | The main API for the project                                                                                                                                           |
| Movies.Business       | Business logic for the solution                                                                                                                                        |
| Movies.Database       | A MS SQL Visual Studio project for publishing the database used by the solution, there is a readme file in the root of the project detailing how to setup the database |
| Movies.Domain         | Contains models for the solution                                                                                                                                       |
| Movies.Infrastructure | IoC setup project for the solution                                                                                                                                     |
| Movies.Repository     | Data access for the solution                                                                                                                                           |

All the projects (with the exception of Movies.Database) are .net 8, all projects run in Visual Studio 2022.

For Movies.API you'll need to set some [user secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=windows#use-visual-studio) for the database connection string, your user secrets should look like;
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=Movies.Database;User Id=sa;Password=HelloWorld!!1234;MultipleActiveResultSets=true;TrustServerCertificate=Yes;"
  }
}
```
**note** exposing usernames and passwords is poor practice, I'm ignoring this to make setup and running of this project as simple as possible, also note you'd usually be able to use windows authentication in your SQL connection string but my setup uses a docker instances of SQL server so a username and password are required.

Finally there are a number of unit test projects.

### UX projects
In the \ux folder you'll find the various frontend implementations. 

The implementations follow the naming convention of Movies.Ux.TechnologyName.

Each implementation has it's own readme detailing and setup / instructions you might require.

## <a name="requirements"></a> Requirements

**Search Movies:**
 - End point = /api/SearchMovies
 - Searches movies by given criteria.
 - Input Parameters
	 - Title (string) = movie title (can be partial)
	 - YearOfRelease (short) = year of movie release
	 - Genres (Genres enum) = list of Genres (currently Action, Comedy, Horror, ScienceFiction), can set multiple
	 - At least one parameter must be set
 - Returns
	 - Movie list as Json
	 - 200 if movies found
	 - 404 if no movies found for the given criteria
	 - 400 if invalid or no parameters provided

**Top Movies**
 - End point = /api/TopMovies{userId}
 - Lists top 5 movies
 - Input Parameters
	 - userId (int) - id of the user to get movie list for
	 - parameter is optional, if not provided will return a list of top movies across all users
 - Returns
 	 - Movie list as Json
	 - 200 if movies found
	 - 404 if no movies found for the given criteria
	 - 400 if invalid or no parameters provided

**Save Movie Rating**
 - End point = /api/SaveMovieRating
 - Saves a movie rating
 - If rating already exists for the user, the new rating value will overwrite the old one
 - Input Parameters
	 - MovieRating Json object
		 - userId (int) - id of the user the rating is for - default scripts will generate users id 1 - 10
		 - movieId (int) - id of the movie the rating is for - default scripts will generate movies id 1 - 40
		 - Rating (byte) - rating value for the movie
 - Returns
	 - 200 if rating saved successfully
	 - 404 if the movie or the user are not found
	 - 400 if the rating Json object is invalid
