# Development Environment Setup

## TaskHub -- Anazeem Company

This document lists all required tools and software that must be
installed before starting the TaskHub project.

------------------------------------------------------------------------

## 1. Operating System Requirements

-   Windows 10 or Windows 11
-   Minimum 8 GB RAM (16 GB recommended)
-   At least 10 GB free disk space

------------------------------------------------------------------------

## 2. Backend Requirements (ASP.NET Core)

### 2.1 .NET SDK

-   .NET SDK version 7 or later\
    Download from: https://dotnet.microsoft.com/download

Verify installation:

    dotnet --version

------------------------------------------------------------------------

### 2.2 Database

-   SQL Server Express
-   SQL Server Management Studio (SSMS): https://learn.microsoft.com/en-us/ssms/install/install?redirectedfrom=MSDN

Download from: https://www.microsoft.com/sql-server

Recommended configuration: - Authentication: Windows Authentication -
Server Name: localhost`\SQLEXPRESS`{=tex}

------------------------------------------------------------------------

## 3. Frontend Requirements (Angular)

### 3.1 Node.js

-   Node.js LTS version

Download from: https://nodejs.org

Verify installation:

    node -v
    npm -v

------------------------------------------------------------------------

### 3.2 Angular CLI

Install globally using npm:

    npm install -g @angular/cli

Verify installation:

    ng version

------------------------------------------------------------------------

## 4. Development Tools

### 4.1 Code Editor

Recommended: - Visual Studio Code

Download from: https://code.visualstudio.com/

Recommended Extensions: - C# - Angular Language Service - ESLint -
Prettier - GitLens

------------------------------------------------------------------------

### 4.2 Version Control

-   Git

Download from: https://git-scm.com/

Verify installation:

    git --version

------------------------------------------------------------------------

## 5. Optional Tools (Recommended)

-   Postman (API testing)
-   SQL Server Profiler (advanced DB debugging)

------------------------------------------------------------------------

This document ensures a complete and ready development environment for
the TaskHub project.
