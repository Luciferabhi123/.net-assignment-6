// Q10. Develop a C# program that demonstrates complete CRUD operations using both Connected and Disconnected ADO.NET architectures,
and compare the implementation of both approaches. 
  
using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    // SQL Server connection string
    static string connectionString =
        @"Server=.;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n===== STUDENT MANAGEMENT SYSTEM =====");

            Console.WriteLine("\nCONNECTED ARCHITECTURE");
            Console.WriteLine("1. Display Students");
            Console.WriteLine("2. Insert Student");
            Console.WriteLine("3. Update Student");
            Console.WriteLine("4. Delete Student");

            Console.WriteLine("\nDISCONNECTED ARCHITECTURE");
            Console.WriteLine("5. Display Students");
            Console.WriteLine("6. Insert Student");
            Console.WriteLine("7. Update Student");
            Console.WriteLine("8. Delete Student");

            Console.WriteLine("\n0. Exit");

            Console.Write("\nEnter your choice: ");

            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    // Connected architecture
                    case 1:
                        DisplayConnected();
                        break;

                    case 2:
                        InsertConnected();
                        break;

                    case 3:
                        UpdateConnected();
                        break;

                    case 4:
                        DeleteConnected();
                        break;

                    // Disconnected architecture
                    case 5:
                        DisplayDisconnected();
                        break;

                    case 6:
                        InsertDisconnected();
                        break;

                    case 7:
                        UpdateDisconnected();
                        break;

                    case 8:
                        DeleteDisconnected();
                        break;

                    case 0:
                        Console.WriteLine("Program closed.");
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }

    // ======================================
    // CONNECTED ARCHITECTURE
    // ======================================

    // Display all students using SqlDataReader
    static void DisplayConnected()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Student";

            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\nStudent Records");

            while (reader.Read())
            {
                Console.WriteLine(
                    reader["StudentID"] + "  " +
                    reader["StudentName"] + "  " +
                    reader["Age"] + "  " +
                    reader["Course"]);
            }

            reader.Close();
        }
    }

    // Insert student using ExecuteNonQuery
    static void InsertConnected()
    {
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Age: ");
        int age = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Course: ");
        string course = Console.ReadLine();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query =
                "INSERT INTO Student (StudentName, Age, Course) " +
                "VALUES (@Name, @Age, @Course)";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = name;
            cmd.Parameters.Add("@Age", SqlDbType.Int).Value = age;
            cmd.Parameters.Add("@Course", SqlDbType.NVarChar, 50).Value = course;

            con.Open();

            int result = cmd.ExecuteNonQuery();

            if (result > 0)
                Console.WriteLine("Student inserted successfully.");
        }
    }

    // Update student using ExecuteNonQuery
    static void UpdateConnected()
    {
        Console.Write("Enter Student ID: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter New Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter New Age: ");
        int age = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter New Course: ");
        string course = Console.ReadLine();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query =
                "UPDATE Student SET StudentName=@Name, Age=@Age, " +
                "Course=@Course WHERE StudentID=@ID";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = name;
            cmd.Parameters.Add("@Age", SqlDbType.Int).Value = age;
            cmd.Parameters.Add("@Course", SqlDbType.NVarChar, 50).Value = course;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

            con.Open();

            int result = cmd.ExecuteNonQuery();

            if (result > 0)
                Console.WriteLine("Student updated successfully.");
            else
                Console.WriteLine("Student ID not found.");
        }
    }

    // Delete student using ExecuteNonQuery
    static void DeleteConnected()
    {
        Console.Write("Enter Student ID: ");
        int id = Convert.ToInt32(Console.ReadLine());

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Student WHERE StudentID=@ID";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

            con.Open();

            int result = cmd.ExecuteNonQuery();

            if (result > 0)
                Console.WriteLine("Student deleted successfully.");
            else
                Console.WriteLine("Student ID not found.");
        }
    }

    // ======================================
    // DISCONNECTED ARCHITECTURE
    // ======================================

    // Display students using DataSet
    static void DisplayDisconnected()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Student";

            SqlDataAdapter adapter = new SqlDataAdapter(query, con);

            DataSet ds = new DataSet();

            adapter.Fill(ds, "Student");

            DataTable table = ds.Tables["Student"];

            Console.WriteLine("\nStudent Records");

            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine(
                    row["StudentID"] + "  " +
                    row["StudentName"] + "  " +
                    row["Age"] + "  " +
                    row["Course"]);
            }
        }
    }

    // Insert student using DataSet
    static void InsertDisconnected()
    {
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Age: ");
        int age = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Course: ");
        string course = Console.ReadLine();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query =
                "SELECT StudentName, Age, Course FROM Student WHERE 1=0";

            SqlDataAdapter adapter = new SqlDataAdapter(query, con);

            DataSet ds = new DataSet();

            adapter.Fill(ds, "Student");

            // Creating INSERT command
            SqlCommand cmd = new SqlCommand(
                "INSERT INTO Student (StudentName, Age, Course) " +
                "VALUES (@Name, @Age, @Course)", con);

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "StudentName");
            cmd.Parameters.Add("@Age", SqlDbType.Int, 0, "Age");
            cmd.Parameters.Add("@Course", SqlDbType.NVarChar, 50, "Course");

            adapter.InsertCommand = cmd;

            // Adding new row to DataSet
            DataRow row = ds.Tables["Student"].NewRow();

            row["StudentName"] = name;
            row["Age"] = age;
            row["Course"] = course;

            ds.Tables["Student"].Rows.Add(row);

            int result = adapter.Update(ds, "Student");

            if (result > 0)
                Console.WriteLine("Student inserted successfully.");
        }
    }

    // Update student using DataSet
    static void UpdateDisconnected()
    {
        Console.Write("Enter Student ID: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter New Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter New Age: ");
        int age = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter New Course: ");
        string course = Console.ReadLine();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            SqlDataAdapter adapter =
                new SqlDataAdapter("SELECT * FROM Student", con);

            DataSet ds = new DataSet();

            adapter.Fill(ds, "Student");

            DataTable table = ds.Tables["Student"];

            table.PrimaryKey = new DataColumn[]
            {
                table.Columns["StudentID"]
            };

            DataRow row = table.Rows.Find(id);

            if (row != null)
            {
                // Updating DataSet record
                row["StudentName"] = name;
                row["Age"] = age;
                row["Course"] = course;

                // Creating UPDATE command
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Student SET StudentName=@Name, Age=@Age, " +
                    "Course=@Course WHERE StudentID=@ID", con);

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "StudentName");
                cmd.Parameters.Add("@Age", SqlDbType.Int, 0, "Age");
                cmd.Parameters.Add("@Course", SqlDbType.NVarChar, 50, "Course");

                SqlParameter idParameter =
                    cmd.Parameters.Add("@ID", SqlDbType.Int, 0, "StudentID");

                idParameter.SourceVersion = DataRowVersion.Original;

                adapter.UpdateCommand = cmd;

                int result = adapter.Update(ds, "Student");

                if (result > 0)
                    Console.WriteLine("Student updated successfully.");
            }
            else
            {
                Console.WriteLine("Student ID not found.");
            }
        }
    }

    // Delete student using DataSet
    static void DeleteDisconnected()
    {
        Console.Write("Enter Student ID: ");
        int id = Convert.ToInt32(Console.ReadLine());

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            SqlDataAdapter adapter =
                new SqlDataAdapter("SELECT * FROM Student", con);

            DataSet ds = new DataSet();

            adapter.Fill(ds, "Student");

            DataTable table = ds.Tables["Student"];

            table.PrimaryKey = new DataColumn[]
            {
                table.Columns["StudentID"]
            };

            DataRow row = table.Rows.Find(id);

            if (row != null)
            {
                // Marking record for deletion
                row.Delete();

                // Creating DELETE command
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Student WHERE StudentID=@ID", con);

                SqlParameter idParameter =
                    cmd.Parameters.Add("@ID", SqlDbType.Int, 0, "StudentID");

                idParameter.SourceVersion = DataRowVersion.Original;

                adapter.DeleteCommand = cmd;

                int result = adapter.Update(ds, "Student");

                if (result > 0)
                    Console.WriteLine("Student deleted successfully.");
            }
            else
            {
                Console.WriteLine("Student ID not found.");
            }
        }
    }
}

/* Expected output
The menu will look like this:
===== STUDENT MANAGEMENT SYSTEM =====

CONNECTED ARCHITECTURE
1. Display Students
2. Insert Student
3. Update Student
4. Delete Student

DISCONNECTED ARCHITECTURE
5. Display Students
6. Insert Student
7. Update Student
8. Delete Student

0. Exit

Enter your choice:


For example, selecting 2 inserts a student using connected architecture, while selecting 6 inserts a student using disconnected architecture */
