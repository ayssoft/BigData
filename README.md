# BigData - .NET 9 N-Layer API Template

A production-ready, enterprise-grade .NET 9 Web API template following Clean Architecture principles with MongoDB and Cassandra integration.

## 🚀 Features

- **.NET 9** - Latest .NET framework
- **Clean Architecture** - Separation of concerns with N-Layer design
- **CQRS Pattern** - MediatR for command/query separation
- **MongoDB** - NoSQL database for products
- **Cassandra** - Distributed database for audit logs
- **JWT Authentication** - Secure API endpoints
- **FluentValidation** - Request validation
- **AutoMapper** - Object-to-object mapping
- **Serilog** - Structured logging with Seq integration
- **Swagger/OpenAPI** - Interactive API documentation
- **Health Checks** - MongoDB and Cassandra health monitoring
- **Docker Support** - Docker Compose for all services
- **Global Error Handling** - Centralized exception middleware
- **Request/Response Logging** - Comprehensive request logging

## 📁 Project Structure

```
BigData/
├── src/
│   ├── BigData.Domain/          # Domain entities and interfaces
│   │   ├── Entities/
│   │   ├── Enums/
│   │   └── Interfaces/
│   ├── BigData.Core/            # Core utilities and common code
│   │   ├── Common/
│   │   ├── Constants/
│   │   ├── Exceptions/
│   │   └── Extensions/
│   ├── BigData.Infrastructure/  # Data access and external services
│   │   ├── Data/
│   │   ├── Repositories/
│   │   └── Configuration/
│   ├── BigData.Application/     # Business logic and use cases
│   │   ├── DTOs/
│   │   ├── Features/
│   │   ├── Mappings/
│   │   └── Behaviors/
│   └── BigData.API/             # API presentation layer
│       ├── Controllers/
│       ├── Middleware/
│       └── Services/
└── tests/
    └── BigData.Tests/           # Unit and integration tests
```

## 🛠️ Technologies

### Backend
- .NET 9.0
- ASP.NET Core Web API
- C# 12

### Databases
- MongoDB 3.6.0
- Cassandra 3.22.0

### Libraries
- MediatR 12.4.1
- AutoMapper 16.0.0
- FluentValidation 12.1.1
- Serilog 10.0.0
- Swashbuckle.AspNetCore 10.1.0
- JWT Authentication

## 📋 Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (for MongoDB, Cassandra, and Seq)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/ayssoft/BigData.git
cd BigData
```

### 2. Start Docker Services

```bash
docker-compose up -d
```

This will start:
- **MongoDB** on port 27017
- **Cassandra** on port 9042
- **Seq** (logging) on port 5341

### 3. Restore Dependencies

```bash
dotnet restore
```

### 4. Build the Solution

```bash
dotnet build
```

### 5. Run the API

```bash
cd src/BigData.API
dotnet run
```

The API will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger UI: `http://localhost:5000` (in Development mode)

## 📖 API Endpoints

### Authentication

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/auth/register` | Register new user |
| POST | `/api/auth/login` | Login and get JWT token |

### Products (MongoDB)

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/products` | Get all products (paginated) | Yes |
| GET | `/api/products/{id}` | Get product by ID | Yes |
| POST | `/api/products` | Create new product | Yes |
| PUT | `/api/products/{id}` | Update product | Yes |
| DELETE | `/api/products/{id}` | Delete product | Yes |

### Audit Logs (Cassandra)

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/auditlogs` | Get audit logs | Yes |
| GET | `/api/auditlogs/{id}` | Get audit log by ID | Yes |
| POST | `/api/auditlogs` | Create audit log | Yes |

### Health Checks

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/health` | Check API and database health |

## 🔐 Authentication

### Register a New User

```bash
POST /api/auth/register
Content-Type: application/json

{
  "username": "testuser",
  "email": "test@example.com",
  "password": "Test@123",
  "confirmPassword": "Test@123"
}
```

### Login

```bash
POST /api/auth/login
Content-Type: application/json

{
  "username": "testuser",
  "password": "Test@123"
}
```

Response:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "username": "testuser",
  "userId": "..."
}
```

### Use the Token

Add the JWT token to the Authorization header:

```bash
Authorization: Bearer {your-token-here}
```

## 📝 Configuration

### appsettings.json

```json
{
  "DatabaseSettings": {
    "MongoDB": {
      "ConnectionString": "mongodb://localhost:27017",
      "DatabaseName": "BigDataDb",
      "ProductsCollectionName": "products"
    },
    "Cassandra": {
      "ContactPoints": "localhost",
      "Port": 9042,
      "Keyspace": "bigdata",
      "LocalDatacenter": "datacenter1"
    }
  },
  "JwtSettings": {
    "SecretKey": "YourSecretKeyHere-ChangeThisInProduction-MinimumLength32Characters!",
    "Issuer": "BigDataAPI",
    "Audience": "BigDataClient",
    "ExpirationMinutes": 60
  }
}
```

## 🧪 Testing

Run tests:

```bash
dotnet test
```

## 📊 Monitoring

### Seq Logging

Access Seq dashboard at `http://localhost:5341` to view structured logs.

### Health Checks

Check API health at `http://localhost:5000/health`

## 🏗️ Architecture

### Clean Architecture Layers

1. **Domain Layer** - Core business entities and interfaces
2. **Core Layer** - Common utilities, exceptions, and result patterns
3. **Infrastructure Layer** - Database implementations and external services
4. **Application Layer** - Business logic, CQRS handlers, and validation
5. **API Layer** - HTTP endpoints, middleware, and API configuration

### Design Patterns

- **Repository Pattern** - Data access abstraction
- **Unit of Work Pattern** - Transaction management
- **CQRS** - Command Query Responsibility Segregation
- **Mediator Pattern** - Loose coupling between components
- **Result Pattern** - Standardized response handling
- **Pipeline Behavior** - Cross-cutting concerns (logging, validation, performance)

## 🔧 Development

### Add New Feature

1. Create entity in `Domain/Entities`
2. Add repository interface in `Domain/Interfaces`
3. Implement repository in `Infrastructure/Repositories`
4. Create DTOs in `Application/DTOs`
5. Add commands/queries in `Application/Features`
6. Create validators in `Application/Features`
7. Add controller in `API/Controllers`

### Database Migration

#### MongoDB
MongoDB is schemaless. Collections are created automatically.

#### Cassandra
The keyspace and tables are created automatically on startup. Schema is defined in `CassandraContext.cs`.

## 📦 Docker Deployment

### Build and Run with Docker Compose

```bash
docker-compose up --build
```

### Stop Services

```bash
docker-compose down
```

### Remove Volumes

```bash
docker-compose down -v
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🙏 Acknowledgments

- Clean Architecture by Robert C. Martin
- CQRS Pattern
- Domain-Driven Design principles
- .NET Community

## 📧 Contact

- GitHub: [@ayssoft](https://github.com/ayssoft)
- Email: team@bigdata.com

---

**Built with ❤️ using .NET 9**