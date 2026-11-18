// Question 1. Display all student records using SqlConnection, SqlCommand and SqlDataReader
// Aim: To connect C# with SQL Server and retrieve all records from the Student table using connected architecture.

using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString =
            @"Server=.;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Student";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    Console.WriteLine("Student Records");
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine("ID\tName\t\tAge\tCourse");
                    Console.WriteLine("--------------------------------------------");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(
                                reader["StudentID"] + "\t" +
                                reader["StudentName"] + "\t\t" +
                                reader["Age"] + "\t" +
                                reader["Course"]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No student records found.");
                    }

                    reader.Close();
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
1. SqlConnection establishes a connection with the SQL Server database.
2. SqlCommand executes the SELECT query.
3. ExecuteReader() retrieves the records.
4. SqlDataReader reads the records one by one using Read().
5. The records are displayed on the console. */

/* Expected output:
Student Records
--------------------------------------------
ID      Name            Age     Course
--------------------------------------------
1       Rahul           21      MCA
2       Priya           22      BCA
3       Amit            20      B.Sc           */
