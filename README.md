# ASP.NET Web API
This web API application is built on .NET Core 6 and provides functionality related to Employee management, Leaves, and Training.

## Endpoints
### Employee
* GET - `/api/Employee` :
This endpoint retrieves a list of all Employees.

* GET - `/api/Employee/getEmpDetails` :
This endpoint retrieves the details of a particular employee, based on the employee id.

* GET - `/api/Employee/managerCheck` :
This endpoint checks whether the logged-in user is a manager or not.

### Leaves
* GET - `/api/Leaves` :
This endpoint retrieves a list of all Leaves.

* POST - `/api/Leaves/addleaves` :
This endpoint adds a new Leave to the system.

### Training
* GET - `/api/Training` :
This endpoint retrieves a list of all Training.

* GET - `/api/Training/getTraining` :
This endpoint retrieves the details of a particular Training, based on the training id.

* POST - `/api/Training/add` :
This endpoint adds a new Training to the system.

## How to run the application
To run this application, follow these steps:

1. Clone the repository to your local machine.
2. Open the solution in Visual Studio.
3. Build the solution.
4. Update the appsettings.json file with your database connection string.
5. Run the application.
6. The application will start running on the specified port.
## Dependencies
This application has the following dependencies:

* .NET Core 6
* MySql.Data 8.0.27
* Newtonsoft.Json 13.0.1
## Database
This application uses a MySQL database for storing data. The database schema and tables must be created manually before running the application. The database connection string can be configured in the `appsettings.json` file.

## Custom Data Context
This application uses a custom data context class named `MySqlManager` to handle database operations. The class uses the `MySql.Data` package to connect to a MySQL database and execute queries. The class includes methods for executing queries and stored procedures and retrieving data as `DataTable` objects. The class can be modified or replaced as needed to connect to a different type of database.

## Conclusion
This is a basic ASP.NET Web API application built on .NET Core 6. It provides functionality related to Employee management, Leaves, and Training. The application is designed to be easily extendable and can be customized to meet the needs of specific use cases.
