# CheckoutGateway
This project implements a payment gateway using .NET 7, PostgreSQL and Dapper.
The main functionalities of this gateway are:
- Register merchants and generate secret_keys to use on authentication
- Validate, process and store credit card payments
- Retrieve payments by merchant or by id

## How to run it
1. Clone project and navigate to project folder
```bash
git clone https://github.com/fsnader/CheckoutGateway.git
cd CheckoutGateway/src/CheckoutGateway/CheckoutGateway
```

2. Navigate to the project folder
```shell
cd 
```

3. Run docker-compose to start the PostgreSQL server and create the database schema

```shell
docker-compose up -d
```

On the first container run, the database schema will be created automatically. If you need to make any changes to the schema or initialize the database in any other way, you can modify the initialize.sql script located in the .postgres directory.

4. To start the application, you will need to start the project through your IDE (Visual Studio, Rider) or run the dotnet command:
```shell
dotnet run
```

5. After that, the running address will be printed in your terminal, and you will be able to access the url to see the Swagger OpenAPI documentation.

![swagger](docs/swagger.png?raw=true)

[//]: # ( TODO: To run the integration tests, make sure the containers are running and then execute the tests using Visual Studio or the command line. Please note that the integration tests will create, update, and delete data from the database, so it's recommended to use a test database or reset the container after running the tests.)

### Authentication

Authentication is required for all endpoints except for the POST /api/auth/signup and POST /api/auth/login endpoints.
Users must include an Authorization header with a JWT token to access protected endpoints.

- `POST /api/auth/signup`: Allows merchants to sign up and create a new account. This endpoints require a merchant name and a client_id, and will generate the secret_id that is required to login.
- `POST /api/auth/login`: Receives a client_id and a secret_id, and returns a token that can be used to access the payments endpoints.

![img.png](docs/token.png?raw=true)

To use the received token on Swagger, it's necessary to copy the received token, click on the "Authorize" button and add the token in the input, in the format "Bearer ${token}":

![img.png](docs/authorize.png?raw=true)
### Endpoints

The application consists on three main API endpoints:
- `POST /api/payments`: Receives a payment request (containing the amount, currency and credit card details), validates it, sends it to the payment gateway and keeps the database updated accordingly.
- `GET /api/payments`: List all payments for a specific merchant.
- `GET /api/payments/{id}`: Retrieves a specific payment by its id. It only returns the payment if its from the merchant that is performing the request and masks sensitive information.

## Architecture

### Solution organization
This project tries to follow some Clean Architecture patterns to keep the domain and the business logic isolated from implementation details, making it technology agnostic and easily testable.

![Clean architecture](docs/clean-architecture.png?raw=true)

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
Validators: FluentValidation input validators

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

![Infrastructure Layer](docs/infrastructure.png?raw=true)

#### WebApi
This is the layer that keeps the implementation details of the presentation layer (API Controllers) of our app, and components that only make sense on this layer (Authentication and Middlewares).

This layer also includes DTOs that are the API contracts. These contracts usually differ from the domain because they are use case specific and hide sensitive domain information from the end user.

![WebApi layer](docs/webapi.png?raw=true)

## Unit Tests
The CheckoutGateway.UnitTests project contains a suite of unit tests for testing the individual components of the API. These tests are designed to test the logic of the code in isolation, without relying on external dependencies. The tests are written using xUnit and can be run from Visual Studio or using the command line.

![unit tests.png](docs/unit-tests.png?raw=true)

## Extras
- List payments endpoint
- Merchant signup and authentication
- Unit tests

## Possible Improvements
- Change bank communication to async with callback webhook
- Include Polly to handle retries
- Include payment history table to keep track of payment updates
- Include timestamp on the payments table