# ToDo API

This is a ToDo API built with **ASP.NET Core** for managing To-Do lists, categories, priorities, and tasks. It supports user authentication, CRUD operations, and filtering of to-do items. The API is Dockerized for easy deployment.

## Project Overview

The ToDo API allows users to manage their tasks by:

- **ToDo Lists**: Create, update, and delete to-do lists.
- **ToDo Items**: Manage individual tasks under specific to-do lists.
- **Priorities**: Assign priorities to tasks.
- **Categories**: Categorize tasks.
- **User Authentication**: Secure API endpoints using JWT Bearer tokens.

---

## 📋 Features

- ✅ User registration & login with JWT
- ✅ Role-based authorization (`owner`, `guest`)
- ✅ Manage ToDo Lists and Tasks or Items
- ✅ Add priorities and categories
- ✅ Filtering & Pagination
- ✅ Docker support

---

## Prerequisites

Before running the project, make sure you have the following installed on your machine:

- **Docker** (for running the application and database in containers)
- **Docker Compose** (to manage multi-container Docker applications)
- **.NET SDK 8** (optional, if you want to run the project locally without Docker)

---

## 🚀 Getting Started

1. **Clone the repository**:

   ```bash
   git clone https://github.com/FadiAlayyas/To-Do-List.git
   cd To-Do-List
   ```

2. **Run Migrations using ef**:

   ```bash
   cd src/TodoApi
   dotnet ef migrations add InitialCreate
   cd ../..
   ```

3. **Build the application using Docker**:

   ```bash
   docker-compose build
   ```

4. **Run the containers**:

   ```bash
   docker-compose up
   ```

---

5. **Project Base url**
   The API will be available at http://localhost:5000

6. **PostMan Base url**
   The PostMan Documentation https://documenter.getpostman.com/view/17854195/2sB2ixjE7i
