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

In a bigger project, the application layers would be kept in different projects (and .dlls), but as this system has only a few use cases, I intentionally decided to organize it using folders:
#### Domain Layer

This is the core domain layer, where entities and abstraction interfaces are defined. This layer doesn't have any dependency on specific technology, and contain the most stable components.

![Domain Layer](docs/domain.png?raw=true)

#### Application Layer
This is the layer that keeps application use cases. Each use case represents a business case and contain a single responsibility.
The only dependency of this layer is the domain layer.

- Merchants:
  - CreateMerchant: Creates a merchant, generating a client_id and a secret_id to be used for login
- Payments:
  - CreatePayment: Creates a payment on the database, sends it to the bank gateway and processes the response.
  - GetPaymentById: Gets a payment for a provided Id and MerchantId
  - GetPaymentsList: Lists all the payments for a specific merchant
OutputPots: Abstractions that represent the result of a use case. The main class here is the UseCaseResult, that is a generic envelope that encapsulates the response of a UseCase to simplify success and error.

![Application Layer](docs/application.png?raw=true)

#### Infrastructure
This is the layer that keeps the implementation details of the domain abstractions. In this application, mostly of this is related to data access (repositories) and external integrations (gateways).

##### Gateways
Includes the BankGateway implementation. This class emulates the connection between the gateway and the Bank. For this project, we're using just a mock class that returns dummy results, but for a real project, it probably would implement a service integration through queues, REST APIs, GRPc, or any other communication protocol.

##### Database
Includes abstractions and classes related to Postgres data access. 

For this project, Dapper is being used to access the database. This MicroORM increases the initial effort of doing CRUD operations, but give much more control and safety for the developer, specially for more complex queries.

##### Repositories
Includes the repositories implementations to access Merchant and Payment entities.

![img.png](docs/infrastructure.png?raw=true)

#### WebApi
This is the layer that keeps the implementation details of the presentation layer (API Controllers) of our app, and components that only make sense on this layer (Authentication and Middlewares).

This layer also includes DTOs that are the API contracts. These contracts usually differ from the domain because they are use case specific and hide sensitive domain information from the end user.

![img_1.png](docs/webapi.png?raw=true)

## Unit Tests
TODO

## Possible Improvements
