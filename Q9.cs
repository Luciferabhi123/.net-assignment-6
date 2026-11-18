// Question 9. Perform Insert, Update and Delete using SqlCommandBuilder and DataSet
// Aim: To perform INSERT, UPDATE and DELETE operations on the Student table using SqlCommandBuilder, SqlDataAdapter and DataSet.

/* Important concept:

Normally, we manually create InsertCommand, UpdateCommand and DeleteCommand.

But SqlCommandBuilder automatically generates these SQL commands from the adapter's SELECT statement, provided that the query and table schema support it. */

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

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Selecting all student records
                string query = "SELECT StudentID, StudentName, Age, Course FROM Student";

                SqlDataAdapter adapter = new SqlDataAdapter(query, con);

                // Retrieving primary key and other schema information
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                // Creating DataSet
                DataSet ds = new DataSet();

                // Filling DataSet
                adapter.Fill(ds, "Student");

                DataTable table = ds.Tables["Student"];

                // Creating CommandBuilder
                using (SqlCommandBuilder builder = new SqlCommandBuilder(adapter))
                {
                    // Displaying menu
                    Console.WriteLine("STUDENT MANAGEMENT");
                    Console.WriteLine("1. Insert Student");
                    Console.WriteLine("2. Update Student");
                    Console.WriteLine("3. Delete Student");

                    Console.Write("Enter your choice: ");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:

                            // Taking new student details
                            Console.Write("Enter Student Name: ");
                            string name = Console.ReadLine();

                            Console.Write("Enter Age: ");
                            int age = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Enter Course: ");
                            string course = Console.ReadLine();

                            // Creating a new DataRow
                            DataRow newRow = table.NewRow();

                            newRow["StudentName"] = name;
                            newRow["Age"] = age;
                            newRow["Course"] = course;

                            // Adding row to DataSet
                            table.Rows.Add(newRow);

                            // Automatically generated INSERT command
                            adapter.Update(ds, "Student");

                            Console.WriteLine("Student inserted successfully.");
                            break;

                        case 2:

                            // Taking student ID
                            Console.Write("Enter Student ID: ");
                            int updateID = Convert.ToInt32(Console.ReadLine());

                            // Finding student record
                            DataRow updateRow = table.Rows.Find(updateID);

                            if (updateRow != null)
                            {
                                Console.Write("Enter New Name: ");
                                updateRow["StudentName"] = Console.ReadLine();

                                Console.Write("Enter New Age: ");
                                updateRow["Age"] = Convert.ToInt32(Console.ReadLine());

                                Console.Write("Enter New Course: ");
                                updateRow["Course"] = Console.ReadLine();

                                // Automatically generated UPDATE command
                                adapter.Update(ds, "Student");

                                Console.WriteLine("Student updated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Student ID not found.");
                            }

                            break;

                        case 3:

                            // Taking student ID
                            Console.Write("Enter Student ID: ");
                            int deleteID = Convert.ToInt32(Console.ReadLine());

                            // Finding student record
                            DataRow deleteRow = table.Rows.Find(deleteID);

                            if (deleteRow != null)
                            {
                                // Marking record for deletion
                                deleteRow.Delete();

                                // Automatically generated DELETE command
                                adapter.Update(ds, "Student");

                                Console.WriteLine("Student deleted successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Student ID not found.");
                            }

                            break;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
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
1. SqlDataAdapter retrieves records using the SELECT query.
2. MissingSchemaAction.AddWithKey helps retrieve primary key information.
3. SqlCommandBuilder generates INSERT, UPDATE and DELETE commands automatically.
4. DataSet stores the student records in memory.
5. The switch statement allows the user to choose an operation.
6. Rows.Add() adds a new student.
7. Changing DataRow column values modifies an existing student.
8. Rows.Find() searches for a student using StudentID.
9. Rows.Delete() marks the selected record for deletion.
10. adapter.Update() executes the generated commands and saves the changes to SQL Server.

Expected output – Insert:
STUDENT MANAGEMENT
1. Insert Student
2. Update Student
3. Delete Student

Enter your choice: 1

Enter Student Name: Mohit
Enter Age: 22
Enter Course: MCA

Student inserted successfully.


Expected output – Update:
Enter your choice: 2

Enter Student ID: 1
Enter New Name: Rahul Kumar
Enter New Age: 23
Enter New Course: MCA

Student updated successfully.


Expected output – Delete:
Enter your choice: 3

Enter Student ID: 2

Student deleted successfully.

Important technical note for Q9

SqlCommandBuilder needs a SELECT command that identifies a single table and includes its primary key.
SQL Server's identity metadata must also be available so that the generated INSERT command handles StudentID correctly. The StudentID value is not entered manually. */
