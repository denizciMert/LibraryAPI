# LibraryAPI Project

## Introduction

Welcome to the LibraryAPI project! This application is designed to provide a robust and scalable solution for managing library-related functionalities through a layered architecture. Built using .NET 8, this project integrates a variety of powerful tools and frameworks including Entity Framework, ASP.NET Core Identity, JWT, Swagger, and MailSlurp.

My goal is to offer a comprehensive API for managing library operations such as user authentication, book management, reservations, penalties, and more. With features like JWT authentication, interactive API documentation via Swagger, and email functionalities using MailSlurp, this project is set up to deliver both security and usability.

## Features

- **User Registration and Authentication:** Secure user management with ASP.NET Core Identity, including registration, login, and JWT-based authentication.

- **Role-Based Actions:** Admin and role-based access control for managing library entities and user permissions.

- **Email Verification:** Email verification for updating user roles and ensuring secure changes to user privileges.

- **CRUD Operations for Library Entities:** Full support for creating, reading, updating, and deleting library-related entities such as books, authors, and publishers.

- **Study Table Reservation Management:** Manage reservations for study tables, including tracking reservation status, time slots, and user interactions.

- **Loan Management:** Manage book loans, including tracking borrowing status, deadlines, and user-related information.

- **Penalty System:** Automated penalty management for overdue returns, with functionality to enforce restrictions on transactions until penalties are settled.

- **Ban System:** Implementation of a ban system where users with outstanding penalties are restricted from performing certain actions until their issues are resolved.

- **Image Upload:** Upload and manage images associated with library items or user profiles, with paths stored and returned for easy access.

- **Interactive API Documentation:** Comprehensive API documentation using Swagger for easy exploration and testing of available endpoints.

- **Email Notifications:** Integration with MailSlurp for sending transactional emails, such as password resets and notifications.

## Technologies

- **ASP.NET Core:** ASP.NET Core is a cross-platform framework for building modern, cloud-based, and internet-connected applications, providing a robust platform for developing scalable and high-performance APIs.

- **Entity Framework:** Entity Framework (EF) is an Object-Relational Mapper (ORM) for .NET that simplifies data access by allowing developers to interact with database entities using .NET objects, enhancing productivity and maintainability.

- **Identity Framework:** ASP.NET Core Identity is a membership system that provides user authentication, authorization, and management features, ensuring secure access to the API and its resources.

- **MsSQL:** Microsoft SQL Server (MsSQL) is a relational database management system offering high performance, security, and data integrity for managing library-related data effectively.

- **JWT (JSON Web Tokens):** JWT is used for secure and scalable authentication, enabling stateless user sessions with token-based security.

- **MailSlurp:** MailSlurp is an email API for sending and receiving emails, used in this project for handling transactional email notifications such as password resets and user notifications.

- **Swagger:** Swagger provides a user-friendly interface for designing, building, and documenting RESTful APIs, allowing for interactive exploration and testing of API endpoints.

## Project Structure

```maths    
    LibraryAPI/
    ├── LibraryAPI.BLL/
    │ ├── Core/
    │ ├── Interfaces/
    │ ├── Mappers/
    │ ├── Services/
    ├── LibraryAPI.DAL/
    │ ├── Data/
    │ │ ├── Interfaces/
    │ ├── Migrations/
    ├── LibraryAPI.Entities/
    │ ├── DTOs/
    │ │ ├── AddressDTO/
    │ │ ├── AuthorDTO/
    │ │ ├── BookDTO/
    │ │ ├── CategoryDTO/
    │ │ ├── CityDTO/
    │ │ ├── CountryDTO/
    │ │ ├── DepartmentDTO/
    │ │ ├── DistrictDTO/
    │ │ ├── EmployeeDTO/
    │ │ ├── LanguageDTO/
    │ │ ├── LoanDTO/
    │ │ ├── LocationDTO/
    │ │ ├── MemberDTO/
    │ │ ├── PenaltyDTO/
    │ │ ├── PenaltyTypeDTO/
    │ │ ├── PublisherDTO/
    │ │ ├── ReservationDTO/
    │ │ ├── ShiftDTO/
    │ │ ├── StudyTableDTO/
    │ │ ├── SubCategoryDTO/
    │ │ ├── TitleDTO/
    │ ├── Enums/
    │ ├── Filters/
    │ ├── Models/
    ├── LibraryAPI.WebAPI/
    │ ├── Properties/
    │ ├── Controllers/
    │ ├── Images/
    ├── Others/
```

- **Controllers:** Contains the API controllers responsible for handling HTTP requests and managing interactions between clients and the application's core functionality. 
- **DTOs:** Data Transfer Objects used for transferring data between the client and server, providing a structure for data exchanged through the API endpoints.
- **Models:** Entity models that represent the database schema and define the structure of data stored in the database.
- **Data:** Includes the database context and configuration for Entity Framework Core, defining the database interactions and schema.
- **Core:** Contains core business logic and application services that drive the application's functionality.
- **Interfaces:** Defines interfaces for services, repositories, and other components, promoting a decoupled design and ease of testing.
- **Mappers:** Provides functionality for mapping between entity models and DTOs, facilitating data transformation and transfer.
- **Services:** Contains business logic and service implementations that interact with the data layer and perform application-specific operations.
- **Images:** Directory for storing uploaded images associated with library items or user profiles.
- **Others:** Placeholder for additional components or resources not covered in the main directories.

## Getting Started

### Prerequisites

- .NET 8 SDK
- MsSQL Server (or any other compatible database)
- Visual Studio or VS Code
- MailSlurp API Key

### Installation

1. Clone the repository:
   
   ```bash
   git clone https://github.com/denizciMert/LibraryAPI.git
   ```

3. Set up the database connection string in appsettings.json:

    ```json
    {
        "Logging": {
            "LogLevel": {
                "Default": "Information",
                "Microsoft.AspNetCore": "Warning"
            }
        },
        "AllowedHosts": "*",
        "ConnectionStrings": {
            "ApplicationDbContext": "YourConnectionString"
        }
    }
    ```

4. Build and run the project:

    ```bash
    dotnet build
    ```
    ```bash
    dotnet run
    ```

## API Endpoints

### Account Management

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td>POST</td>
<td>/api/account/Login</td>
<td>Log in a user</td>
</tr>
<tr>
<td>GET</td>
<td>/api/account/Logout</td>
<td>Log out a user (authorized)</td>
</tr>
<tr>
<td>GET</td>
<td>/api/account/GetProfile</td>
<td>Retrieve the user's profile (authorized)</td>
</tr>
<tr>
<td>GET</td>
<td>/api/account/GetLoans</td>
<td>Retrieve the user's loans (authorized)</td>
</tr>
<tr>
<td>POST</td>
<td>/api/account/ReturnLoans</td>
<td>Return a loan (authorized)</td>
</tr>
<tr>
<td>GET</td>
<td>/api/account/GetPenalties</td>
<td>Retrieve the user's penalties (authorized)</td>
</tr>
<tr>
<td>POST</td>
<td>/api/account/PayPenalties</td>
<td>Pay a penalty (authorized)</td>
</tr>
<tr>
<td>POST</td>
<td>/api/account/RateBook</td>
<td>Rate a book (authorized)</td>
</tr>
<tr>
<td>GET</td>
<td>/api/account/GetReservation</td>
<td>Retrieve the user's reservation (authorized)</td>
</tr>
<tr>
<td>POST</td>
<td>/api/account/EndReservation</td>
<td>End a reservation (authorized)</td>
</tr>
<tr>
<td>POST</td>
<td>/api/account/ForgetPassword</td>
<td>Request a password reset</td>
</tr>
<tr>
<td>POST</td>
<td>/api/account/ResetPassword</td>
<td>Reset the password</td>
</tr>
<tr>
<td>POST</td>
<td>/api/account/RequestEmailChange</td>
<td>Request an email change (authorized)</td>
</tr>
<tr>
<td>POST</td>
<td>/api/account/ChangeEmail</td>
<td>Change the email (authorized)</td>
</tr>
<tr>
<td>GET</td>
<td>/api/account/RequestEmailConfirm</td>
<td>Request email confirmation (authorized)</td>
</tr>
<tr>
<td>POST</td>
<td>/api/account/ConfirmEmail</td>
<td>Confirm the email (authorized)</td>
</tr>
</tbody>
</table>

### Addresses

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td>GET</td>
<td>/api/Addresses/Get</td>
<td>Retrieve all addresses (requires authorization for "Çalışan" or "Yönetici" roles)</td>
</tr>
<tr>
<td>GET</td>
<td>/api/Addresses/Get/{id}</td>
<td>Retrieve an address by its ID (requires authorization for "Kullanıcı", "Çalışan", or "Yönetici" roles)</td>
</tr>
<tr>
<td>PUT</td>
<td>/api/Addresses/Put/{id}</td>
<td>Update an address by its ID (requires authorization for "Kullanıcı", "Çalışan", or "Yönetici" roles)</td>
</tr>
<tr>
<td>POST</td>
<td>/api/Addresses/Post</td>
<td>Create a new address (requires authorization for "Kullanıcı", "Çalışan", or "Yönetici" roles)</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/Addresses/Delete/{id}</td>
<td>Delete an address by its ID (requires authorization for "Kullanıcı", "Çalışan", or "Yönetici" roles)</td>
</tr>
</tbody>
</table>

### Authors

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td>GET</td>
<td>/api/Authors/Get</td>
<td>Retrieve all authors (requires authorization)</td>
</tr>
<tr>
<td>GET</td>
<td>/api/Authors/Get/{id}</td>
<td>Retrieve an author by its ID (requires authorization for "Kullanıcı", "Çalışan", or "Yönetici" roles)</td>
</tr>
<tr>
<td>PUT</td>
<td>/api/Authors/Put/{id}</td>
<td>Update an author by its ID (requires authorization for "Kullanıcı", "Çalışan", or "Yönetici" roles)</td>
</tr>
<tr>
<td>POST</td>
<td>/api/Authors/Post</td>
<td>Create a new author (requires authorization for "Çalışan" or "Yönetici" roles)</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/Authors/Delete/{id}</td>
<td>Delete an author by its ID (requires authorization for "Çalışan" or "Yönetici" roles)</td>
</tr>
</tbody>
</table>

### Bans

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td>POST</td>
<td>/api/Ban/Ban</td>
<td>Ban a user (requires authorization for "Çalışan" or "Yönetici" roles; "Yönetici" users cannot be banned; "Çalışan" users can only be banned by the sudo user)</td>
</tr>
<tr>
<td>POST</td>
<td>/api/Ban/UnBan</td>
<td>Unban a user (requires authorization for "Çalışan" or "Yönetici" roles; "Yönetici" users are not banned; only a sudo user can unban a "Çalışan" user)</td>
</tr>
</tbody>
</table>

### Books

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td>GET</td>
<td>/api/Books/Get</td>
<td>Retrieve all books (authorized users only)</td>
</tr>
<tr>
<td>GET</td>
<td>/api/Books/Get/{id}</td>
<td>Retrieve a single book by ID (authorized users with roles "Kullanıcı", "Çalışan", or "Yönetici")</td>
</tr>
<tr>
<td>POST</td>
<td>/api/Books/Post</td>
<td>Create a new book (authorized users with roles "Çalışan" or "Yönetici"; allows file upload for book cover)</td>
</tr>
<tr>
<td>PUT</td>
<td>/api/Books/Put/{id}</td>
<td>Update a book by ID (authorized users with roles "Kullanıcı", "Çalışan", or "Yönetici"; allows file upload for updating book cover)</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/Books/Delete/{id}</td>
<td>Delete a book by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
</tbody>
</table>

### Categories

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td>GET</td>
<td>/api/Categories/Get</td>
<td>Retrieve all categories (authorized users only)</td>
</tr>
<tr>
<td>GET</td>
<td>/api/Categories/Get/{id}</td>
<td>Retrieve a single category by ID (authorized users with roles "Kullanıcı", "Çalışan", or "Yönetici")</td>
</tr>
<tr>
<td>POST</td>
<td>/api/Categories/Post</td>
<td>Create a new category (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>PUT</td>
<td>/api/Categories/Put/{id}</td>
<td>Update a category by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/Categories/Delete/{id}</td>
<td>Delete a category by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
</tbody>
</table>

### Cities

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td>GET</td>
<td>/api/Cities/Get</td>
<td>Retrieve all cities (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>GET</td>
<td>/api/Cities/Get/{id}</td>
<td>Retrieve a single city by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>POST</td>
<td>/api/Cities/Post</td>
<td>Create a new city (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>PUT</td>
<td>/api/Cities/Put/{id}</td>
<td>Update a city by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/Cities/Delete/{id}</td>
<td>Delete a city by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
</tbody>
</table>

### Countries

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td>GET</td>
<td>/api/Countries/Get</td>
<td>Retrieve all countries (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>GET</td>
<td>/api/Countries/Get/{id}</td>
<td>Retrieve a single country by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>POST</td>
<td>/api/Countries/Post</td>
<td>Create a new country (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>PUT</td>
<td>/api/Countries/Put/{id}</td>
<td>Update a country by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/Countries/Delete/{id}</td>
<td>Delete a country by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
</tbody>
</table>

### Departments

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td>GET</td>
<td>/api/Departments/Get</td>
<td>Retrieve all departments (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>GET</td>
<td>/api/Departments/Get/{id}</td>
<td>Retrieve a single department by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>POST</td>
<td>/api/Departments/Post</td>
<td>Create a new department (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>PUT</td>
<td>/api/Departments/Put/{id}</td>
<td>Update an existing department by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/Departments/Delete/{id}</td>
<td>Delete a department by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
</tbody>
</table>

### Districts

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td>GET</td>
<td>/api/Districts/Get</td>
<td>Retrieve all districts (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>GET</td>
<td>/api/Districts/Get/{id}</td>
<td>Retrieve a single district by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>POST</td>
<td>/api/Districts/Post</td>
<td>Create a new district (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>PUT</td>
<td>/api/Districts/Put/{id}</td>
<td>Update an existing district by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/Districts/Delete/{id}</td>
<td>Delete a district by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
</tbody>
</table>

### Employees

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td>GET</td>
<td>/api/Employees/Get</td>
<td>Retrieve all employees (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>GET</td>
<td>/api/Employees/Get/{id}</td>
<td>Retrieve a single employee by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>POST</td>
<td>/api/Employees/Post</td>
<td>Create a new employee (authorized users with role "Yönetici"). File upload is optional.</td>
</tr>
<tr>
<td>PUT</td>
<td>/api/Employees/Put/{id}</td>
<td>Update an existing employee by ID (authorized users with role "Yönetici"). File upload is optional.</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/Employees/Delete/{id}</td>
<td>Delete an employee by ID (authorized users with role "Yönetici")</td>
</tr>
</tbody>
</table>

### Languages

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td>GET</td>
<td>/api/Languages/Get</td>
<td>Retrieve all languages (authorization required)</td>
</tr>
<tr>
<td>GET</td>
<td>/api/Languages/Get/{id}</td>
<td>Retrieve a single language by ID (authorization required for roles "Kullanıcı", "Çalışan", "Yönetici")</td>
</tr>
<tr>
<td>POST</td>
<td>/api/Languages/Post</td>
<td>Create a new language (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>PUT</td>
<td>/api/Languages/Put/{id}</td>
<td>Update an existing language by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/Languages/Delete/{id}</td>
<td>Delete a language by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
</tbody>
</table>

### Loans

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td>GET</td>
<td>/api/Loans/Get</td>
<td>Retrieve all loans (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>GET</td>
<td>/api/Loans/Get/{id}</td>
<td>Retrieve a single loan by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>POST</td>
<td>/api/Loans/Post</td>
<td>Create a new loan (authorized users with roles "Çalışan" or "Yönetici"; user must not have an existing loan)</td>
</tr>
<tr>
<td>PUT</td>
<td>/api/Loans/Put/{id}</td>
<td>Update an existing loan by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/Loans/Delete/{id}</td>
<td>Delete a loan by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
</tbody>
</table>

### Locations

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td>GET</td>
<td>/api/Locations/Get</td>
<td>Retrieve all locations (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>GET</td>
<td>/api/Locations/Get/{id}</td>
<td>Retrieve a single location by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>POST</td>
<td>/api/Locations/Post</td>
<td>Create a new location (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>PUT</td>
<td>/api/Locations/Put/{id}</td>
<td>Update an existing location by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/Locations/Delete/{id}</td>
<td>Delete a location by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
</tbody>
</table>

### Members

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td>GET</td>
<td>/api/Members/Get</td>
<td>Retrieve all members (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>GET</td>
<td>/api/Members/Get/{id}</td>
<td>Retrieve a single member by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>POST</td>
<td>/api/Members/Post</td>
<td>Create a new member (authorized users with roles "Kullanıcı", "Çalışan", or "Yönetici")</td>
</tr>
<tr>
<td>PUT</td>
<td>/api/Members/Put/{id}</td>
<td>Update a member by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/Members/Delete/{id}</td>
<td>Delete a member by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
</tbody>
</table>

### Penalties

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>

<tr>
<td>GET</td>
<td>/api/Penalties/Get</td>
<td>Retrieve all penalties (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>GET</td>
<td>/api/Penalties/Get/{id}</td>
<td>Retrieve a single penalty by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>POST</td>
<td>/api/Penalties/Post</td>
<td>Create a new penalty (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/Penalties/Delete/{id}</td>
<td>Delete a penalty by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
</tbody>
</table>

### Penaty Types

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>

<tr>
<td>GET</td>
<td>/api/PenaltyTypes/Get</td>
<td>Retrieve all penalty types (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>GET</td>
<td>/api/PenaltyTypes/Get/{id}</td>
<td>Retrieve a single penalty type by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>PUT</td>
<td>/api/PenaltyTypes/Put/{id}</td>
<td>Update a penalty type by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>POST</td>
<td>/api/PenaltyTypes/Post</td>
<td>Create a new penalty type (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/PenaltyTypes/Delete/{id}</td>
<td>Delete a penalty type by ID (authorized users with roles "Çalışan" or "Yönetici")</td>
</tr>
</tbody>
</table>

### Publishers

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td>GET</td>
<td>/api/Publishers/Get</td>
<td>Retrieve all publishers (requires authorization)</td>
</tr>
<tr>
<td>GET</td>
<td>/api/Publishers/Get/{id}</td>
<td>Retrieve a single publisher by ID (requires authorization for roles "Ziyaretçi", "Kullanıcı", "Çalışan", or "Yönetici")</td>
</tr>
<tr>
<td>PUT</td>
<td>/api/Publishers/Put/{id}</td>
<td>Update a publisher by ID (requires authorization for roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>POST</td>
<td>/api/Publishers/Post</td>
<td>Create a new publisher (requires authorization for roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/Publishers/Delete/{id}</td>
<td>Delete a publisher by ID (requires authorization for roles "Çalışan" or "Yönetici")</td>
</tr>
</tbody>
</table>

### Reservations

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td>GET</td>
<td>/api/Reservations/Get</td>
<td>Retrieve all reservations (requires authorization for roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>GET</td>
<td>/api/Reservations/Get/{id}</td>
<td>Retrieve a reservation by ID (requires authorization for roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>POST</td>
<td>/api/Reservations/Post</td>
<td>Create a new reservation (requires authorization for roles "Çalışan" or "Yönetici"). A user can only have one reservation at a time.</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/Reservations/Delete/{id}</td>
<td>Delete a reservation by ID (requires authorization for roles "Çalışan" or "Yönetici")</td>
</tr>
</tbody>
</table>

### Shifts

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td>GET</td>
<td>/api/Shifts/Get</td>
<td>Retrieve all shifts (requires authorization for roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>GET</td>
<td>/api/Shifts/Get/{id}</td>
<td>Retrieve a shift by ID (requires authorization for roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>POST</td>
<td>/api/Shifts/Post</td>
<td>Create a new shift (requires authorization for role "Yönetici")</td>
</tr>
<tr>
<td>PUT</td>
<td>/api/Shifts/Put/{id}</td>
<td>Update a shift by ID (requires authorization for role "Yönetici")</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/Shifts/Delete/{id}</td>
<td>Delete a shift by ID (requires authorization for role "Yönetici")</td>
</tr>
</tbody>
</table>

### Study Tables

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td>GET</td>
<td>/api/StudyTables/Get</td>
<td>Retrieve all study tables (requires authorization for roles "Kullanıcı", "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>GET</td>
<td>/api/StudyTables/Get/{id}</td>
<td>Retrieve a study table by ID (requires authorization for roles "Kullanıcı", "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>POST</td>
<td>/api/StudyTables/Post</td>
<td>Create a new study table (requires authorization for roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>PUT</td>
<td>/api/StudyTables/Put/{id}</td>
<td>Update a study table by ID (requires authorization for roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/StudyTables/Delete/{id}</td>
<td>Delete a study table by ID (requires authorization for roles "Çalışan" or "Yönetici")</td>
</tr>
</tbody>
</table>

### SubCategories

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td>GET</td>
<td>/api/SubCategories/Get</td>
<td>Retrieve all subcategories (requires authorization for roles "Kullanıcı", "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>GET</td>
<td>/api/SubCategories/Get/{id}</td>
<td>Retrieve a subcategory by ID (requires authorization for roles "Kullanıcı", "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>POST</td>
<td>/api/SubCategories/Post</td>
<td>Create a new subcategory (requires authorization for roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>PUT</td>
<td>/api/SubCategories/Put/{id}</td>
<td>Update a subcategory by ID (requires authorization for roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/SubCategories/Delete/{id}</td>
<td>Delete a subcategory by ID (requires authorization for roles "Çalışan" or "Yönetici")</td>
</tr>
</tbody>
</table>

### Titles

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td>GET</td>
<td>/api/Titles/Get</td>
<td>Retrieve all titles (requires authorization for roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>GET</td>
<td>/api/Titles/Get/{id}</td>
<td>Retrieve a title by ID (requires authorization for roles "Çalışan" or "Yönetici")</td>
</tr>
<tr>
<td>POST</td>
<td>/api/Titles/Post</td>
<td>Create a new title (requires authorization for role "Yönetici")</td>
</tr>
<tr>
<td>PUT</td>
<td>/api/Titles/Put/{id}</td>
<td>Update a title by ID (requires authorization for role "Yönetici")</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/Titles/Delete/{id}</td>
<td>Delete a title by ID (requires authorization for role "Yönetici")</td>
</tr>
</tbody>
</table>

### Uploads

<table>
<thead>
<tr>
<th>Method</th>
<th>Endpoint</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td>POST</td>
<td>/api/Upload/Upload</td>
<td>Upload an image file (requires authorization for role "Yönetici")</td>
</tr>
</tbody>
</table>

## Acknowledgements

- This project was developed as part of the backend program at <a href="https://softito.com.tr/index.php" rel="nofollow">Softito Yazılım - Bilişim Akademisi</a>.
- Special thanks to the instructors and peers who provided valuable feedback and support throughout the development process.
