# TaskManager Pro - .NET Learning Project

   This project is designed for junior developers to learn the basics of building APIs with ASP.NET Core, using authentication (JWT), EF Core with SQLite, and basic service separation.

   ## 🔧 Prerequisites
   - Visual Studio 2022 or later (Community Edition is fine)
   - .NET 8 SDK

   ## 📦 Technologies Used
   - ASP.NET Core Web API
   - Entity Framework Core with SQLite
   - JWT Authentication
   - Swagger

   ## 🧱 Project Structure
   - `AuthService`: Handles user registration, login, and JWT generation
   - `TaskService`: Manages CRUD for tasks, protected by JWT
   - `Shared`: Shared DTOs and potentially models

   ## ⚙️ Setup Instructions
   1. Clone the repository.
   2. Open `TaskManagerPro.sln` in Visual Studio.
   3. Set `AuthService` as startup project, open Package Manager Console:
      - Run: `Update-Database` to apply migrations (to be added)
   4. Press F5 to launch `AuthService`.
   5. Test Register/Login via Swagger.

   ##  Suggested  Plan

   **1:**
   - Explore solution structure
   - Understand `Program.cs` and `appsettings.json`
   - Implement `RegisterRequest.cs`, `LoginRequest.cs`

   **2:**
   - Create `AuthController`
   - Write Register/Login logic using EF + JWT
   - Test with Swagger and Postman

   **3:**
   - Explore `TaskService` structure
   - Create `TaskItem` model and `TaskDbContext`
   - Implement CRUD endpoints in `TaskController`

   **4:**
   - Add JWT validation middleware
   - Secure endpoints, test access control

   **5:**
   - Polish code and handle validation errors
   - Try optional stretch goals (Docker, DTO mapping, role-based auth)

   ## ✅ Learning Goals
   - Understand how to set up and configure an ASP.NET Core API
   - Work with EF Core and SQLite
   - Implement secure user authentication using JWT
   - Build RESTful services and protect them
   - Simulate basic microservice separation with 2 projects
