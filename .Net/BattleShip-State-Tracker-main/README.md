#  Battleship State Tracker API
Battleship state tracking API for a single player that must support the following logic:

- Create a board
- Add a battleship to the board
- Take an “attack” at a given position, and report back whether the attack resulted in a hit or a miss.


## Implementation
Here there are two projects one with API and other with Test.

### Battleship.Api
	 Developed in ASP.Net Core 5.0 following Clean archtitecture, Domain Driven, CQRS principles using mediator command pattern.
	 Also Persistence layers is in memory and data context is maintained using Entity Framework Core.

	The implementation is split mainly into the following layers:
	- Domain
	- Application
	- Persistence
	- Controllers 
	- Configuration
	- Middlewares
	- Common

### BattleshipApi.Tests
This project has integration tests for Battleship.Api Controllers

## Deployment
The application has been deployed is available here: 
https://battleshipapi20210611235158.azurewebsites.net/index.html

## References

- Mediatr: https://github.com/jbogard/MediatR
- AutoMapper: https://automapper.org/
- Fluent Validation https://fluentvalidation.net/
- Fluent Assertions: https://fluentassertions.com/introduction
