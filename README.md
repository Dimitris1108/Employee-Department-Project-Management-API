# Employee, Department & Project Management API

A RESTful API built with ASP.NET Core 5 for managing employees, departments, and projects.

## Overview

This API handles:
- **Departments** - company organizational units
- **Employees** - staff members belonging to departments with salary info
- **Projects** - initiatives with budgets and auto-generated codes
- **Assignments** - linking employees to projects with specific roles

Employees can work on multiple projects, and each project can have multiple employees.

## Tech Stack

- ASP.NET Core 5
- Entity Framework Core 5
- SQL Server

## Project Structure

```
├── Controllers/        # API endpoints
├── Services/           # Business logic
├── Repositories/       # Database queries
├── Models/             # Database entities
├── DTOs/               # Request/response objects
├── Data/               # DbContext
└── Migrations/         # Database migrations
```

The code follows a layered architecture: Controllers call Services, Services call Repositories, Repositories talk to the database.

## Getting Started

### Prerequisites

- .NET 5 SDK
- SQL Server (LocalDB or SQL Server Express)

### Setup

1. Clone the repo
   ```bash
   git clone https://github.com/Dimitris1108/Employee-Department-Project-Management-API.git
   cd Employee-Department-Project-Management-API
   ```

2. Update connection string in `appsettings.json` if needed
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=CompanyManagementDB;Trusted_Connection=True;"
     }
   }
   ```

3. Run migrations
   ```bash
   dotnet ef database update
   ```

4. Start the app
   ```bash
   dotnet run
   ```

5. Test the API using Postman or any HTTP client at `https://localhost:5000/api`

## API Endpoints

### Departments

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/departments` | List all |
| GET | `/api/departments/{id}` | Get one |
| POST | `/api/departments` | Create |
| PUT | `/api/departments/{id}` | Update |
| DELETE | `/api/departments/{id}` | Delete |
| GET | `/api/departments/{id}/budget` | Get total project budget |

**Create department:**
```json
POST /api/departments

{
    "name": "Engineering",
    "officeLocation": "Building A"
}
```

### Employees

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/employees` | List all |
| GET | `/api/employees/{id}` | Get one |
| POST | `/api/employees` | Create |
| PUT | `/api/employees/{id}` | Update |
| DELETE | `/api/employees/{id}` | Delete |
| GET | `/api/employees/{id}/projects` | Get assigned projects |

**Create employee:**
```json
POST /api/employees

{
    "firstName": "John",
    "lastName": "Doe",
    "email": "john.doe@company.com",
    "salary": 55000.00,
    "departmentId": 1
}
```

### Projects

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/projects` | List all |
| GET | `/api/projects/{id}` | Get one |
| POST | `/api/projects` | Create |
| PUT | `/api/projects/{id}` | Update |
| DELETE | `/api/projects/{id}` | Delete |

**Create project:**
```json
POST /api/projects

{
    "name": "Website Redesign",
    "budget": 75000.00
}
```

The `projectCode` is generated automatically using an external API and appended with the project ID (e.g., "xK9mB2pL1").

### Employee-Project Assignments

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/employeeprojects` | List all |
| GET | `/api/employeeprojects/{empId}/{projId}` | Get one |
| POST | `/api/employeeprojects` | Assign employee |
| PUT | `/api/employeeprojects/{empId}/{projId}` | Update role |
| DELETE | `/api/employeeprojects/{empId}/{projId}` | Remove assignment |

**Assign employee to project:**
```json
POST /api/employeeprojects

{
    "employeeId": 1,
    "projectId": 1,
    "role": "Lead Developer"
}
```

## Database Schema

```
Departments (1) ──── (Many) Employees
Employees   (Many) ──── (Many) Projects  [via EmployeeProjects with Role]
```

### Entity Fields

**Department**
- Id, Name, OfficeLocation

**Employee**
- Id, FirstName, LastName, Email, Salary, DepartmentId

**Project**
- Id, Name, Budget, ProjectCode

**EmployeeProject**
- EmployeeId, ProjectId, Role

## Notes

- Deleting a department with employees is blocked.
- Deleting an employee removes their project assignments (cascade).
- Project creation uses a transaction - if the external API fails, nothing gets saved.
- Project codes are generated from an external random string API + project ID.
- External API URL is configured in `appsettings.json` under `ExternalServices:RandomStringGeneratorUrl`.
