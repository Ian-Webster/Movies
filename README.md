
# Movies

API for movie ratings.

**Set up**
Written in Visual Studio 2017, using asp.net core 2.0.

Solution is set up to use a local MS SQL instance, a database name of "Movies" is assumed, windows authentication is assumed. If either of these need to be changed you will need to edit the "DefaultConnectionString" in the app.settings.json file in the Movies.API project.

**Swagger**
I've used swagger (https://swagger.io) to provide a front end for testing the APIs. The default root for the project is swagger/index.html.

I've enabled documentation for swagger so hopefully the swagger UI should provide enough details to use the APIs.

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

**Future Improvements**
 - Front end project (probably using Angular)
 - Unit tests for the API project
 - Unit tests for the Repository project
 - Implement Async through out the solution
 - Additional APIs
	 - Movie add / edit
	 - User add / edit
