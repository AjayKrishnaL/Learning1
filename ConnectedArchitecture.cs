using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;


namespace AssignmentOnDatabase
{
    class ConnectedArchitecture
    {
        static string conString = @"Server=INCHCMPC08657;Database = Northwind;trusted_connection = true;";
        static void Main(string[] args)
        {
            int number = 1;
            Console.WriteLine("-----------------Welcome to Database-----------------");

            while (number==1)
            {
                string[] menuOption = { "1. Display Data", "2. Insert Data", "3. Update Data", "4. Delete Data", "5. Display Count" };
                foreach (var item in menuOption)
                {
                    Console.WriteLine(item);
                }

                int choice = GetInt("Enter your Choice");
                switch (choice)
                {
                    case 1:
                        DisplayData();

                        break;
                    case 2:
                        InsertData();


                        break;
                    case 3:
                        UpdateData();


                        break;
                    case 4:
                        DeleteData();


                        break;
                    case 5:
                        DisplayCount();

                        break;
                    case 6:
                        number = 0;
                        break;
                    default:
                        Console.WriteLine("Invalid Entry!!!!!");
                        break;
                }
            }

            Console.ReadLine();
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

        private static void DeleteData()
        {
            Console.WriteLine("Enter the LastName to be deleted ");
            string Last = Console.ReadLine();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                string delateString = @"DELETE FROM Employees WHERE LastName=@LastName";
                using (SqlCommand cmd = new SqlCommand(delateString, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;

                    cmd.Parameters.AddWithValue("@LastName", Last);


                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static void UpdateData()
        {
            Console.WriteLine("Enter the FirstName to be updated");
            string First = Console.ReadLine();
            Console.WriteLine("Enter the LastName");
            string Last = Console.ReadLine();

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                string updateString = @"UPDATE Employees SET FirstName =@FirstName WHERE LastName=@LastName";
                using (SqlCommand cmd = new SqlCommand(updateString, con))
                {

                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@FirstName", First);
                    cmd.Parameters.AddWithValue("@LastName", Last);


                    cmd.ExecuteNonQuery();
                }
            }


        }

        private static void InsertData()
        {
            Console.WriteLine("Enter the FirstName");
            string First = Console.ReadLine();
            Console.WriteLine("Enter the LastName");
            string Last = Console.ReadLine();

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                string insertString = @"INSERT INTO Employees (FirstName, LastName) VALUES (@FirstName,@LastName) ";
                using (SqlCommand cmd = new SqlCommand(insertString, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@FirstName", First);
                    cmd.Parameters.AddWithValue("@LastName", Last);


                    cmd.ExecuteNonQuery();
                }
            }

        }

        private static void DisplayData()
        {


            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string selectAllString = @"SELECT * FROM EMPLOYEES";
                using (SqlCommand cmd = new SqlCommand(selectAllString, con))
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Console.WriteLine($"FirstName: {rdr["FirstName"]}  | LastName: {rdr["LastName"]}");
                    }
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
