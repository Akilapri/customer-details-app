# Customer Details Web API

## Overview

This project is a simple ASP.NET Core Web API for managing customer details. It provides CRUD (Create, Read, Update, Delete) operations for customer data.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Configuration](#configuration)
- [API Documentation](#api-documentation)
- [Dependencies](#dependencies)
- [Contributing](#contributing)

## Prerequisites

Before you begin, ensure you have the following:

- [.NET SDK](https://dotnet.microsoft.com/download) installed
- [Node.js](https://nodejs.org/) installed (if you are working with a client-side application)
- [Visual Studio Code](https://code.visualstudio.com/) or another preferred IDE

## Getting Started

1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/customer-details-web-api.git

2. Navigate to the project directory

   ```bash
   cd CustomerDetailsApplication\CustomerDetailsWebApi

3. Build and run the project:
   The API should be accessible at https://localhost:5000.

   ```bash
    dotnet run

4. Navigate to the client-side directory (if applicable, replace with the actual directory):

   ```bash
   cd customer_details_app

5. Install frontend dependencies and run the React app.
The React app should be accessible at http://localhost:3000.

   ```bash
   npm install
   npm start


## Project Structure
**Database**: Contains the Entity Framework Core DbContext and database-related files.
**Repository**: Implements the data access layer and interacts with the database.
**Controllers**: Contains the API controllers for handling HTTP requests.
**ClientApp**: React frontend for managing customer data.

## Configuration
Configure the database connection in appsettings.json.

Adjust CORS settings in Program.cs based on your React app's origin.

## API Documentation
API documentation is generated using Swagger/OpenAPI.

Access the Swagger UI at https://localhost:5000/swagger to explore and test the API endpoints.

## Dependencies
ASP.NET Core
Entity Framework Core
React
Swagger/OpenAPI

## Contributing
Contributions are welcome! Please follow the Contributing Guidelines.
