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
