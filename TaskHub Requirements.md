# Software Requirements Specification (SRS)

## TaskHub -- Internal Task Management System

### Anazeem Company

---

## 1. Introduction

### 1.1 Purpose

This document defines the functional and non-functional requirements for the **TaskHub** system, an internal task management module developed for **Anazeem Company**.

The system enables employees to create, assign, track, and manage tasks efficiently, with an admin panel for user management and role-based access control.

### 1.2 Scope

TaskHub allows employees to manage tasks through a web-based application using Angular and ASP.NET Core. Features include:

- Task creation, assignment, and tracking  
- Admin panel for managing users and roles  
- Role-based access control (Admin/User)  
- Task comments and status updates  
- JWT-based authentication

### 1.3 Intended Audience

- Technical evaluators  
- Software developers  
- Project stakeholders at Anazeem Company  

---

## 2. System Overview

The system consists of:

- **Backend**: RESTful API built with ASP.NET Core, using Controller-Service-Repository pattern, Entity Framework Core, and JWT authentication  
- **Frontend**: Angular web application with Reactive Forms and modular architecture  
- **Database**: SQL Server for persistence of users, tasks, comments, and roles

---

## 3. Functional Requirements

### 3.1 Task Management

- FR-1: Users shall create a new task.  
- FR-2: Admins shall assign tasks to users.  
- FR-3: Users and Admins shall edit task details according to their roles.  
- FR-4: Admins shall delete tasks.  
- FR-5: Users shall update the status of tasks assigned to them.

### 3.2 Task Viewing

- FR-6: Users shall view a list of tasks assigned to them.  
- FR-7: Admins shall view all tasks.  
- FR-8: Users and Admins shall view detailed information about a task, including comments and status history.

### 3.3 Comments

- FR-9: Users shall add comments to tasks.  
- FR-10: Users and Admins shall view all comments related to a task.  
- FR-11: Users may edit or delete their own comments; Admins can manage all comments.

### 3.4 User Management

- FR-12: Admins shall create, update, and delete users.  
- FR-13: Admins shall assign roles (Admin/User) to users.  
- FR-14: Admins shall view all registered users.

### 3.5 Authentication & Authorization

- FR-15: Users shall log in using a username and password.  
- FR-16: The system shall issue a JWT token upon successful login.  
- FR-17: All endpoints shall validate the JWT token and enforce role-based access.

### 3.6 Validation & Error Handling

- FR-18: The system shall validate input data on both frontend and backend.  
- FR-19: The system shall return meaningful error messages.

---

## 4. Non-Functional Requirements

### 4.1 Performance

- NFR-1: The system shall respond to user actions within 2 seconds.

### 4.2 Security

- NFR-2: All requests shall be validated for authentication and authorization.  
- NFR-3: Sensitive data shall be protected and errors handled securely.

### 4.3 Usability

- NFR-4: The system shall provide a user-friendly interface.  
- NFR-5: Validation and error messages shall be clear and informative.

### 4.4 Maintainability

- NFR-6: The system shall follow clean architecture principles.  
- NFR-7: The codebase shall be modular, well-documented, and easy to extend.

---

## 5. System Constraints

- Backend must use ASP.NET Core Web API.  
- Frontend must use Angular.  
- Database must use SQL Server.  
- Authentication must use JWT.  
- Role-based access must distinguish between Admin and User.

---

## 6. Assumptions

- Users are internal employees of Anazeem Company.  
- Admins are responsible for user and role management.  
- All users have network access to the internal portal.

---

## 7. Future Enhancements

- Real-time notifications for task updates  
- Reporting and analytics  
- Email integration for task assignment and reminders  
- Advanced filtering and search of tasks  
- Audit logs for user and task activities
