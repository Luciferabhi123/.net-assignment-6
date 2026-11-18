// Question 3. Update student details using connected architecture
// Aim: To update the details of an existing student using SqlCommand.

using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
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
                string query =
                    "UPDATE Student SET StudentName = @Name, " +
                    "Age = @Age, Course = @Course WHERE StudentID = @ID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = name;
                    cmd.Parameters.Add("@Age", SqlDbType.Int).Value = age;
                    cmd.Parameters.Add("@Course", SqlDbType.NVarChar, 50).Value = course;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                    con.Open();

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        Console.WriteLine("Student details updated successfully.");
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
1. The user enters the Student ID whose details need to be changed.
2. New student details are accepted from the user.
3. The UPDATE query changes the name, age and course.
4. The WHERE condition ensures that only the student with the entered ID is updated.
5. ExecuteNonQuery() executes the UPDATE statement.
6. The affected row count tells us whether a record was updated.

Expected output:
Enter Student ID to update: 1
Enter New Student Name: Rahul Kumar
Enter New Age: 23
Enter New Course: MCA

Student details updated successfully.
*/
