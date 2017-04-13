using System;
using System.Data;
using System.Data.SqlClient;

namespace AssignmentOnDatabase
{
    class DisconnectedArchitecture
    {
        static string conString = @"Server=INCHCMPC08657;Database = Northwind;trusted_connection = true;";
        static string cmdstring = @"Select * from Employees;Select * from Categories";
        static void Main(string[] args)
        {
            int number = 1;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("-----------------Welcome to Database-----------------");
          while (number==1)
            {
         
                string[] menuOption = { "1. Display Data", "2. Insert Data", "3. Update Data", "4. Delete Data", "5. Display Count 6. Exit" };
                foreach (var item in menuOption)
                {
                    Console.WriteLine(item);
                    Console.ResetColor();
                }
                int choice = GetInt("Enter your Choice");
                switch (choice)
                {
                    case 1:
                        displayData();

                        break;
                    case 2:
                        InsertData();

                        break;
                    case 3:
                        UpdateData();

                        break;
                    case 4:
                        deleteData();

                        break;
                    case 5:
                        DisplayCount();

                        break;
                    case 6:
                       number= 0;
                        break;

                    default:
                        Console.WriteLine("Invalid Entry!!!!!");
                        break;
                }
                Console.ReadLine();
            }
        }

        private static void DisplayCount()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string selectAllString = @"SELECT COUNT(*) FROM EMPLOYEES";
                using (SqlCommand cmd = new SqlCommand(selectAllString, con))
                {
                    int count = (int)cmd.ExecuteScalar();
                    Console.WriteLine($"Total no. of records = {count}");
                }
            }
        }

        private static void displayData()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(cmdstring, con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Console.WriteLine(dr["FirstName"]);
                    }
                }
            }
        }

        private static void UpdateData()
        {
            Console.WriteLine("Enter the id to be updated");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the FirstName to be updated");
            string First = Console.ReadLine();
            Console.WriteLine("Enter the LastName");
            string Last = Console.ReadLine();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(cmdstring, con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    SqlCommandBuilder scb = new SqlCommandBuilder(da);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if ((int)dr["EmployeeID"] == id)
                            dr["FirstName"] = First;
                        dr["LastName"] = Last;
                    }
                    da.Update(ds);
                }
            }
        }

        private static void InsertData()
        {

            Console.WriteLine("Enter the EmployeeID to be inserted ");
            string lname = Console.ReadLine();
            Console.WriteLine("Enter the firstname to be inserted ");
            string fname = Console.ReadLine();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(cmdstring, con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.Parameters.AddWithValue("@FirstName", fname);
                    cmd.Parameters.AddWithValue("@LastName", lname);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    SqlCommandBuilder scb = new SqlCommandBuilder(da);
                    DataRow dr = ds.Tables[0].NewRow();
                    dr["FirstName"] = fname;
                    dr["LastName"] = lname;
                    DataTable dt = ds.Tables[0];
                    dt.Rows.Add(dr);
                    da.Update(ds);
                }
            }
        }

        private static void deleteData()
        {
            Console.WriteLine("Enter the EmployeeID to be deleted ");
            int id = int.Parse(Console.ReadLine());
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(cmdstring, con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.Parameters.AddWithValue("@EmployeeID", id);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    SqlCommandBuilder scb = new SqlCommandBuilder(da);
                    DataRow dr = ds.Tables[0].NewRow();
                    foreach (DataRow dr1 in ds.Tables[0].Rows)
                    {
                        if ((int)dr1["EmployeeID"] == id)
                            dr.Delete();
                    }
                    da.Update(ds);
                }
            }
        }

        private static int GetInt(string message)
        {
            int val = 0;
            while (true)
            {
                Console.WriteLine(message);
                if (int.TryParse(Console.ReadLine(), out val))
                    break;

                Console.WriteLine("Error! Try again");
            }
            return val;

        }
    }

}
