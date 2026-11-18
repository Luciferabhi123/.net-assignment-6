// Question 8. Delete a student record using DataSet and SqlDataAdapter
// Aim: To delete an existing student record using disconnected architecture.

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

        Console.Write("Enter Student ID to delete: ");
        int id = Convert.ToInt32(Console.ReadLine());

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Selecting all student records
                string query = "SELECT * FROM Student";

                SqlDataAdapter adapter = new SqlDataAdapter(query, con);

                // Creating DataSet
                DataSet ds = new DataSet();

                adapter.Fill(ds, "Student");

                DataTable table = ds.Tables["Student"];

                // Setting primary key
                table.PrimaryKey = new DataColumn[]
                {
                    table.Columns["StudentID"]
                };

                // Finding student by ID
                DataRow row = table.Rows.Find(id);

                if (row != null)
                {
                    // Marking selected row for deletion
                    row.Delete();

                    // Creating DELETE command
                    string deleteQuery =
                        "DELETE FROM Student WHERE StudentID = @ID";

                    SqlCommand cmd = new SqlCommand(deleteQuery, con);

                    SqlParameter idParameter =
                        cmd.Parameters.Add("@ID", SqlDbType.Int, 0, "StudentID");

                    idParameter.SourceVersion = DataRowVersion.Original;

                    adapter.DeleteCommand = cmd;

                    // Applying deletion to database
                    int result = adapter.Update(ds, "Student");

                    if (result > 0)
                    {
                        Console.WriteLine("Student record deleted successfully.");
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
1. SqlDataAdapter retrieves records from SQL Server.
2. DataSet stores the retrieved records.
3. Rows.Find(id) searches for the required student.
4. row.Delete() marks the selected record for deletion in the DataSet.
5. DeleteCommand defines the DELETE SQL statement.
6. adapter.Update() applies the deletion to SQL Server.

Expected output:
Enter Student ID to delete: 3

Student record deleted successfully. */
