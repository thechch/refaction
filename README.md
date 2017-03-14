# Xero.RefactorMe
First of all, the project has got a convention-base name and was rewritten to Dotnet Core during the fact that I don't have Windows machine at home.

By the same reason it was written in Visual Studio Code using Dotnet Core 1.1.0 and Postgres was using as a database. 

SLN file is created for Visual studio users to open all the projects together.

## Getting started
### Run project in Docker containers:
1. Install Docker if you don't have one.
2. After cloning the repo, cd to /src/Xero.RefactorMe.Web
3. If macOS/Linux machine run chmod 755 run_webapi.sh and set the line ending to LF (If it's not);
   If Windows machine set the line ending to LF;
4. Run the command docker-compose up;
5. Application is available in 0.0.0.0:5000 or for Windows users (sometimes) localhost:5000

Commands list from within a project (If dotnet core is installed): 
* **dotnet test** - run tests
* **dotnet build** - run build
* **dotnet run** - run the project

## What has been done
* Decomposed to three layers. Repository layer interacts as a Service layer because in current scenario Service Layer woudl be just a wrapper around repository which is basically just a redundant overhead. If in the future we will need services we could implement Service Layer easily and replace all the dependancies.
* Integration and unit tests implemented. Implementation not completed, just to show the way I would do that. 
* App follows Dot Net Standards structure for multiple projects.
* Data and Model are PCL and cross-platform.
* Two controllers instead one to follow Single Responsibility principle. 
* Dockerization.

## There should be these endpoints:
1. `GET /products` - gets all products.
2. `GET /products/{name}` - finds all products matching the specified name.
3. `GET /products/{id}` - gets the project that matches the specified ID - ID is a GUID.
4. `POST /products` - creates a new product.
5. `PUT /products/{id}` - updates a product.
6. `DELETE /products/{id}` - deletes a product and its options.
7. `GET /products/{id}/options` - finds all options for a specified product.
8. `GET /products/{id}/options/{optionId}` - finds the specified product option for the specified product.
9. `POST /products/{id}/options` - adds a new product option to the specified product.
10. `PUT /products/{id}/options/{optionId}` - updates the specified product option.
11. `DELETE /products/{id}/options/{optionId}` - deletes the specified product option.
