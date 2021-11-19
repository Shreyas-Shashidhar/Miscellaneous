# User API Dev Guide
  1. As per the requirements, API endpoints are added for User and Accounts.
  2. Used .Net Core 3.0 environment.
  3. Followed Command Query Responsibility Segregation(CQRS) Pattern while designing the code.
  4. Also followed Domain driven design.
  5. Used SQL Docker image instance for persisting the data.
  6. Data is seeded when the application is launched.
  7. Endpoint for interacting with Users resource: /api/users.
  8. Endpoint for interacting with Users resource: /api/accounts.
  9. Unit tests are written for Command and Queries.
  10. Integration tests are writter for each controllers.
  11. Swagger is used and its respective documentation opens up in the root URL.

## Building
  dotnet build;cd TestProject.WebAPI;dotnet run

## Testing
rm -rf reports && dotnet build && dotnet test --logger xunit --results-directory ./reports/

## Deploying
  1. Need to run 'docker-compose up', to download and start the SQL docker image instance.
  2. Need to update the appsetting.json with appropriate server and database details in connection string.
        "ConnectionStrings": {
            "DefaultConnection": "Server=localhost;Database=TestProjectDb;User Id=sa;Password=YourStrong@Passw0rd;"
        }     
  3. Build the application using above command (dotnet build)
  4. Run 'update-database' command in Package manager console to initialize the database.
  5. Run the application using dotnet run, data is seeded into database at the start if not present.
  6. Swagger endpoint opens up at the launch of application
  7. Integration tests too can be executed at the moment and it uses in memory database.



## Additional Information

Git Code link Url : https://git-rba.hackerrank.com/git/a43738db-3495-4d87-bfb1-2c993065efc9
