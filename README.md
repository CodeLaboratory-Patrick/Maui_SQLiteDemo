# C# with SQLite: A Comprehensive Guide

SQLite is a lightweight, embedded, relational database management system (RDBMS) that is easy to set up and use directly with C#. It's ideal for applications that require a simple database without the overhead of managing a dedicated database server. Below, we'll explore what SQLite is, its features, provide examples, explain when to use it, and reference useful resources.

## What is SQLite?

SQLite is an open-source, serverless, self-contained SQL database engine. Unlike many other database systems, it doesn't require a server to run, and it stores the entire database as a single file on disk. This makes it ideal for small applications, testing environments, and use cases where simplicity and ease of use are more critical than advanced database capabilities.

## Features of SQLite

- **Self-contained**: SQLite requires no external dependencies, making it very easy to deploy.
- **Serverless**: No server process is needed; the database operates directly on the file system.
- **Zero Configuration**: No setup or configuration is needed to use SQLite, making it perfect for rapid development.
- **Cross-Platform**: SQLite runs on all major operating systems, including Windows, Linux, and macOS.
- **Lightweight**: SQLite is highly compact, often less than 1MB, and has minimal memory usage.
- **Atomic Transactions**: Supports atomic commit and rollback, ensuring database consistency.

| Feature        | Description                                                                 |
|----------------|-----------------------------------------------------------------------------|
| **Self-contained** | Requires no external software or libraries, easy to integrate.           |
| **Serverless**  | Operates directly from disk, no database server is needed.                 |
| **Cross-platform** | Compatible with multiple operating systems, ensuring portability.      |
| **Zero Configuration** | No installation or configuration is needed, easy to use out-of-the-box. |
| **Lightweight** | Small footprint suitable for embedded systems and mobile devices.          |
| **Transactions** | Supports commit and rollback operations, providing data reliability.      |

## Using SQLite in C#

### Setting Up SQLite in C#

To use SQLite in a C# project, you can use the `System.Data.SQLite` library, or more commonly, the **SQLite NuGet package**.

1. Open your project in **Visual Studio**.
2. Open **NuGet Package Manager** and search for `Microsoft.Data.Sqlite`.
3. Install the package.

### Example: Creating and Using an SQLite Database in C#

Here's a step-by-step example of how to create and use an SQLite database in a C# application.

```csharp
using System;
using System.Data.SQLite;

namespace SQLiteDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=sample.db;Version=3;";

            // Create Database Connection
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Create Table
                string createTableQuery = "CREATE TABLE IF NOT EXISTS students (id INTEGER PRIMARY KEY, name TEXT, age INTEGER);";
                using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Insert Data
                string insertDataQuery = "INSERT INTO students (name, age) VALUES ('John Doe', 25);";
                using (SQLiteCommand command = new SQLiteCommand(insertDataQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Read Data
                string selectQuery = "SELECT * FROM students;";
                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Name: {reader["name"]}, Age: {reader["age"]}");
                    }
                }
            }
        }
    }
}
```

### Explanation
- **Creating the Connection**: The `SQLiteConnection` object is used to connect to the database. If the specified database file (`sample.db`) doesn't exist, it will be automatically created.
- **Creating a Table**: We execute an SQL `CREATE TABLE` command to create a new table called `students`.
- **Inserting Data**: We insert a sample student record into the `students` table.
- **Reading Data**: We use an SQL `SELECT` query to read and print the data.

## When Should You Use SQLite?
SQLite is ideal for certain use cases, and understanding these can help you decide when it's the right fit for your application.

| Use Case                      | Description                                                                                   |
|-------------------------------|-----------------------------------------------------------------------------------------------|
| **Embedded Applications**     | Suitable for mobile, desktop, or IoT applications where a simple, integrated database is needed.|
| **Prototyping**               | Great for quickly building prototypes or proof-of-concept projects.                           |
| **Single-User Applications**  | Perfect for applications that don’t need concurrent user access, such as a personal finance app.|
| **Testing**                   | Can be used as a local, lightweight test database for testing code logic without overhead.    |
| **Small-to-Medium Projects**  | Suitable for projects with limited data and minimal complexity in the database interactions.   |

## When to Avoid SQLite
- **High Concurrency**: SQLite does not handle a large number of concurrent writes well.
- **Large Databases**: For very large data sets (multiple GB), it's more efficient to use a more robust RDBMS like MySQL or PostgreSQL.
- **Complex Transactions**: For complex, distributed transactions, a server-based RDBMS is better suited.

## Resources and References
Here are some useful resources for learning more about SQLite in C#:

- [SQLite Official Documentation](https://www.sqlite.org/docs.html)
- [Microsoft.Data.Sqlite Documentation](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/?tabs=netcore-cli)
- [SQLite C# Tutorial](https://zetcode.com/csharp/sqlite/)
- [NuGet - Microsoft.Data.Sqlite](https://www.nuget.org/packages/Microsoft.Data.Sqlite)
---

# Common Attributes in SQLite .NET

When working with SQLite in a .NET application, understanding the common attributes used in mapping between C# classes and SQLite tables is essential for managing your database effectively. Below, I'll provide a detailed guide about the various common attributes used in SQLite .NET, along with examples, situations where they are used, and references to learn more.

## What are SQLite .NET Attributes?

In SQLite .NET, attributes are used to define how C# classes are mapped to SQLite tables. Attributes specify key properties, such as table names, column constraints, and relationships between fields in the database. These attributes help ensure proper data handling, efficient storage, and simplified database management.

The attributes come from libraries like **SQLite.Net** or **SQLite-net-pcl**, which allow easy integration of SQLite databases with .NET applications.

## Common SQLite .NET Attributes

| Attribute         | Description                                                                                     | Example Usage                                           |
|-------------------|-------------------------------------------------------------------------------------------------|---------------------------------------------------------|
| `[Table]`         | Defines the name of the table to map the class to.                                              | `[Table("Students")]`                                  |
| `[PrimaryKey]`    | Specifies the primary key of the table.                                                         | `[PrimaryKey]`                                          |
| `[AutoIncrement]` | Indicates that the value should be automatically incremented for each new record.               | `[PrimaryKey, AutoIncrement]`                           |
| `[Column]`        | Defines the column name if it differs from the property name in the C# class.                   | `[Column("StudentName")]`                              |
| `[MaxLength]`     | Limits the maximum length of a string column.                                                   | `[MaxLength(50)]`                                       |
| `[Unique]`        | Ensures that the column value is unique across all records in the table.                        | `[Unique]`                                              |
| `[NotNull]`       | Specifies that the column cannot have a `NULL` value.                                           | `[NotNull]`                                             |
| `[Ignore]`        | Prevents the property from being mapped to the SQLite table, useful for non-persistent fields.  | `[Ignore]`                                              |
| `[Indexed]`       | Creates an index on the column, which can help speed up searches on that column.                | `[Indexed]`                                             |

### Detailed Examples
Let's explore how you can use these attributes in a typical C# class mapped to an SQLite database.

```csharp
using SQLite;

namespace SQLiteAttributesExample
{
    [Table("Students")]
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Column("StudentName"), MaxLength(50), NotNull]
        public string Name { get; set; }

        [Indexed]
        public int Age { get; set; }

        [Ignore]
        public string TempValue { get; set; } // This field will not be stored in the database.
    }
}
```

### Attribute Breakdown
- **[Table("Students")]**: The class `Student` is mapped to the `Students` table in the SQLite database.
- **[PrimaryKey, AutoIncrement]**: The `Id` property is set as the primary key and its value will automatically increment.
- **[Column("StudentName")]**: The `Name` property is mapped to the column `StudentName` in the table.
- **[MaxLength(50), NotNull]**: Specifies that `Name` cannot exceed 50 characters and cannot be null.
- **[Indexed]**: Adds an index to the `Age` column, making retrieval operations based on age faster.
- **[Ignore]**: The `TempValue` property is not mapped to the database, useful for data we only want to store temporarily in the class.

## When to Use SQLite .NET Attributes?

| Use Case                      | Description                                                                                   |
|-------------------------------|-----------------------------------------------------------------------------------------------|
| **Define Table Mappings**     | Attributes like `[Table]` and `[Column]` help control how C# classes relate to SQLite tables. |
| **Database Constraints**      | Attributes like `[PrimaryKey]`, `[NotNull]`, and `[Unique]` ensure data integrity in the database. |
| **Optimize Performance**      | Using `[Indexed]` can significantly improve the performance for certain queries.             |
| **Control Data Mapping**      | `[Ignore]` is helpful when you have data in the class you don't want persisted in the database. |

Attributes simplify the mapping between C# code and the underlying database structure. They are crucial when your application needs structured data management, enforces constraints, or ensures specific behaviors for particular properties.

## Resources and References
Here are some useful references for learning more about SQLite attributes in .NET:

- [SQLite Documentation](https://www.sqlite.org/docs.html)
- [SQLite-net-pcl Documentation](https://github.com/praeclarum/sqlite-net)
- [Microsoft Data SQLite Documentation](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/?tabs=netcore-cli)
- [C# SQLite Tutorial on ZetCode](https://zetcode.com/csharp/sqlite/)
---

# .NET + SQLite Data Types

When working with SQLite in a .NET application, understanding how data types in C# map to SQLite types is essential for building a robust and efficient database solution. This document provides an in-depth overview of the data types used in SQLite and how they relate to .NET types, alongside some detailed examples and a discussion on when and where to use them.

## Introduction to SQLite Data Types

SQLite uses a dynamic type system where values have a type but columns do not necessarily enforce a single data type. Instead of traditional data types like `INT` or `VARCHAR`, SQLite uses **storage classes** which define how the data is stored internally. The main storage classes are:

1. **NULL**: Represents a null value.
2. **INTEGER**: Represents signed integers (1, 2, 3, etc.).
3. **REAL**: Represents floating-point values.
4. **TEXT**: Represents text strings.
5. **BLOB**: Represents binary large objects.

## Mapping .NET Data Types to SQLite

In a .NET application, using the SQLite .NET library, it is important to understand how C# data types map to SQLite storage classes. Below is a table that shows the common mappings.

| .NET Type         | SQLite Storage Class | Description                                    |
|-------------------|----------------------|------------------------------------------------|
| `int`             | INTEGER              | Stores 32-bit signed integer values.           |
| `long`            | INTEGER              | Stores 64-bit signed integer values.           |
| `float`           | REAL                 | Stores floating-point values with lower precision.|
| `double`          | REAL                 | Stores double-precision floating-point values. |
| `string`          | TEXT                 | Stores text data in UTF-8 or UTF-16 format.    |
| `bool`            | INTEGER              | Stores `1` for `true` and `0` for `false`.     |
| `byte[]`          | BLOB                 | Stores binary data, such as images or files.   |
| `DateTime`        | TEXT/INTEGER/REAL    | Stores dates and times, often serialized as a formatted string or as a Unix timestamp. |

### Detailed Examples
Let's explore these mappings with an example C# class that will be stored in an SQLite database.

```csharp
using SQLite;
using System;

namespace SQLiteDataTypesExample
{
    [Table("Employees")]
    public class Employee
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        public int Age { get; set; }

        public double Salary { get; set; }

        public bool IsActive { get; set; }

        public DateTime HireDate { get; set; }

        public byte[] ProfilePicture { get; set; } // BLOB to store image data.
    }
}
```

### Explanation
- **`int` to INTEGER**: The `Id` and `Age` fields are stored as `INTEGER` in SQLite. They represent whole numbers.
- **`string` to TEXT**: The `Name` field is mapped to `TEXT` since it represents a sequence of characters.
- **`double` to REAL**: The `Salary` field is mapped to `REAL`, allowing storage of floating-point numbers.
- **`bool` to INTEGER**: The `IsActive` field is stored as `1` for true or `0` for false.
- **`DateTime` to TEXT/INTEGER/REAL**: The `HireDate` can be stored as a formatted string, an integer (Unix time), or a real number depending on the developer's choice.
- **`byte[]` to BLOB**: The `ProfilePicture` is stored as a BLOB, making it suitable for binary data like images.

## Handling Date and Time in SQLite with .NET
SQLite doesn’t have a native `DATETIME` type, so there are a few different ways to store `DateTime` values. Depending on your use case, you can choose between the following:

| Storage Method   | Description                                                                 |
|------------------|-----------------------------------------------------------------------------|
| **TEXT**         | Stores the date as a formatted string (`YYYY-MM-DD HH:MM:SS`).               |
| **INTEGER**      | Stores the date as a Unix timestamp (number of seconds since 1970-01-01).    |
| **REAL**         | Stores the date as Julian day numbers, representing dates in a floating-point format. |

The choice depends on the application's requirements for readability, precision, and ease of comparison. For instance, if you need to easily read the date values in the database, storing them as `TEXT` is often preferred.

### Example: DateTime Storage

```csharp
public class Order
{
    [PrimaryKey, AutoIncrement]
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; } // Stored as TEXT or INTEGER depending on the serialization method.
}
```

To serialize the `DateTime` as a string, you could use custom logic during reading and writing.

```csharp
public static void InsertOrder(SQLiteConnection db, Order order)
{
    string dateStr = order.OrderDate.ToString("yyyy-MM-dd HH:mm:ss");
    db.Execute("INSERT INTO Orders (OrderDate) VALUES (?)", dateStr);
}
```

## When to Use SQLite Data Types in .NET?

| Use Case                          | Description                                                                                 |
|-----------------------------------|---------------------------------------------------------------------------------------------|
| **Local Data Storage**            | When creating mobile, desktop, or IoT applications that require lightweight data storage.   |
| **Rapid Prototyping**             | Suitable for quickly prototyping applications with minimal configuration overhead.          |
| **Offline Applications**          | Ideal for applications needing offline storage without the need for a server-based database.|
| **Binary Data**                   | Use `BLOB` when storing binary data like images or documents.                               |
| **Simple Data Modeling**          | When the data relationships are straightforward and do not require complex joins or indexing.|

SQLite's data type flexibility allows developers to focus on application logic rather than worrying about rigid data type definitions. This flexibility makes it perfect for local databases in mobile or desktop applications, where simplicity and portability are crucial.

## References and Resources
Here are some references to help you learn more about SQLite data types in .NET:

- [SQLite Data Types](https://www.sqlite.org/datatype3.html)
- [Microsoft.Data.Sqlite Documentation](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/types)
- [C# SQLite Tutorial](https://zetcode.com/csharp/sqlite/)
---

# Understanding `SQLiteOpenFlags` in .NET

In SQLite for .NET, `SQLiteOpenFlags` is an enumeration that specifies options for how a database file should be opened. These flags control various behaviors of the SQLite connection, such as whether the file should be created if it doesn't exist or how it should be accessed (read-only or read-write). Understanding how to use these flags effectively can help ensure efficient database management and prevent issues related to data access.

In this document, we'll break down what `SQLiteOpenFlags` are, their various options, provide examples, explain when to use them, and offer resources for further reading.

## What is `SQLiteOpenFlags`?

`SQLiteOpenFlags` is an enumeration defined in the `SQLite` library that is used to configure the way an SQLite database connection is opened. This enumeration includes multiple options that you can combine to customize the behavior of your connection.

You typically use `SQLiteOpenFlags` when creating or configuring a database connection, for example, when using `SQLiteConnection` in the **SQLite-net** library for .NET applications.

## Common `SQLiteOpenFlags` Options

| Flag                       | Description                                                                                     |
|----------------------------|-------------------------------------------------------------------------------------------------|
| `ReadOnly`                 | Opens the database in read-only mode.                                                          |
| `ReadWrite`                | Opens the database in read-write mode, but will not create the file if it does not exist.       |
| `Create`                   | Creates the database file if it does not exist.                                                 |
| `FullMutex`                | Ensures that multiple threads can safely access the database.                                   |
| `SharedCache`              | Allows multiple database connections to share the same cache.                                   |
| `PrivateCache`             | Prevents the connection from using the shared cache, creating a private cache instead.          |
| `NoMutex`                  | Disables mutexes, allowing faster database access but risking thread safety.                    |

### Declaring `SQLiteOpenFlags` in C#
Here is an example of how to declare `SQLiteOpenFlags` in a .NET application:

```csharp
using SQLite;

public class DatabaseConfig
{
    public const SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite |
                                         SQLiteOpenFlags.Create |
                                         SQLiteOpenFlags.FullMutex;
}
```

### Explanation of Example
In the example above:
- **`SQLiteOpenFlags.ReadWrite`**: The database is opened in read-write mode, which means you can perform both read and write operations.
- **`SQLiteOpenFlags.Create`**: If the specified database file does not exist, it will be created.
- **`SQLiteOpenFlags.FullMutex`**: This ensures that the database can be accessed safely by multiple threads, preventing data corruption.

## Example: Using `SQLiteOpenFlags` in a Database Connection

Below is a code snippet demonstrating how to use `SQLiteOpenFlags` when establishing a connection to an SQLite database in a C# application:

```csharp
using SQLite;
using System;

namespace SQLiteOpenFlagsExample
{
    public class DatabaseService
    {
        private const string DatabasePath = "mydatabase.db";

        private static readonly SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite |
                                                        SQLiteOpenFlags.Create |
                                                        SQLiteOpenFlags.FullMutex;

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(DatabasePath, Flags);
        }

        public static void Main(string[] args)
        {
            using (var db = GetConnection())
            {
                Console.WriteLine("Database connected successfully with specified flags.");
            }
        }
    }
}
```

### Explanation
- **`DatabasePath`**: This defines the path where the database is stored or will be created.
- **`Flags`**: This constant contains multiple `SQLiteOpenFlags` values combined using the bitwise OR (`|`) operator.
- **`GetConnection()`**: This method returns a connection object that uses the specified flags, allowing you to interact with the database according to the given configuration.

## When to Use `SQLiteOpenFlags`?

| Use Case                         | Recommended Flags                                                                           |
|----------------------------------|---------------------------------------------------------------------------------------------|
| **Read-Only Access**             | `SQLiteOpenFlags.ReadOnly`                                                                  |
| **Full Read-Write Operations**   | `SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create`                                      |
| **Multi-Threaded Access**        | `SQLiteOpenFlags.FullMutex` for thread safety                                               |
| **In-Memory Database**           | Use in combination with `:memory:` as the database path                                     |
| **Private Cache Requirement**    | `SQLiteOpenFlags.PrivateCache` to prevent sharing cache with other connections              |

These flags allow for fine-grained control over how you interact with the SQLite database, which is particularly useful in scenarios that involve concurrency, cache management, or specific read/write requirements.

## Resources and References
Here are some resources you can use to learn more about `SQLiteOpenFlags` and their use in .NET:

- [SQLite-net GitHub Documentation](https://github.com/praeclarum/sqlite-net)
- [SQLite Data Types and Flags Documentation](https://www.sqlite.org/datatype3.html)
- [Microsoft.Data.Sqlite Overview](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/?tabs=netcore-cli)

---
# Understanding SQLite Connection in C#

The statement `connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);` is a common way to create a connection to an SQLite database in a C# application. It is part of the SQLite-net library for .NET, which makes it easy to interact with SQLite databases directly from your C# code.

In this document, we'll explore what this connection statement is, its features, provide detailed examples, explain when and where to use it, and reference useful resources.

## What is SQLite Connection?

`SQLiteConnection` is a class provided by the SQLite-net library, which is used to establish a connection to an SQLite database. It takes in parameters such as the **database path** and **connection flags** to control how the connection should be opened and what behavior should be enabled. This statement is fundamental to managing data in an SQLite database, such as inserting, querying, updating, or deleting records.

### Components of the Statement
- **`SQLiteConnection`**: This class represents a connection to a specific SQLite database.
- **`Constants.DatabasePath`**: This is typically a string that holds the path to the database file. It specifies where the database file is located or should be created.
- **`Constants.Flags`**: These are connection flags that determine how the SQLite database should be accessed (e.g., read-only, read-write, create if not exists).

## Example Code: Connecting to an SQLite Database
Here’s an example of how you can set up and use `SQLiteConnection` in a C# application.

```csharp
using SQLite;
using System;

namespace SQLiteConnectionExample
{
    public static class Constants
    {
        public const string DatabasePath = "mydatabase.db";
        public const SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite |
                                             SQLiteOpenFlags.Create |
                                             SQLiteOpenFlags.FullMutex;
    }

    public class DatabaseService
    {
        private SQLiteConnection connection;

        public DatabaseService()
        {
            connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
        }

        public void TestConnection()
        {
            Console.WriteLine("Database connection established successfully.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DatabaseService dbService = new DatabaseService();
            dbService.TestConnection();
        }
    }
}
```

### Explanation
- **`Constants.DatabasePath`**: Defines the path where the database file is stored. In this example, `"mydatabase.db"` is used as the database file name.
- **`Constants.Flags`**: Combines multiple `SQLiteOpenFlags` using the bitwise OR operator (`|`), allowing read-write access, automatic creation of the database file, and enabling full mutex for thread safety.
- **`new SQLiteConnection(Constants.DatabasePath, Constants.Flags)`**: This creates a new SQLite connection object using the specified path and flags.
- **`TestConnection()`**: A simple method that confirms the connection has been successfully created.

## Understanding SQLite Connection Flags
The connection flags are important as they determine the behavior of the database access. Below are some common flags used in SQLite .NET connections:

| Flag                       | Description                                                                                     |
|----------------------------|-------------------------------------------------------------------------------------------------|
| `ReadOnly`                 | Opens the database in read-only mode.                                                          |
| `ReadWrite`                | Opens the database in read-write mode, without creating it if it doesn’t exist.                 |
| `Create`                   | Creates the database file if it doesn’t already exist.                                         |
| `FullMutex`                | Ensures that the database connection is thread-safe by using a full mutex.                      |
| `NoMutex`                  | Disables the mutex, which may be faster but is not thread-safe.                                 |

## When to Use SQLite Connections in C#?

`SQLiteConnection` is typically used in scenarios that require a lightweight, serverless database solution for local storage. Here are some common use cases:

| Use Case                        | Description                                                                                 |
|---------------------------------|---------------------------------------------------------------------------------------------|
| **Mobile Applications**         | Ideal for mobile apps where local storage of structured data is needed.                     |
| **Desktop Applications**        | Useful for small to medium-sized desktop applications needing a simple data storage solution.|
| **IoT Devices**                 | Effective for devices with limited resources requiring data persistence.                    |
| **Prototyping**                 | Great for quickly prototyping an application without the need for a heavy database server.   |
| **Local Caching**               | Can be used as a local cache or a small datastore for holding data before syncing remotely.  |

## Pros and Cons of Using SQLite Connections

| Pros                                       | Cons                                           |
|-------------------------------------------|------------------------------------------------|
| **Lightweight**: Minimal resource usage.  | **Limited Concurrency**: Not ideal for heavy concurrent writes. |
| **Serverless**: No database server setup. | **No Advanced Features**: Lacks complex RDBMS features like stored procedures. |
| **Simple API**: Easy to implement and use.| **Single-User Focus**: Better for single-user or local applications.            |

## Resources for Further Reading
Here are some references to help you understand SQLite connections in C# more thoroughly:

- [SQLite Documentation](https://www.sqlite.org/docs.html)
- [SQLite-net GitHub Documentation](https://github.com/praeclarum/sqlite-net)
- [Microsoft Data SQLite Documentation](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/?tabs=netcore-cli)
- [C# SQLite Tutorial](https://zetcode.com/csharp/sqlite/)

---
# Understanding `connection.CreateTable<>()` in SQLite for C#

The method `connection.CreateTable<>()` is part of the SQLite-net library used in .NET applications for interacting with SQLite databases. This method provides a simple and efficient way to create a table in an SQLite database that corresponds to a C# class. In this guide, we will explore what `CreateTable<>()` is, its features, detailed examples, use cases, and offer references for further reading.

## What is `connection.CreateTable<>()`?

`CreateTable<>()` is a generic method in the **SQLite-net** library that allows you to create a table in the SQLite database based on the structure of a C# class. This method uses reflection to analyze the class properties, and it automatically generates the corresponding SQL command to create the table.

### Features of `CreateTable<>()`

- **Automatic Mapping**: Automatically creates an SQLite table based on a C# class structure, using attributes to define table and column characteristics.
- **Ease of Use**: No need to write SQL statements manually for table creation, which saves time and reduces the chances of errors.
- **Flexible Schema**: Supports a variety of attributes, such as `[PrimaryKey]`, `[AutoIncrement]`, `[NotNull]`, etc., to define column constraints.
- **Safe Execution**: If the table already exists, the method does nothing, preventing errors related to duplicate table creation.

### Example Code: Using `CreateTable<>()`

Below is an example of how to use `CreateTable<>()` to create a table for a C# class.

```csharp
using SQLite;
using System;

namespace SQLiteCreateTableExample
{
    public class Employee
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        public int Age { get; set; }

        public double Salary { get; set; }
    }

    public class DatabaseService
    {
        private SQLiteConnection connection;

        public DatabaseService(string databasePath)
        {
            connection = new SQLiteConnection(databasePath);
            connection.CreateTable<Employee>();
        }

        public void AddEmployee(Employee employee)
        {
            connection.Insert(employee);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var dbService = new DatabaseService("mydatabase.db");
            dbService.AddEmployee(new Employee { Name = "John Doe", Age = 30, Salary = 50000 });
            Console.WriteLine("Employee added successfully.");
        }
    }
}
```

### Explanation
- **`Employee` Class**: This class represents the structure of the table in the database. Attributes like `[PrimaryKey]` and `[AutoIncrement]` are used to define the primary key and auto-increment behavior for the `Id` column.
- **`connection.CreateTable<Employee>()`**: This line creates a table named `Employee` in the SQLite database if it does not already exist.
- **`AddEmployee` Method**: This method is used to insert a new employee record into the database.

## Attributes Supported by `CreateTable<>()`

| Attribute         | Description                                                                                     |
|-------------------|-------------------------------------------------------------------------------------------------|
| `[PrimaryKey]`    | Defines the primary key of the table.                                                           |
| `[AutoIncrement]` | Indicates that the value should automatically increment for each new record.                   |
| `[NotNull]`       | Specifies that the column cannot have a `NULL` value.                                           |
| `[Unique]`        | Ensures that the value in the column is unique across all records in the table.                 |
| `[Indexed]`       | Adds an index on the column, which can help speed up search operations.                         |

## When to Use `CreateTable<>()`?

The `CreateTable<>()` method is typically used in the following scenarios:

| Use Case                           | Description                                                                                 |
|------------------------------------|---------------------------------------------------------------------------------------------|
| **Application Initialization**     | Used when initializing an application to ensure all required tables are created.            |
| **Schema Setup for New Features**  | Creating new tables when adding new features to an application.                             |
| **Embedded or Mobile Databases**   | Perfect for embedded systems, mobile applications, or desktop applications needing lightweight local storage. |
| **Rapid Development**              | Helps during rapid development to quickly set up or modify the database structure.          |

### Pros and Cons of `CreateTable<>()`

| Pros                                       | Cons                                                       |
|-------------------------------------------|------------------------------------------------------------|
| **Simplifies Table Creation**: No SQL     | **Less Control**: Limited compared to manually defining tables with SQL. |
| **Error Prevention**: Reduces risk of syntax errors. | **Complex Schemas**: Not suitable for highly complex table structures or relations. |
| **Automatic Mapping**: Easily create tables from C# classes. | **Performance**: May be slower during initialization if the number of tables is large. |

## Resources for Further Reading
Here are some references to help you learn more about `CreateTable<>()` and its use in C# with SQLite:

- [SQLite Documentation](https://www.sqlite.org/docs.html)
- [SQLite-net GitHub Documentation](https://github.com/praeclarum/sqlite-net)
- [Microsoft Data SQLite Documentation](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/?tabs=netcore-cli)
- [C# SQLite Tutorial](https://zetcode.com/csharp/sqlite/)

---
# Understanding SQLite CRUD Operations in C#

The provided code is a series of methods to perform **CRUD** (Create, Read, Update, Delete) operations on an SQLite database using a C# application. These methods involve the use of SQLite-net to manage and manipulate data for a `Customer` object in a database. Let's break down what each method does, its features, provide detailed examples, and explore the scenarios where these operations are useful.

## Code Overview

The following methods are present in the code:

1. **`AddOrUpdate(Customer customer)`**: Handles inserting or updating a customer record in the database.
2. **`GetAll()`**: Retrieves all customer records from the database.
3. **`Get(int id)`**: Retrieves a specific customer by ID.
4. **`GetAllTheSecondVersion()`**: Retrieves all customer records using a direct SQL query.
5. **`Delete(int customerID)`**: Deletes a specific customer from the database.

### Features of CRUD Methods in SQLite

- **Exception Handling**: Each method includes a `try-catch` block to handle exceptions gracefully, ensuring that errors are caught and communicated through a `StatusMessage` variable.
- **Flexible Operations**: The `AddOrUpdate` method can both insert and update records, making it more versatile for data management.
- **Direct Querying**: Methods such as `GetAllTheSecondVersion` demonstrate the ability to use direct SQL queries when needed for more flexibility.

## Detailed Explanation of Each Method

### `AddOrUpdate(Customer customer)`
The `AddOrUpdate` method inserts a new customer record if the `ID` is zero (indicating a new customer) or updates an existing record if the `ID` is not zero.

```csharp
public void AddOrUpdate(Customer customer)
{
    int result = 0;
    try
    {
        if (customer.ID != 0)
        {
            result = connection.Update(customer);
            StatusMessage = $"{result} row(s) updated";
        }
        else
        {
            result = connection.Insert(customer);
            StatusMessage = $"{result} row(s) created";
        }
    }
    catch (Exception e)
    {
        StatusMessage = $"Error : {e.Message}";
    }
}
```

- **`Insert`**: Adds a new record to the database.
- **`Update`**: Updates an existing record.
- **`StatusMessage`**: Keeps track of the success or error messages.

### `GetAll()`
Retrieves all records from the `Customer` table and returns them as a list of `Customer` objects.

```csharp
public List<Customer> GetAll()
{
    try
    {
        return connection.Table<Customer>().ToList();
    }
    catch (Exception e)
    {
        StatusMessage = $"Error : {e.Message}";
    }
    return null;
}
```

- **`connection.Table<Customer>()`**: Uses LINQ to access the `Customer` table and convert it to a list.
- **Return Value**: Returns a list of all customer records, or `null` if an error occurs.

### `Get(int id)`
Fetches a specific customer based on the `ID` provided.

```csharp
public Customer Get(int id)
{
    try
    {
        return connection.Table<Customer>().FirstOrDefault(x => x.ID == id);
    }
    catch (Exception e)
    {
        StatusMessage = $"Error : {e.Message}";
    }
    return null;
}
```

- **`FirstOrDefault`**: Retrieves the first match based on the condition (`ID == id`). Returns `null` if no record matches.

### `GetAllTheSecondVersion()`
Retrieves all records from the `Customer` table using a raw SQL query.

```csharp
public List<Customer> GetAllTheSecondVersion()
{
    try
    {
        return connection.Query<Customer>("SELECT * FROM Customers").ToList();
    }
    catch (Exception e)
    {
        StatusMessage = $"Error : {e.Message}";
    }
    return null;
}
```

- **`Query<Customer>()`**: Executes a raw SQL command to retrieve all customer records.
- **Direct SQL Usage**: Useful when more complex queries are needed.

### `Delete(int customerID)`
Deletes a specific customer record based on the provided `customerID`.

```csharp
public void Delete(int customerID)
{
    try
    {
        var customer = Get(customerID);
        connection.Delete(customer);
    }
    catch (Exception e)
    {
        StatusMessage = $"Error : {e.Message}";
    }
}
```

- **`Delete`**: Removes the customer from the database if it exists.
- **Dependency on `Get()`**: The customer is first retrieved and then deleted, ensuring the customer exists before deletion.

## When to Use These CRUD Methods?

| Use Case                         | Description                                                                                 |
|----------------------------------|---------------------------------------------------------------------------------------------|
| **Application Initialization**   | Use `CreateTable<>()` followed by `AddOrUpdate()` to initialize and populate the database.  |
| **Data Management**              | Use `AddOrUpdate()` to handle both new inserts and updates for a streamlined user experience.|
| **Retrieving All Data**          | Use `GetAll()` or `GetAllTheSecondVersion()` to display data in a list or UI.               |
| **Single Record Operations**     | Use `Get(id)` to fetch specific customer details for editing or viewing.                    |
| **Record Deletion**              | Use `Delete()` to remove customers that are no longer needed, e.g., in admin panels.        |

## Pros and Cons of These CRUD Operations

| Pros                                        | Cons                                                         |
|--------------------------------------------|--------------------------------------------------------------|
| **Simplicity**: Easy-to-use API for database access. | **Error Handling**: May need more robust error handling depending on use case. |
| **Exception Management**: Built-in try-catch. | **Concurrency**: SQLite's write operations are not highly concurrent.          |
| **Combined Insert/Update**: Streamlined with `AddOrUpdate()`. | **Limited Control**: Less control compared to writing custom SQL for each operation. |

## Resources for Further Reading
Here are some references to learn more about SQLite CRUD operations in C#:

- [SQLite Documentation](https://www.sqlite.org/docs.html)
- [Microsoft Data SQLite Documentation](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/?tabs=netcore-cli)
- [C# SQLite Tutorial](https://zetcode.com/csharp/sqlite/)

---
# Understanding Cascade Operations in C# with SQLite

When working with relational databases like SQLite, the concept of **cascading** is an important tool for managing relationships between tables. Cascading helps to automatically apply changes, such as deletions or updates, from one table to another based on defined relationships. In this guide, we will explore what cascading is, how to use it in a C# application with SQLite, its features, provide detailed examples, and discuss the scenarios where it can be useful.

## What is Cascade in SQLite?

**Cascading** refers to operations in a relational database where changes in one table automatically cause corresponding changes in another related table. This is especially useful in maintaining referential integrity between related tables. Cascade operations are often used with **foreign keys** to ensure that updates or deletions of rows in a parent table propagate to the child table.

SQLite supports cascade operations through **foreign key constraints**. In the context of C#, these constraints can be implemented using attributes and methods available in the **SQLite-net** library, which allows the database to automatically manage related records.

### Types of Cascade Operations
- **ON DELETE CASCADE**: When a record in the parent table is deleted, all related records in the child table are also deleted automatically.
- **ON UPDATE CASCADE**: When a record in the parent table is updated, related records in the child table are updated accordingly.

## Enabling Foreign Keys in SQLite
SQLite requires explicit activation of foreign key constraints. To enable foreign key support in SQLite using C#, you need to run a command after opening a connection:

```csharp
connection.Execute("PRAGMA foreign_keys = ON;");
```
This ensures that foreign key constraints, including cascade operations, are enforced by the database engine.

## Example: Implementing Cascade in C# with SQLite
Consider a scenario where you have two tables: `Orders` and `OrderItems`. Each order can have multiple order items, creating a one-to-many relationship.

### Defining the Classes
```csharp
using SQLite;
using System.Collections.Generic;

public class Order
{
    [PrimaryKey, AutoIncrement]
    public int OrderId { get; set; }

    [NotNull]
    public string CustomerName { get; set; }

    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public List<OrderItem> Items { get; set; }
}

public class OrderItem
{
    [PrimaryKey, AutoIncrement]
    public int OrderItemId { get; set; }

    [ForeignKey(typeof(Order))]
    public int OrderId { get; set; }

    [NotNull]
    public string ProductName { get; set; }

    public int Quantity { get; set; }
}
```

### Explanation
- **`Order` Class**: Represents the `Orders` table. It includes an `OrderId` as the primary key and a list of `OrderItem` objects.
- **`OrderItem` Class**: Represents the `OrderItems` table. It includes `OrderItemId` as the primary key and `OrderId` as a foreign key, establishing a relationship with the `Order` table.
- **Cascade Attribute**: The `[OneToMany(CascadeOperations = CascadeOperation.All)]` attribute defines that operations on the `Order` should be cascaded to `OrderItem` (i.e., deleting an `Order` will also delete related `OrderItem` records).

### Enabling Foreign Keys and Creating Tables
```csharp
using System;

public class DatabaseService
{
    private SQLiteConnection connection;

    public DatabaseService(string databasePath)
    {
        connection = new SQLiteConnection(databasePath);
        connection.Execute("PRAGMA foreign_keys = ON;");
        connection.CreateTable<Order>();
        connection.CreateTable<OrderItem>();
    }

    public void DeleteOrder(int orderId)
    {
        try
        {
            connection.Delete<Order>(orderId);
            Console.WriteLine("Order and related items deleted successfully.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }
}
```
### Explanation
- **Foreign Keys Activation**: `PRAGMA foreign_keys = ON` ensures that SQLite enforces foreign key constraints, allowing cascading delete operations.
- **`DeleteOrder(int orderId)` Method**: Deletes an order from the `Orders` table. Due to the cascade setting, all associated order items in the `OrderItems` table are also deleted automatically.

## When to Use Cascade Operations?

| Use Case                          | Description                                                                                 |
|-----------------------------------|---------------------------------------------------------------------------------------------|
| **Maintaining Referential Integrity** | Automatically keep related data consistent without manual intervention.                  |
| **Complex Relationships**         | Simplify management of complex one-to-many or many-to-many relationships.                   |
| **Reducing Code Complexity**      | Minimize the need to write additional logic for managing related records.                   |
| **Data Cleanup**                  | Ensures that when a parent record is deleted, no orphaned child records remain.            |

## Pros and Cons of Using Cascades

| Pros                                         | Cons                                                            |
|----------------------------------------------|-----------------------------------------------------------------|
| **Automatic Cleanup**: Keeps database clean. | **Unintentional Deletions**: Improper use can lead to data loss.|
| **Consistency**: Maintains data integrity.   | **Limited Control**: Less manual control over deletions.        |
| **Simplified Code**: Less code to manage relationships. | **Performance Impact**: Cascades may slow down large delete operations. |

## Resources for Further Reading
Here are some references to help you learn more about cascading in SQLite with C#:

- [SQLite Documentation on Foreign Keys](https://www.sqlite.org/foreignkeys.html)
- [SQLite-net GitHub Documentation](https://github.com/praeclarum/sqlite-net)
- [Entity Framework with SQLite](https://learn.microsoft.com/en-us/ef/core/providers/sqlite/)
- [C# SQLite Tutorial](https://zetcode.com/csharp/sqlite/)

---
# Understanding Cascade Insert, Cascade Read, and Cascade Delete in C# with SQLite

Cascading in SQLite allows the propagation of changes (such as inserts, reads, and deletions) between related tables, maintaining consistency within relational databases. In C# applications, implementing cascading operations ensures that actions performed on a parent table also affect child tables without the need for explicit additional code. In this guide, we will explore how to perform **Cascade Insert**, **Cascade Read**, and **Cascade Delete** in SQLite using C#. We will go through what each operation is, its features, provide examples, and discuss relevant use cases.

## What are Cascade Operations?

Cascade operations ensure that when a change is made to a record in a parent table, related changes are automatically propagated to the associated records in child tables. This is crucial for maintaining data consistency and simplifying code in relational databases.

### Types of Cascade Operations
- **Cascade Insert**: Automatically inserts related records into the child table when a new parent record is added.
- **Cascade Read**: Reads all related records from child tables alongside the parent record, providing complete data retrieval for a given relationship.
- **Cascade Delete**: Deletes related records from the child table automatically when the corresponding parent record is deleted.

## Example Scenario: Orders and OrderItems
Consider a simple example where you have two tables: `Order` and `OrderItems`. Each order can contain multiple items, establishing a one-to-many relationship.

### Defining the Classes for Cascading Operations

```csharp
using SQLite;
using System.Collections.Generic;

public class Order
{
    [PrimaryKey, AutoIncrement]
    public int OrderId { get; set; }

    [NotNull]
    public string CustomerName { get; set; }

    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public List<OrderItem> Items { get; set; }
}

public class OrderItem
{
    [PrimaryKey, AutoIncrement]
    public int OrderItemId { get; set; }

    [ForeignKey(typeof(Order))]
    public int OrderId { get; set; }

    [NotNull]
    public string ProductName { get; set; }

    public int Quantity { get; set; }
}
```

### Enabling Foreign Keys in SQLite
SQLite requires foreign keys to be explicitly enabled to perform cascading operations:

```csharp
connection.Execute("PRAGMA foreign_keys = ON;");
```

This command must be run after establishing the connection to ensure that SQLite enforces the defined foreign key constraints.

## Detailed Examples of Cascade Operations

### 1. Cascade Insert
Cascade inserts occur when a new `Order` is inserted, and related `OrderItems` are automatically added.

```csharp
public void AddOrderWithItems(Order order)
{
    try
    {
        connection.InsertWithChildren(order, recursive: true);
        Console.WriteLine("Order and related items inserted successfully.");
    }
    catch (Exception e)
    {
        Console.WriteLine($"Error: {e.Message}");
    }
}
```
- **`InsertWithChildren()`**: This method inserts the `Order` and recursively inserts all associated `OrderItems`. The `recursive` parameter ensures that related child records are also inserted.

### 2. Cascade Read
Cascade reads are used to retrieve a parent record along with all its related child records in one operation.

```csharp
public Order GetOrderWithItems(int orderId)
{
    try
    {
        var order = connection.GetWithChildren<Order>(orderId);
        Console.WriteLine("Order and related items fetched successfully.");
        return order;
    }
    catch (Exception e)
    {
        Console.WriteLine($"Error: {e.Message}");
    }
    return null;
}
```
- **`GetWithChildren()`**: Fetches the `Order` object along with its associated `OrderItems`.

### 3. Cascade Delete
Cascade delete ensures that when an `Order` is deleted, all related `OrderItems` are deleted automatically.

```csharp
public void DeleteOrder(int orderId)
{
    try
    {
        var order = connection.GetWithChildren<Order>(orderId);
        connection.Delete(order, recursive: true);
        Console.WriteLine("Order and related items deleted successfully.");
    }
    catch (Exception e)
    {
        Console.WriteLine($"Error: {e.Message}");
    }
}
```
- **`Delete(order, recursive: true)`**: Deletes the `Order` and all associated `OrderItems` recursively.

## When to Use Cascade Operations?

| Use Case                           | Description                                                                                 |
|------------------------------------|---------------------------------------------------------------------------------------------|
| **Complex Parent-Child Relationships** | Suitable when managing one-to-many relationships where actions on the parent affect children. |
| **Data Consistency Maintenance**   | Ensures consistency by automating insert, update, or delete operations between related tables. |
| **Simplify Code**                  | Reduces the need for writing explicit logic to handle each child record individually.         |
| **Data Cleanup**                   | Prevents orphaned records when deleting parent records, ensuring the database remains clean.  |

### Pros and Cons of Cascade Operations

| Pros                                         | Cons                                                            |
|----------------------------------------------|-----------------------------------------------------------------|
| **Automated Management**: Saves time by automatically propagating changes. | **Data Loss Risk**: Improper use of cascade delete could result in unintended data loss. |
| **Consistency**: Keeps the database consistent and reduces redundant code. | **Performance**: Cascade operations may slow down when dealing with large datasets.      |
| **Code Simplification**: Easier to manage relationships without writing repeated logic. | **Complexity**: For large relationships, debugging cascade issues could be challenging. |

## Resources for Further Reading
To learn more about cascading operations in SQLite and C#:

- [SQLite Documentation on Foreign Keys](https://www.sqlite.org/foreignkeys.html)
- [SQLite-net GitHub Documentation](https://github.com/praeclarum/sqlite-net)
- [Entity Framework with SQLite](https://learn.microsoft.com/en-us/ef/core/providers/sqlite/)
- [C# SQLite Tutorial](https://zetcode.com/csharp/sqlite/)

---

# Understanding Relationships in C# with SQLite: One-to-One, One-to-Many, and Many-to-Many

In relational databases, relationships between tables are crucial for representing real-world data and its associations. SQLite, when used in a C# application, provides ways to manage different types of relationships, such as **One-to-One**, **One-to-Many**, and **Many-to-Many**. In this guide, we will explore these types of relationships, their characteristics, and how to implement them in C# using SQLite-net. We'll also go over examples and discuss scenarios where each type of relationship is most appropriate.

## Types of Relationships
- **One-to-One**: One record in a table is related to one and only one record in another table.
- **One-to-Many**: One record in a table is related to multiple records in another table.
- **Many-to-Many**: Many records in a table are related to many records in another table.

### Overview of Relationships
| Relationship Type   | Description                                                                                   | Example Use Case                              |
|---------------------|-----------------------------------------------------------------------------------------------|-----------------------------------------------|
| **One-to-One**      | Each record in the first table relates to a single record in the second table.                | A person has a unique passport.               |
| **One-to-Many**     | A single record in the first table relates to multiple records in the second table.           | A customer can have multiple orders.          |
| **Many-to-Many**    | Records in one table are related to multiple records in the second table, and vice versa.     | Students enrolled in multiple courses.        |

## 1. One-to-One Relationship
A **One-to-One** relationship means that each row in Table A is associated with exactly one row in Table B. This can be useful for separating optional information into another table to improve organization or security.

### Example: Person and Passport
Consider the relationship between a `Person` and a `Passport`.

```csharp
using SQLite;

public class Person
{
    [PrimaryKey, AutoIncrement]
    public int PersonId { get; set; }

    [NotNull]
    public string Name { get; set; }

    [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead | CascadeOperation.CascadeDelete)]
    public Passport Passport { get; set; }
}

public class Passport
{
    [PrimaryKey, AutoIncrement]
    public int PassportId { get; set; }

    [ForeignKey(typeof(Person))]
    public int PersonId { get; set; }

    [NotNull]
    public string PassportNumber { get; set; }
}
```
### Explanation
- **`Person` Class**: Represents individuals. Each `Person` can have an associated `Passport`.
- **`Passport` Class**: Contains details about the passport and links to a `Person` through the `PersonId` foreign key.
- **`[OneToOne]` Attribute**: Specifies a one-to-one relationship between `Person` and `Passport`.

## 2. One-to-Many Relationship
A **One-to-Many** relationship means that a row in Table A is related to multiple rows in Table B. This is the most common type of relationship.

### Example: Customer and Orders
Consider the relationship between a `Customer` and their `Orders`.

```csharp
using SQLite;
using System.Collections.Generic;

public class Customer
{
    [PrimaryKey, AutoIncrement]
    public int CustomerId { get; set; }

    [NotNull]
    public string Name { get; set; }

    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public List<Order> Orders { get; set; }
}

public class Order
{
    [PrimaryKey, AutoIncrement]
    public int OrderId { get; set; }

    [ForeignKey(typeof(Customer))]
    public int CustomerId { get; set; }

    [NotNull]
    public string Product { get; set; }
}
```
### Explanation
- **`Customer` Class**: Represents customers, each of whom may have multiple `Orders`.
- **`Order` Class**: Represents an individual order, linked to a `Customer` through `CustomerId`.
- **`[OneToMany]` Attribute**: Indicates that a `Customer` can have multiple `Orders`.

## 3. Many-to-Many Relationship
A **Many-to-Many** relationship occurs when multiple rows in Table A are related to multiple rows in Table B. This is usually implemented through a **junction table**.

### Example: Students and Courses
Consider a scenario where `Student` can enroll in multiple `Courses`, and each `Course` can have multiple `Students`.

```csharp
using SQLite;
using System.Collections.Generic;

public class Student
{
    [PrimaryKey, AutoIncrement]
    public int StudentId { get; set; }

    [NotNull]
    public string Name { get; set; }

    [ManyToMany(typeof(StudentCourse))]
    public List<Course> Courses { get; set; }
}

public class Course
{
    [PrimaryKey, AutoIncrement]
    public int CourseId { get; set; }

    [NotNull]
    public string CourseName { get; set; }

    [ManyToMany(typeof(StudentCourse))]
    public List<Student> Students { get; set; }
}

public class StudentCourse
{
    [ForeignKey(typeof(Student))]
    public int StudentId { get; set; }

    [ForeignKey(typeof(Course))]
    public int CourseId { get; set; }
}
```
### Explanation
- **`Student` Class**: Represents students, each of whom can enroll in multiple `Courses`.
- **`Course` Class**: Represents courses, each of which can have multiple `Students`.
- **`StudentCourse` Junction Table**: Represents the many-to-many relationship by linking `StudentId` and `CourseId`.
- **`[ManyToMany]` Attribute**: Indicates a many-to-many relationship, with a junction table (`StudentCourse`) to facilitate the relationship.

## When to Use Each Type of Relationship?
| Relationship Type   | Scenario                                                                                     |
|---------------------|---------------------------------------------------------------------------------------------|
| **One-to-One**      | Use when each entity in Table A must be uniquely linked to one entity in Table B. Useful for separating optional or sensitive data (e.g., separating contact details from the main user record). |
| **One-to-Many**     | Most suitable for relationships where a parent entity needs to link to multiple child entities. Examples include orders belonging to a customer or comments on a blog post. |
| **Many-to-Many**    | Use when multiple entities need to be related to multiple others. Ideal for scenarios like students enrolling in multiple courses or tags applied to multiple articles. |

## Pros and Cons of Relationships in SQLite
| Pros                                    | Cons                                                            |
|-----------------------------------------|-----------------------------------------------------------------|
| **Data Normalization**: Reduces redundancy by splitting data into separate tables. | **Complexity**: Can add complexity to database schema and retrieval logic. |
| **Referential Integrity**: Maintains relationships between data automatically. | **Performance Overhead**: Joins between tables may impact performance, especially for many-to-many relationships. |
| **Logical Organization**: Provides a structured, real-world representation of data. | **Foreign Key Management**: Requires proper handling of foreign keys for cascading operations. |

## Resources for Further Reading
Here are some references to learn more about managing relationships in SQLite using C#:

- [SQLite Official Documentation](https://www.sqlite.org/docs.html)
- [SQLite-net GitHub Documentation](https://github.com/praeclarum/sqlite-net)
- [Microsoft Data SQLite Documentation](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/?tabs=netcore-cli)
- [Entity Framework Relationships](https://learn.microsoft.com/en-us/ef/core/modeling/relationships)
- [C# SQLite Tutorial](https://zetcode.com/csharp/sqlite/)
