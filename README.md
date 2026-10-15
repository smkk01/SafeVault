# SafeVault

SafeVault is a .NET 8 ASP.NET Core Web API sample for user registration and login. It uses MySQL for user records, BCrypt for password hashing, Swagger for API exploration, and includes a small registration page.

## Requirements

- .NET 8 SDK
- MySQL Server

## Getting started

1. Create a MySQL database and a `Users` table. The following schema supports the fields used by the API:

   ```sql
   CREATE DATABASE SafeVault;
   USE SafeVault;

   CREATE TABLE Users (
       UserID INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
       Username VARCHAR(255) NOT NULL UNIQUE,
       Email VARCHAR(255) NOT NULL,
       PasswordHash VARCHAR(255) NULL,
       Role VARCHAR(50) NOT NULL DEFAULT 'User'
   );
   ```

2. Configure the MySQL connection string. Do not put real credentials in source control. For local development, set it with .NET user secrets from the repository root:

   ```powershell
   dotnet user-secrets init --project SafeVault.Api
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<your MySQL connection string>" --project SafeVault.Api
   ```

   Alternatively, set the `ConnectionStrings__DefaultConnection` environment variable to a MySQL connection string.

3. Run the API:

   ```powershell
   dotnet run --project SafeVault.Api
   ```

   With the supplied HTTP launch profile, the API is available at `http://localhost:5156`. Open `http://localhost:5156/swagger` to explore the endpoints or `http://localhost:5156/index.html` for the registration page. Endpoints that read or write users require the MySQL database to be available.

## API endpoints

| Method | Route | Description |
| --- | --- | --- |
| `POST` | `/api/auth/register` | Register a user. `Role` is optional and defaults to `User`. |
| `POST` | `/api/auth/login` | Check a username and password. Returns `401 Unauthorized` for an unknown user or invalid password. |
| `POST` | `/api/users` | Create a user with a username and email. |
| `POST` | `/api/users/CreateUser` | Alias for `POST /api/users`. |
| `GET` | `/api/users/{username}` | Retrieve a user by username. |
| `GET` | `/api/admin?role=Admin` | Return the sample admin dashboard response when the supplied role is `Admin`. |

### Request examples

Register:

```json
{
  "username": "alice",
  "email": "alice@example.com",
  "password": "choose-a-password",
  "role": "User"
}
```

Login:

```json
{
  "username": "alice",
  "password": "choose-a-password"
}
```

Create a user:

```json
{
  "username": "bob",
  "email": "bob@example.com"
}
```

## Run tests

From the repository root:

```powershell
dotnet test SafeVault.sln
```

The NUnit test project covers password hashing and verification, input validation, and related security-focused examples.

## Project structure

- `SafeVault.Api/` — ASP.NET Core API, controllers, services, models, static registration page, and configuration.
- `SafeVault.Tests/` — NUnit tests for the API's authentication and input-validation helpers.
- `SafeVault.sln` — solution containing the API and test projects.

## Security note

This is a learning/sample project, not a production-ready authentication system. It does not issue access tokens or establish authenticated sessions. The admin endpoint trusts a caller-supplied `role` query parameter, and registration accepts a role from the request body; neither mechanism enforces authorization. Add proper authentication, server-side authorization, and role assignment controls before exposing the API to untrusted users.
