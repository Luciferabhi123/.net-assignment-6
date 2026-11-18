# 💻 .NET Assignment 6 – C# Database Connectivity

Welcome to my **.NET Assignment 5** repository! 👋

This repository contains practical C# programs developed as part of my .NET laboratory assignment. The main objective is to understand database connectivity and perform CRUD operations using **ADO.NET and SQL Server**.

I have written these programs using simple logic, parameterized SQL queries, and practical examples to understand how C# communicates with a database.

---

## 🚀 Technologies Used

![C#](https://img.shields.io/badge/Language-C%23-purple)
![.NET](https://img.shields.io/badge/Framework-.NET-blue)
![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-red)
![ADO.NET](https://img.shields.io/badge/Technology-ADO.NET-green)
![Status](https://img.shields.io/badge/Status-In%20Progress-orange)

- **Programming Language:** C#
- **Framework:** .NET
- **Database:** Microsoft SQL Server
- **Database Technology:** ADO.NET
- **IDE:** Visual Studio
- **Version Control:** Git and GitHub

---

## 📚 Assignment Contents

Explore the programs below:

| No. | Assignment | Architecture | File |
|---|---|---|---|
| 1 | Display all student records using SqlDataReader | Connected | [Q1.cs](Q1.cs) |
| 2 | Insert a new student record | Connected | [Q2.cs](Q2.cs) |
| 3 | Update student details using SqlCommand | Connected | [Q3.cs](Q3.cs) |
| 4 | Delete a student using Student ID | Connected | [Q4.cs](Q4.cs) |
| 5 | Retrieve records using SqlDataAdapter and DataSet | Disconnected | [Q5.cs](Q5.cs) |

### ✅ Assignment Progress

- [x] Q1 – Display Student Records
- [x] Q2 – Insert Student Record
- [x] Q3 – Update Student Details
- [x] Q4 – Delete Student Record
- [x] Q5 – Retrieve Records using DataSet

---

## 🗄️ Database Configuration

Database Name: `StudentDB`

Table Name: `Student`

| Column | Data Type | Description |
|---|---|---|
| StudentID | INT | Primary Key (Identity) |
| StudentName | NVARCHAR(50) | Student Name |
| Age | INT | Student Age |
| Course | NVARCHAR(50) | Course Name |

### Connection String

```csharp
string connectionString =
    @"Server=.;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";
```

**Note:** Change the server name according to your SQL Server installation.

---

## ⚙️ How to Run These Programs

1. Install Visual Studio and Microsoft SQL Server.
2. Create a database named `StudentDB`.
3. Create the `Student` table.
4. Open the required `.cs` file in Visual Studio.
5. Configure the SQL Server connection string.
6. Run the program using `Ctrl + F5`.
7. Check the output in the console window.

---

## 📖 Important ADO.NET Concepts Covered

| Concept | Purpose |
|---|---|
| SqlConnection | Establishes database connection |
| SqlCommand | Executes SQL queries |
| SqlDataReader | Reads records one by one |
| SqlDataAdapter | Transfers data between database and memory |
| DataSet | Stores data in memory |
| ExecuteReader() | Executes SELECT queries |
| ExecuteNonQuery() | Executes INSERT, UPDATE and DELETE |
| Fill() | Fills DataSet with database records |

---

## 🎯 Learning Objectives

Through these practical programs, I am learning:

- How to connect C# applications with SQL Server.
- How to perform CRUD operations using ADO.NET.
- How connected architecture works.
- How disconnected architecture works.
- How to retrieve and manipulate database records.
- How to use parameterized SQL queries.

---

## 👨‍💻 About This Repository

This repository is created for academic learning and practical implementation of .NET programming concepts.

**Learning by coding, practicing and improving every day! 🚀**

⭐ If you find this repository useful, feel free to explore the programs and learn from them.

---

**Made with ❤️ using C# and SQL Server**
