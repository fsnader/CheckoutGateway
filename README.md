# CheckoutGateway
This project implements a payment gateway using .NET 7, PostgreSQL and Dapper.
The main functionalities of this gateway are:
- Register merchants and generate secret_keys to use on authentication
- Validate, process and store credit card payments
- Retrieve payments by merchant or by id

## How to run it

To run the API and its associated tests, you will need to have Docker installed on your machine. Once you have Docker installed, you can run the following command to start the PostgreSQL server and create the required tables

```shell
cd src/CheckoutGateway/CheckoutGateway
docker-compose up -d
```

On the first container run, the database schema will be created automatically. If you need to make any changes to the schema or initialize the database in any other way, you can modify the initialize.sql script located in the .postgres directory.

To start the application, you will need to start the project through your IDE (Visual Studio, Rider) or run the dotnet command:
```shell
dotnet run
```

After that, the running address will be printed in your terminal, and you will be able to access the url to see the Swagger OpenAPI documentation.

![swagger](docs/swagger.png?raw=true)

[//]: # ( TODO: To run the integration tests, make sure the containers are running and then execute the tests using Visual Studio or the command line. Please note that the integration tests will create, update, and delete data from the database, so it's recommended to use a test database or reset the container after running the tests.)

### Authentication

Authentication is required for all endpoints except for the POST /api/auth/signup and POST /api/auth/login endpoints.
Users must include an Authorization header with a JWT token to access protected endpoints.

- `POST /api/auth/signup`: Allows merchants to sign up and create a new account. This endpoints require a merchant name and a client_id, and will generate the secret_id that is required to login.
- `POST /api/auth/login`: Receives a client_id and a secret_id, and returns a token that can be used to access the payments endpoints.

## Architecture

### Solution organization
This project tries to follow some Clean Architecture patterns to keep the domain and the business logic isolated from implementation details, making it technology agnostic and easily testable.

The solution is divided in the following layers


### Data Access

### BankGateway

## Unit Tests

## Possible Improvements
