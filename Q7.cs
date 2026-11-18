// Question 7. Update an existing student record using DataSet and SqlDataAdapter
// Aim: To update an existing student's details using disconnected architecture.

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

        Console.Write("Enter Student ID to update: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter New Student Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter New Age: ");
        int age = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter New Course: ");
        string course = Console.ReadLine();

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Selecting student records
                string query = "SELECT * FROM Student";

                SqlDataAdapter adapter = new SqlDataAdapter(query, con);

                // Creating DataSet
                DataSet ds = new DataSet();

                adapter.Fill(ds, "Student");

                DataTable table = ds.Tables["Student"];

                // Setting StudentID as primary key
                table.PrimaryKey = new DataColumn[]
                {
                    table.Columns["StudentID"]
                };

                // Finding student using ID
                DataRow row = table.Rows.Find(id);

                if (row != null)
                {
                    // Updating values in DataSet
                    row["StudentName"] = name;
                    row["Age"] = age;
                    row["Course"] = course;

                    // Creating UPDATE command
                    string updateQuery =
                        "UPDATE Student SET StudentName = @Name, " +
                        "Age = @Age, Course = @Course WHERE StudentID = @ID";

                    SqlCommand cmd = new SqlCommand(updateQuery, con);

                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "StudentName");
                    cmd.Parameters.Add("@Age", SqlDbType.Int, 0, "Age");
                    cmd.Parameters.Add("@Course", SqlDbType.NVarChar, 50, "Course");

                    SqlParameter idParameter =
                        cmd.Parameters.Add("@ID", SqlDbType.Int, 0, "StudentID");

                    // Using the original StudentID in WHERE condition
                    idParameter.SourceVersion = DataRowVersion.Original;

                    adapter.UpdateCommand = cmd;

                    // Updating database from DataSet
                    int result = adapter.Update(ds, "Student");

                    if (result > 0)
                    {
                        Console.WriteLine("Student record updated successfully.");
                    }
                }
                else
                {
                    Console.WriteLine("Student ID not found.");
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
1. SqlDataAdapter retrieves student records.
2. Fill() stores records inside the DataSet.
3. Rows.Find(id) searches for a student using StudentID.
4. The selected row's values are modified in memory.
5. UpdateCommand defines the SQL UPDATE operation.
6. adapter.Update() sends the modified record to SQL Server.

Expected output:
Enter Student ID to update: 1
Enter New Student Name: Sonu Kumar
Enter New Age: 23
Enter New Course: MCA

Student record updated successfully. */
