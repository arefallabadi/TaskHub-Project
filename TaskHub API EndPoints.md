# TaskHub Backend API Endpoints

This document lists all the backend API endpoints for **TaskHub**, including their HTTP method, required authorization, and description.

| Endpoint                 | Method | Auth       | Description |
|--------------------------|--------|------------|-------------|
| `/api/auth/login`        | POST   | No         | User login, returns JWT and Role |
| `/api/users`             | GET    | Admin      | Get all users |
| `/api/users/{id}`        | GET    | Admin      | Get user by ID |
| `/api/users`             | POST   | Admin      | Create new user |
| `/api/users/{id}`        | PUT    | Admin      | Update user details |
| `/api/users/{id}`        | DELETE | Admin      | Delete user |
| `/api/tasks`             | GET    | User/Admin | Get tasks assigned to current user (or all tasks for Admin) |
| `/api/tasks/{id}`        | GET    | User/Admin | Get task details |
| `/api/tasks`             | POST   | Admin      | Create new task |
| `/api/tasks/{id}`        | PUT    | Admin      | Update task details (Admin only) |
| `/api/tasks/{id}`        | DELETE | Admin      | Delete task |
| `/api/tasks/{id}/status` | PATCH  | User/Admin | Update **task status** (User can update assigned task, Admin can update any task) |
| `/api/comments`          | POST   | User/Admin | Add comment to a task |
| `/api/comments/{id}`     | PUT    | User/Admin | Edit comment (only author or Admin) |
| `/api/comments/{id}`     | DELETE | User/Admin | Delete comment (only author or Admin) |

## Notes on Authorization

- **Admin**
  - Full CRUD on Users, Tasks, and Comments
  - Can view Dashboard and all tasks
- **User**
  - Can view and update only tasks assigned to them
  - Can add/edit/delete own comments
  - Cannot create/delete tasks or manage users

