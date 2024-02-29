# Movies Database
## Running via Docker

If you want to run the database via a Docker instance of MS SQL please follow these instructions;
1. Open a Powershell prompt and run this command to pull the MS SQL server Docker image and start a new container;
    ```
    docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=HelloWorld!!1234" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
    ```
2. Connect to the instance with [SSMS](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16), the credentials will be as follows;
    1. server = .
    2. username = sa
    3. password = HelloWorld!!1234
3. Create a database named "Movies.Database"
4. Open the Movies solution, within the Movies.Database project you should find a "PublishProfiles" folder, right click "Movies.Database.publish.xml" and chose publish
5. You might be asked to re-enter credentials - if you are the server, username and password are all as per step 2 above
6. Once the publish is complete, refresh Movies.Database database in SSMS and you should see the following tables;
    1. Genre
    2. Movie
    3. MovieRating
    4. User
7. The tables should all contain data