// Question 6. Insert a new student record using DataSet and SqlDataAdapter
// Aim: To insert a new student record into the Student table using disconnected architecture with DataSet and SqlDataAdapter.

using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        // SQL Server connection string
        string connectionString =
            @"Server=.;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

        // Taking student details from the user
        Console.Write("Enter Student Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Age: ");
        int age = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Course: ");
        string course = Console.ReadLine();

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Creating DataAdapter
                string query =
                    "SELECT StudentName, Age, Course FROM Student WHERE 1 = 0";

                SqlDataAdapter adapter = new SqlDataAdapter(query, con);

                // Creating DataSet
                DataSet ds = new DataSet();

                // Filling DataSet with table structure
                adapter.Fill(ds, "Student");

                // Creating INSERT command
                string insertQuery =
                    "INSERT INTO Student (StudentName, Age, Course) " +
                    "VALUES (@Name, @Age, @Course)";

                SqlCommand cmd = new SqlCommand(insertQuery, con);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "StudentName");
                cmd.Parameters.Add("@Age", SqlDbType.Int, 0, "Age");
                cmd.Parameters.Add("@Course", SqlDbType.NVarChar, 50, "Course");

                adapter.InsertCommand = cmd;

                // Creating a new row in DataSet
                DataRow row = ds.Tables["Student"].NewRow();

                row["StudentName"] = name;
                row["Age"] = age;
                row["Course"] = course;

                // Adding new row to DataSet
                ds.Tables["Student"].Rows.Add(row);

                // Sending changes to SQL Server
                int result = adapter.Update(ds, "Student");

                if (result > 0)
                {
                    Console.WriteLine("Student record inserted successfully.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.ReadLine();
    }
}

/* Explanation:
1. SqlDataAdapter is created to retrieve the table structure.
2. DataSet stores the table structure in memory.
3. NewRow() creates a new row.
4. Student details are assigned to the new row.
5. Rows.Add() adds the row to the DataSet.
6. InsertCommand defines how the new row will be inserted into SQL Server.
7. adapter.Update() sends the new record to the database.

Expected output:
Enter Student Name: Sonu
Enter Age: 22
Enter Course: MCA

Student record inserted successfully.


Note: WHERE 1 = 0 retrieves the table structure without retrieving existing records. StudentID is omitted because SQL Server generates it automatically through IDENTITY. */
