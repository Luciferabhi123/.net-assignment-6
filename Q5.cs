// Question 5. Retrieve and display all student records using SqlDataAdapter and DataSet
// Aim: To retrieve student records from SQL Server using SqlDataAdapter and DataSet in disconnected architecture.

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

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                {
                    DataSet ds = new DataSet();

                    // Fill the DataSet with records from the Student table
                    adapter.Fill(ds, "Student");

                    DataTable table = ds.Tables["Student"];

                    Console.WriteLine("Student Records");
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine("ID\tName\t\tAge\tCourse");
                    Console.WriteLine("--------------------------------------------");

                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            Console.WriteLine(
                                row["StudentID"] + "\t" +
                                row["StudentName"] + "\t\t" +
                                row["Age"] + "\t" +
                                row["Course"]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No student records found.");
                    }
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
1. SqlConnection defines the connection to SQL Server.
2. SqlDataAdapter acts as a bridge between the database and the DataSet.
3. DataSet stores the retrieved records in memory.
4. Fill() executes the SELECT query and fills the DataSet with the returned records.
5. DataTable accesses the Student table stored inside the DataSet.
6. A foreach loop displays each student's details.

Expected output:
Student Records
--------------------------------------------
ID      Name            Age     Course
--------------------------------------------
1       Rahul           21      MCA
2       Priya           22      BCA
3       Amit            20      B.Sc
*/
