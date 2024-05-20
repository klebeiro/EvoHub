# EvoHub - GitHub API Integration Project

This project is a web application built with .NET for the backend and Angular for the frontend. 
It integrates with the GitHub API to fetch and display information about GitHub repositories, users, and other resources.

It was very fun to build and I could put a lot of concepts in practice. Hope you like it! 😊

## Requirements

- "My repository" screen with my github account repositories information ✅
- "Repository details" screen ✅
- "Other repository" screen where the user can search for other repositories ✅
- (BONUS) Favorite repositories feature ✅
- Implement business rules in the Application layer ✅
- Implement the api consuming in Infrastructure layer ✅
- (BONUS) Implement the favorite feature according to the last items and insert
  the button to do so in the repository details screen ✅
- Be written in ASP.NET MVC or WebApi(SPA), C# +4.0 ✅
- Visual Studio +2017 Solution ✅
- Ready to Run by pressing F5 ✅

⭐ I've improved the project setup by updating the .NET projects, improving the architecture and implementing more fuctionalities
such as generic API consuming, global exception handling, remove favorite feature, etc. I also reworked the UI, made it responsive and implemented all the front-end 
components by myself using Html, Css and Ts. I've considered to use Angular Material in the details modal but in the end I've prefered to implement it by myself to show
my Front-End skills.
⭐ I've decided to build the front-end with Angular because in the Technical Interview was told that in the ABC Evo was the mainly used front-end
technology. 

## Features
- Fetch and display details of GitHub repositories.
- Search for GitHub repositories by repository name
- List repositories of a specific GitHub user.
- Retrieve information of a specific repository by username and repository id
- Data persistence of repository information on a SQLite DB to keep favorite repositories accessible
- Responsive design for an optimal viewing experience across devices.

## Prerequisites
.NET SDK (latest version recommended)
Node.js and npm
Angular CLI

## Getting Started
1. Clone the repository
> git clone https://github.com/your-username/github-api-integration.git
> cd github-api-integration
2. Open the solutions file on Visual Studio and select the EvoHub.SPA project as the Startup Project
3. Press the run button and wait a few seconds to the application start

## Project Structure
### Back-End
- Domain: contains the classes and interfaces that defines the models and functionalities of the application
- Infrastructure: integrates the application with the external services, in this case the database and the github api.
  Also there is a CrossCutting class library that is accessible to all layers that requires it and defines the constants
  of the project to centralize the strings and minimize hard coded content.
- Tests: contains the back-end unit tests of the application
- Application: contains the functionalities implementation according to business rules
- Presentation: contains the Single Page Application project that has controllers that consumes the application services and expose an interface
  between the front-end and the back-end services and the web client that consumes the controller endpoints.
### Front-End
- Components: reusable portions of the page
- Models: classes that are used to load the API data
- Services: provide methods to consume the API endpoints
- Pages: component composition mapped by routes and effectively used by the client

## API endpoints
- GET / POST / DELETE: /favorites
- GET: githubapi/repositories
- GET: githubapi/owner-repository-by-id?owner={ownerLogin}&id={repositoryId}
- GET: githubapi/repositories-by-name?name={repositoryName}

 
## Improvements
I'm currently working to improve my unit test writing skills and learning more about software development principles, Angular and .NET.
