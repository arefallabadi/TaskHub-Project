# TaskHub UML Diagram

## 1️⃣ Class Diagram (Entities)
+--------------------------------+
|      User                      |
+--------------------------------+
| - Id: int                      |
| - Username: string             |
| - PasswordHash: string         |
| - Role: string                 |
+--------------------------------+
| +AssignedTasks: List<TaskItem> |
+--------------------------------+

         1
User ----------* TaskItem
         assignedTo

+--------------------------+
|    TaskItem              |
+--------------------------+
| - Id: int                |
| - Title: string          |
| - Description: string    |
| - Status: string         |
| - CreatedAt: DateTime    |
| - DueDate: DateTime?     |
+--------------------------+
| +Comments: List<Comment> |
+--------------------------+

         1
TaskItem ---------* Comment
         task

+-----------------------+
|     Comment           |
+-----------------------+
| - Id: int             |
| - TaskId: int         |
| - UserId: int         |
| - Content: string     |
| - CreatedAt: DateTime |
+-----------------------+

## 2️⃣ Component Diagram (Architecture)
+--------------------+       +----------------------+
|  Angular Frontend  | <---> |  ASP.NET Core API    |
|  - Components      |       |  - Controllers       |
|  - Services        |       |  - Services          |
|  - Reactive Forms  |       |  - Repositories      |
+--------------------+       |  - DbContext         |
                             +----------------------+
                                      |
                                      v
                             +----------------------+
                             |  SQL Server          |
                             |  - Users Table       |
                             |  - Tasks Table       |
                             |  - Comments Table    |
                             +----------------------+

## 3️⃣ Controller Overview

+-------------------+
|   AuthController  |
+-------------------+
| +Login()          |
+-------------------+

+-------------------+
|  UsersController  |
+-------------------+
| +GetUsers()       |
| +GetUser(id)      |
| +CreateUser()     |
| +UpdateUser(id)   |
| +DeleteUser(id)   |
+-------------------+

+--------------------+
|  TasksController   |
+--------------------+
| +GetTasks()        |
| +GetTask(id)       |
| +CreateTask()      |
| +UpdateTask(id)    |
| +DeleteTask(id)    |
| +UpdateTaskStatus()|
+--------------------+

+-------------------+
| CommentsController|
+-------------------+
| +AddComment()     |
| +UpdateComment()  |
| +DeleteComment()  |
+-------------------+

### ✅ Key Points

1. **User ↔ TaskItem**: 1 to N 
2. **TaskItem ↔ Comment**: 1 to N 
3. **Role-based Access**: Admin / User

