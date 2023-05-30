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
Includes the BankGateway implementation. This mock class emulates the connection between the gateway and the Bank and return random results.
For a real project, it probably would implement a service integration through queues, REST APIs, GRPc, or any other communication protocol.

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

## Cloud Deployment
To do a real cloud deployment, we could use the following services:
- Database: Use Amazon RDS to deploy a managed PostgreSQL instance
- Application: Use Amazon EKS to deploy a managed Kubernetes cluster and deploy the application on it in a scalable way

## Possible Improvements/Refactors
There are some other aspects that should be considered for a real implementation of a payment gateway. 

### Include Polly to handle gateway retries
In a HTTP integration with the bank, we could include Polly to implement some retry and circuit breaker strategies. This would ensure that temporary network or service problems would affect customers less.

### Include payment history table to keep track of payment updates
It would be interesting to keep some sort of event tracking table containing all the payment status updates.
This would give more visibility of aspects like when the payment had been created, when it got accepted, when it got finished, etc...

### Bank Gateway Request / Response flow
The current implementation assumes that the CreatePayment request will return the final result of the payment synchronously, like the following sequence diagram:
![actual-flow](docs/actual-gateway-flow.png?raw=true)

Instead of following this synchronous flow, the gateway processing could be done asynchronous:
1. Our gateway sends a request to the bank with the payment information
2. The bank does a quick fail fast validation to ensure the input is valid. If is, the bank returns a 202 Accepted result confirming that the payment will be processed.
3. When the bank updates the transaction (accepting, rejecting, etc) it notifies our gateway in a webhook
4. This webhook than updates our database accordingly and notify subscribed customers

![async pt1](docs/async-flow-pt1.png?raw=true)
![async pt2](docs/async-flow-pt2.png?raw=true)

This option has some advantages:
- The customer doesn't have to wait the whole payment processing on the same HTTP request, what improves the user experience
- In case of any failures on the gateway or bank processing, we can retry a few times before rejecting the transaction, what improves the reliability of our system

But it also brings some disadvantages:
- The architecture is much more complex and has a lot of moving parts
- The development, monitoring and maintenance efforts are higher
- Handling real time notifications can bring other technical and UX challenges

Because of that, it would be important to evaluate the trade-offs before following this architecture.
