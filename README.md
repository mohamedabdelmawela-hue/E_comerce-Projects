# 🛒 E-Commerce Web API

A complete **E-Commerce RESTful Web API** built with **ASP.NET Core** and **Entity Framework Core**, designed to demonstrate real-world backend development concepts such as authentication, authorization, layered architecture, shopping cart management, checkout, order processing, stock management, and database operations.

---

## 🚀 Project Overview

This project provides the backend infrastructure for an E-Commerce application.

Users can:

* Register and Login
* Authenticate using JWT
* Browse products and categories
* Add products to their shopping cart
* Update product quantities in the cart
* Remove products from the cart
* Checkout their cart
* Create orders automatically during checkout
* Manage product stock during checkout

The project was developed to practice building a structured, maintainable, and secure **ASP.NET Core Web API**.

---

## 🛠️ Technologies & Tools

* **C#**
* **ASP.NET Core Web API**
* **Entity Framework Core**
* **SQL Server**
* **LINQ**
* **JWT Authentication**
* **Role-Based Authorization**
* **RESTful APIs**
* **Repository Pattern**
* **Service Layer**
* **Dependency Injection**
* **DTOs**
* **Middleware**
* **Global Exception Handling**
* **Transactions**
* **Swagger / OpenAPI**
* **Git & GitHub**

---

## 🏗️ Architecture

The project follows a layered architecture to separate responsibilities between different parts of the application.

```text
Controller
    ↓
Service Layer
    ↓
Repository
    ↓
Entity Framework Core
    ↓
SQL Server
```

### Main Layers

#### Controllers

Responsible for:

* Receiving HTTP requests
* Validating request flow
* Extracting authenticated user information
* Returning HTTP responses

#### Service Layer

Contains the main business logic such as:

* User registration and login
* Product operations
* Cart management
* Checkout
* Order creation
* Stock management

#### Repository Layer

Responsible for database operations such as:

* Create
* Read
* Update
* Delete
* Querying entities

#### Models

Represent the database entities and their relationships.

#### DTOs

Used to control the data exchanged between the API and clients and avoid exposing database entities directly.

---

## 🔐 Authentication & Authorization

The API uses **JWT (JSON Web Token)** for authentication.

### Authentication Flow

```text
Register
   ↓
Login
   ↓
JWT Token
   ↓
Authorization Header
   ↓
Protected API Endpoints
```

Authenticated requests use:

```http
Authorization: Bearer {token}
```

The authenticated user's ID is retrieved from the JWT `NameIdentifier` claim when accessing user-specific resources.

For example, during checkout:

```text
POST /api/Checkout
       ↓
JWT
       ↓
UserId
       ↓
User Cart
       ↓
Checkout
```

This prevents the client from having to send the `userId` manually.

---

## 👤 User Management

The API supports:

* User Registration
* User Login
* JWT Authentication
* Role-Based Authorization

### Example

```http
POST /api/User/Register
```

```http
POST /api/User/Login
```

---

## 📦 Product Management

Products contain information such as:

* Product Name
* Description
* Price
* Quantity
* Category

Supported operations include:

```text
GET     /api/Product
GET     /api/Product/{id}
POST    /api/Product
PUT     /api/Product/{id}
DELETE  /api/Product/{id}
```

Products are associated with categories using Entity Framework Core relationships.

---

## 🗂️ Category Management

The API supports CRUD operations for product categories.

```text
GET     /api/Category
GET     /api/Category/{id}
POST    /api/Category
PUT     /api/Category/{id}
DELETE  /api/Category/{id}
```

---

## 🛒 Shopping Cart

Each user can have a shopping cart containing multiple products.

The cart system supports:

* Create Cart
* Get Cart
* Add Product
* Update Quantity
* Remove Product
* Clear Cart during Checkout

### Cart Flow

```text
User
 ↓
Cart
 ↓
Cart Items
 ↓
Products
```

When adding an existing product to the cart, the quantity is increased instead of creating a duplicate cart item.

---

## 🧾 Checkout

Checkout is the main business workflow of the project.

The client does not need to send the `userId`.

```http
POST /api/Checkout
```

The API gets the authenticated user's ID from the JWT token.

### Checkout Flow

```text
JWT
 ↓
Get User ID
 ↓
Get User Cart
 ↓
Get Cart Items
 ↓
Validate Products
 ↓
Validate Stock
 ↓
Calculate Total Price
 ↓
Create Order
 ↓
Create Order Items
 ↓
Decrease Product Stock
 ↓
Clear Cart
 ↓
Checkout Successfully
```

---

## 📋 Orders

Orders are created automatically during checkout.

An order contains information such as:

* User
* Order Date
* Total Price

Each order can contain multiple order items.

```text
Order
  │
  ├── Order Item
  ├── Order Item
  └── Order Item
```

---

## 📦 Stock Management

The system validates product stock before completing checkout.

Example:

```text
Available Stock = 10
Requested Quantity = 3

Checkout
   ↓
New Stock = 7
```

If the requested quantity is greater than the available stock, checkout is rejected.

---

## 🔄 Transaction Management

Checkout contains multiple database operations.

To maintain data consistency, the checkout process can be executed inside a database transaction.

```text
BEGIN TRANSACTION
       ↓
Create Order
       ↓
Create Order Items
       ↓
Decrease Stock
       ↓
Clear Cart
       ↓
COMMIT
```

If an error occurs:

```text
ROLLBACK
```

This prevents partially completed checkout operations.

---

## ⚠️ Exception Handling

The project uses custom middleware for centralized exception handling.

Instead of writing exception handling separately inside every controller, exceptions are handled through middleware.

Example response:

```json
{
  "Message": "Cart Is Empty"
}
```

This keeps controllers cleaner and provides consistent API responses.

---

## 🧪 API Documentation

Swagger / OpenAPI is used to test and document the API.

Swagger provides an interactive interface for:

* Testing endpoints
* Sending requests
* Testing JWT authentication
* Viewing request parameters
* Viewing API responses

---

## 🗄️ Database

The project uses:

**Microsoft SQL Server**

Entity Framework Core is used for:

* Database communication
* Entity mapping
* Relationships
* CRUD operations
* LINQ queries

Main entities include:

```text
User
Category
Product
Cart
CartItem
Order
OrderItem
```

---

## 🔗 Entity Relationships

Simplified relationship structure:

```text
User
 │
 └── Cart
      │
      └── CartItems
             │
             └── Product
                    │
                    └── Category


User
 │
 └── Orders
       │
       └── OrderItems
              │
              └── Product
```

---

## 📁 Project Structure

```text
myStore
│
├── Controllers
│   ├── UserController.cs
│   ├── ProductController.cs
│   ├── CategoryController.cs
│   ├── CartController.cs
│   ├── CartItemController.cs
│   └── CheckoutController.cs
│
├── Models
│   ├── userModel.cs
│   ├── productModel.cs
│   ├── categoryModel.cs
│   ├── cartModel.cs
│   ├── cartItemModel.cs
│   ├── orderModel.cs
│   └── orderItemModel.cs
│
├── DTOs
│   └── ...
│
├── Repository
│   ├── UserRepository.cs
│   ├── ProductRepository.cs
│   ├── CategoryRepository.cs
│   ├── CartRepository.cs
│   ├── CartItemRepository.cs
│   ├── OrderRepository.cs
│   └── OrderItemRepository.cs
│
├── ServiceLayer
│   ├── UserServiceLayer.cs
│   ├── ProductService.cs
│   ├── CategoryService.cs
│   ├── CartService.cs
│   ├── CartItemService.cs
│   └── CheckoutService.cs
│
├── Middleware
│   ├── ExceptionMiddleware.cs
│   └── NotFoundException.cs
│
├── Data
│   └── E_comerceContext.cs
│
└── Program.cs
```

---

## ⚙️ Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/mohamedabdelmawela-hue/E_comerce-Projects.git
```

### 2. Open the project

Open the solution using:

**Visual Studio**

### 3. Configure SQL Server

Update the connection string in:

```text
appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ECommerceDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 4. Apply migrations

```bash
Update-Database
```

or:

```bash
dotnet ef database update
```

### 5. Run the application

```bash
dotnet run
```

### 6. Open Swagger

After running the application, open the Swagger URL displayed by ASP.NET Core.

---

## 🧪 Example API Workflow

A typical customer workflow:

```text
1. Register
      ↓
2. Login
      ↓
3. Get JWT Token
      ↓
4. Authorize Swagger
      ↓
5. Browse Categories
      ↓
6. Browse Products
      ↓
7. Create Cart
      ↓
8. Add Product to Cart
      ↓
9. Update Quantity
      ↓
10. Checkout
      ↓
11. Order Created
      ↓
12. Stock Updated
      ↓
13. Cart Cleared
```

---

## 🎯 Project Goals

This project was built to gain practical experience with:

* Backend development using ASP.NET Core
* REST API design
* Database design
* Entity Framework Core
* Authentication and authorization
* Layered architecture
* Repository Pattern
* Service Layer
* Dependency Injection
* Business logic implementation
* Exception handling
* Shopping cart workflows
* Order processing
* Stock management
* Transaction management
* API testing with Swagger

---

## 🔮 Future Improvements

Possible future improvements include:

* Pagination
* Product Search
* Product Filtering
* Sorting
* Refresh Tokens
* Email Confirmation
* Password Reset
* Admin Dashboard
* Product Images
* Payment Gateway Integration
* Order Status Management
* Unit Testing
* Integration Testing
* Clean Architecture
* Unit of Work
* Docker

---

## 👨‍💻 Author

**Mohamed Abd Elmawela Kamal**

Junior .NET Backend Developer

### GitHub

`mohamedabdelmawela-hue`
### Technologies Focus
C#
ASP.NET Core
Web API
Entity Framework Core
SQL Server
JWT
RESTful APIs
## 📌 Note
This project is a practical backend project developed to demonstrate real-world **.NET Web API development concepts, architecture, authentication, database operations, and E-Commerce business workflows.**
