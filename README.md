# Notifications Microservice - Scenario

## How to run
Open the solution in VS2017 and hit run on IISExpress (F5)

All the NuGet packages should be restored and the project will open in a browser

## Notes
- I used `modelBuilder.HasData` to add the initial template into the table, as part of the migration
- To allow the local front end to communicate to this API I allowed CORS to any origin, this would
have to be reconsidered moving on to a more sensitive preproduction and live environments
- the Integration tests project creates a new instance of the Database in memory every time the tests are
started, an improvement could be recreating the database for each test, as it can become hard to synchronise 
once there are a large number of tests changing data in the same Database

