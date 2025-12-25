# TaskHub -- Internal Task Management System

### Anazeem Company

## 📌 Overview

**TaskHub** is an internal task management module developed for
**Anazeem Company** as part of a mid-level full-stack evaluation using
**Angular** and **ASP.NET Core Web API**.

The system enables teams and administrators to create, assign, track, and manage tasks efficiently while maintaining a clean architecture, scalable API design, a smooth user experience, and a full administration panel with user management, login authentication, and a dynamic menu.

------------------------------------------------------------------------

## 🎯 Features

### 🔹 Authentication & Administration

-   Authentication & Administration
-   Login page for users and admins
-   Role-based access control (Admin / User)
-   Administration panel with menu navigation
-   User management (create, edit, delete users)
-   Dashboard overview for tasks and statistics

### 🔹 Task Management

-   Create and assign tasks
-   Update task status (To Do, In Progress, Completed)
-   Edit and delete tasks
-   View task details

### 🔹 Comments

-   Add comments to tasks
-   Track task-related discussions and updates

### 🔹 Validation & UX

-   Frontend validation using Angular Reactive Forms
-   Backend validation with meaningful error messages
-   User-friendly feedback for actions and errors
-   Responsive layout with menu navigation

------------------------------------------------------------------------

## 🛠️ Tech Stack

### Backend

-   ASP.NET Core Web API
-   Entity Framework Core
-   Controller -- Service -- Repository pattern
-   SQL Server (Developer Edition)
-   DTOs and AutoMapper
-   JWT Authentication & Role-based Authorization
-   Global exception handling

### Frontend

-   Angular
-   Reactive Forms
-   Angular Services for API integration
-   Modular and scalable structure
-   Tailwind CSS for responsive UI
-   Admin panel with sidebar menu and top navigation

------------------------------------------------------------------------

## 🧱 Architecture

### Backend Structure

API
├── Controllers
├── Services
├── Repositories
├── Entities
├── DTOs
├── DbContext
├── Middleware
└── Authentication

### Frontend Structure

src/app
├── core
├── features
│   ├── auth
│   │   └── login
│   ├── dashboard
│   ├── tasks
│   │   ├── task-list
│   │   ├── task-form
│   │   └── task-details
│   └── users
│       ├── user-list
│       └── user-form
├── shared
└── services

------------------------------------------------------------------------

## 🚀 Getting Started

### Prerequisites

-   .NET SDK (.NET 7+)
-   Node.js & npm
-   Angular CLI
-   SQL Server

### Backend Setup

``` bash
cd backend
dotnet restore
dotnet ef database update
dotnet run
```

### Frontend Setup

``` bash
cd frontend
npm install
ng serve
```
------------------------------------------------------------------------

## 👤 Author

**Aref Al-Labadi**
Mid-Level Full Stack Developer (Angular & ASP.NET Core)

Developed for **Anazeem Company**
