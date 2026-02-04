# Employee, Department & Project Management API

A RESTful API built with ASP.NET Core 5 for managing employees, departments, and projects.

## Overview

This API handles:
- **Departments** - company organizational units
- **Employees** - staff members belonging to departments
- **Projects** - initiatives with budgets and auto-generated codes
- **Assignments** - linking employees to projects with specific roles

Employees can work on multiple projects, and each project can have multiple employees.

## Tech Stack

- ASP.NET Core 5
- Entity Framework Core 5
- SQL Server
- Swagger for API docs

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
- SQL Server (LocalDB works fine)

### Setup

1. Clone the repo
```bash
   git clone https://github.com/yourusername/EmployeeDepartmentAndProjectManagement.git
   cd EmployeeDepartmentAndProjectManagement
```

2. Update connection string in `appsettings.json` if needed
```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=CompanyManagementDB;Trusted_Connection=True;"
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

5. Open Swagger at `https://localhost:5001/swagger`

## API Endpoints

### Departments

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/departments` | List all |
| GET | `/api/departments/{id}` | Get one |
| POST | `/api/departments` | Create |
| PUT | `/api/departments/{id}` | Update |
| DELETE | `/api/departments/{id}` | Delete |

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

The `projectCode` is generated automatically from an external API + project ID.

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

## Database
```
Departments (1) ──── (Many) Employees
Employees   (Many) ──── (Many) Projects  [via EmployeeProjects with Role]
```

## Notes

- Deleting a department with employees is blocked.
- Deleting an employee removes their project assignments (cascade).
- Project creation uses a transaction - if the external API fails, nothing gets saved.
- Project codes are: random string from API + project ID (e.g., "A1B2C3D41").
