// Question 4. Delete a student record based on Student ID
// Aim: To delete an existing student record from the Student table using connected architecture.

using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString =
            @"Server=.;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

        Console.Write("Enter Student ID to delete: ");
        int id = Convert.ToInt32(Console.ReadLine());

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Student WHERE StudentID = @ID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                    con.Open();

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        Console.WriteLine("Student record deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Student ID not found.");
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
1. The user enters the Student ID.
2. A DELETE query is created with a WHERE condition.
3. SqlConnection connects to the database.
4. SqlCommand executes the DELETE query.
5. ExecuteNonQuery() returns the number of deleted records.
6. A suitable message is displayed depending on the result.

Expected output:
Enter Student ID to delete: 2

Student record deleted successfully.
*/
