# helloworld
Hello World with Api implementation

The hello world project is developed using .Net Core framework. 
The project is structured using a micro-service pattern and is comprised of the following projects.

	API - hosts application logic and persistent store. 
	Cli - Command line interface representing the user interface.
	Interface - Application interface for implementation abstractiokn
	Entity - entity definitions used by the application
	Store - persistent storage mechanism

The Cli connects to the Api using the ApiStore defined in the connection string section of the Cli app.config file.
The Api saves and retrieves the data from a persistent storage using the DocumentStore which is defined
in the connection string section of the Api app.config file. The DocumentStore implementation mocks NoSql storage 
similar to MongoDb and Postgress. The ApiStore implementation mocks cloud storage solutions i.e. Firebase, 
Elasticsearch or any restful web api implementation.

To run the application, the Api should be running before the Cli is run. This can be done inside Visual Studio 
starting the Api project without Debugging or through command line using dotnet run command. Take note that for 
the dotnet command, the port is automatically incremented from the defined port in the project settings. Once
the application is running, the api can be tested manually through the swagger interface. When running through
Visual studio, the uri will be http://localhost:54494/swagger/swagger/index.html, when run through dotnet command,
it will be http://localhost:54495/swagger/index.html. The discrepancy is due to an existing bug in swagger 
(https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/559).

Once the Api is up and running, if ran through the dotnet command, the app.config for the Cli needs to be updated 
to match the new port. The Cli can then be run through Visual Studio or dotnet run command which should display
the Hello World! message.

Assumptions:
- The code sample is created in such a way that the architecture and interaction between the components are 
highlighted. The performance aspect, specifically for the DocumentStore implementation, is created for 
demonstration purposes only. 
- Similarly, the test cases in the ApiTest project is created to give structure rather than cover full functional
testing.