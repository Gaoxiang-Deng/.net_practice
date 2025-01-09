# Student Management API Documentation

## Project Overview
This project is a **Student Management API** built using **ASP.NET Core**. It provides functionalities for managing student records, including CRUD operations, filtering, sorting, and pagination. The API is modularized for better maintainability and scalability.

---

## Features

### 1. Student Information Management
- **Add Student**: Add a new student by providing name, major, and GPA.
- **Get All Students**: Retrieve the list of all students.
- **Get Student by ID**: Retrieve detailed information about a specific student by ID.
- **Update Student**: Update student information (name, major, GPA) by ID.
- **Delete Student**: Delete a student by ID.

### 2. Filtering and Sorting
- **Filter by Major**: Retrieve students belonging to a specific major.
- **Sort by GPA**: Retrieve students sorted by GPA in ascending or descending order.

### 3. Pagination
- **Paginated Results**: Retrieve a specific page of students with a defined page size.

---

## API Endpoints

### **Student Information Management**
| HTTP Method | Endpoint                | Description                      |
|-------------|-------------------------|----------------------------------|
| POST        | `/students`            | Add a new student               |
| GET         | `/students`            | Retrieve all students           |
| GET         | `/students/{id}`       | Retrieve a student by ID        |
| PUT         | `/students/{id}`       | Update student information      |
| DELETE      | `/students/{id}`       | Delete a student by ID          |

### **Filtering and Sorting**
| HTTP Method | Endpoint                | Query Parameters           | Description                      |
|-------------|-------------------------|----------------------------|----------------------------------|
| GET         | `/students/filter`     | `major` (e.g., "Physics") | Filter students by major         |
| GET         | `/students/sort`       | `order` (asc/desc)         | Sort students by GPA             |

### **Pagination**
| HTTP Method | Endpoint                | Query Parameters           | Description                      |
|-------------|-------------------------|----------------------------|----------------------------------|
| GET         | `/students/page`       | `pageNumber`, `pageSize`   | Paginate the student list        |

---

## Technologies Used
- **ASP.NET Core**: Framework for building the API.
- **C#**: Programming language.
- **Swagger/OpenAPI**: For API documentation and testing.
- **Dependency Injection**: Used for injecting the `StudentService`.

---

## Project Structure
```plaintext
MyWebApi/
├── Controllers/
│   └── StudentController.cs
├── Models/
│   └── Student.cs
├── Services/
│   └── StudentService.cs
├── Program.cs
```

### **Key Components**
1. **`Controllers/StudentController.cs`**
   - Defines the API endpoints.
   - Routes map to `StudentService` methods.

2. **`Models/Student.cs`**
   - Defines the `Student` model with `Id`, `Name`, `Major`, and `GPA` properties.

3. **`Services/StudentService.cs`**
   - Contains business logic for managing student data.
   - Handles CRUD operations, filtering, sorting, and pagination.

4. **`Program.cs`**
   - Configures services and middleware.
   - Maps API endpoints.

---

## How to Run the Project

1. **Clone the Repository**
   ```bash
   git clone https://github.com/your-repo/StudentManagementAPI.git
   cd StudentManagementAPI
   ```

2. **Build and Run**
   ```bash
   dotnet run
   ```

3. **Access Swagger UI**
   Open a browser and navigate to:
   ```
   http://localhost:5001/swagger
   ```
   Use the Swagger UI to test all API endpoints.

---

## Example Usage

### Add a New Student
**Request**:
```http
POST /students
Content-Type: application/json
```
**Body**:
```json
{
  "name": "David",
  "major": "Computer Science",
  "gpa": 3.7
}
```
**Response**:
```json
{
  "id": 4,
  "name": "David",
  "major": "Computer Science",
  "gpa": 3.7
}
```

### Get All Students
**Request**:
```http
GET /students
```
**Response**:
```json
[
  { "id": 1, "name": "Alice", "major": "Computer Science", "gpa": 3.8 },
  { "id": 2, "name": "Bob", "major": "Mathematics", "gpa": 3.5 }
]
```

---

## Future Improvements
1. **Database Integration**: Replace in-memory data with a persistent database (e.g., SQL Server).
2. **Authentication**: Add user authentication and authorization.
3. **Validation**: Implement data validation for inputs.

---

## Author
Developed by [Your Name].

