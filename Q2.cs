// Question 2. Insert a new student record using connected architecture
// Aim: To insert a new student record into the Student table using SqlConnection and SqlCommand.

using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString =
            @"Server=.;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

        Console.Write("Enter Student Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Student Age: ");
        int age = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Student Course: ");
        string course = Console.ReadLine();

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query =
                    "INSERT INTO Student (StudentName, Age, Course) " +
                    "VALUES (@Name, @Age, @Course)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = name;
                    cmd.Parameters.Add("@Age", SqlDbType.Int).Value = age;
                    cmd.Parameters.Add("@Course", SqlDbType.NVarChar, 50).Value = course;

                    con.Open();

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        Console.WriteLine("Student record inserted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Record was not inserted.");
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
1. The user enters the student's name, age and course.
2. An INSERT query is created to add the new record.
3. Parameters are used to pass the entered values safely into the query.
4. con.Open() establishes the database connection.
5. ExecuteNonQuery() executes the INSERT statement and returns the number of affected rows. */

/* Expected output:
Enter Student Name: Sonu
Enter Student Age: 22
Enter Student Course: MCA

Student record inserted successfully.

Note: StudentID is generated automatically because we defined it as an IDENTITY column. */
