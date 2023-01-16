using AdonetExample.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

public class Program
{

    private const string _CONNECTION_STRING = @"server=localhost;Database=PhoneBook;Trusted_Connection=true";





    public static List<Person> GetAllConnected()
    {
        //SqlConnection connection = new SqlConnection(_CONNECTION_STRING);

        SqlConnection connection = new SqlConnection();
        connection.ConnectionString = _CONNECTION_STRING;

        //SqlCommand command = new SqlCommand("", connection);
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        //command.CommandType = System.Data.CommandType.StoredProcedure;
        //command.CommandText = "GetAllPerson";
        //command.CommandText = "INSERT INTO People VALUES ('Ahmet','Mehmet','+90(532) 352 09 98','ahmet.mehmet@code.edu.az')";

        command.CommandText = "SELECT [Id], [FirstName], LastName, Phone, [Email] FROM People";

        if (connection.State == ConnectionState.Closed) connection.Open();
        List<Person> list = new List<Person>();

        // Data okuma işlemi
        SqlDataReader dr = command.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                Person person = new();
                person.Id = Convert.ToInt32(dr[0]);
                person.FirstName = dr["FirstName"].ToString();
                person.LastName = dr["LastName"].ToString();
                person.Phone = dr["Phone"].ToString();
                person.Email = dr["Email"].ToString();

                list.Add(person);
            }
        }

        dr.Close();
        connection.Close();
        return list;
    }

    public static List<Person> Search(string input)
    {
        //SqlConnection connection = new SqlConnection(_CONNECTION_STRING);

        SqlConnection connection = new SqlConnection();
        connection.ConnectionString = _CONNECTION_STRING;

        //SqlCommand command = new SqlCommand("", connection);
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        //command.CommandType = System.Data.CommandType.StoredProcedure;
        //command.CommandText = "GetAllPerson";
        //command.CommandText = "INSERT INTO People VALUES ('Ahmet','Mehmet','+90(532) 352 09 98','ahmet.mehmet@code.edu.az')";

        command.CommandText = $"SELECT [Id], [FirstName], LastName, Phone, [Email] FROM People where [FirstName] = '{input}' or LastName = '{input}' or Phone = '{input}' or [Email] = '{input}'";


        if (connection.State == ConnectionState.Closed) connection.Open();
        List<Person> list = new List<Person>();

        // Data okuma işlemi
        SqlDataReader dr = command.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                Person person = new();
                person.Id = Convert.ToInt32(dr[0]);
                person.FirstName = dr["FirstName"].ToString();
                person.LastName = dr["LastName"].ToString();
                person.Phone = dr["Phone"].ToString();
                person.Email = dr["Email"].ToString();

                list.Add(person);
            }
        }

        dr.Close();
        connection.Close();
        return list;
    }




    public static bool Delete(int id)
    {
        


        SqlConnection connection = new SqlConnection();
        connection.ConnectionString = _CONNECTION_STRING;


        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        //command.CommandText = $"INSERT INTO People VALUES ('{person.FirstName}','{person.LastName}','{person.Phone}','{person.Email}')";

        command.CommandText = $"Delete From People Where Id ={id}";
        
        //command.Parameters.AddWithValue("@LastName", person.LastName);

        //string text = "delete from People Where Id = @Id" + txtId.Text; 1



        if (connection.State == ConnectionState.Closed) connection.Open();

        bool result = command.ExecuteNonQuery() > 0;

        connection.Close();
        return result;
    }



    public static List<Person> GetAllDisConnected()
    {
        string command = "SELECT [Id], [FirstName], LastName, Phone, [Email] FROM People";
        SqlDataAdapter da = new SqlDataAdapter(command, _CONNECTION_STRING);

        DataTable dt = new DataTable();
        da.Fill(dt);


        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    Console.WriteLine(dt.Rows[i]["FirstName"]);
        //}
        //foreach (DataRow row in dt.Rows)
        //{
        //    Console.WriteLine(row.ItemArray[0]);
        //    Console.WriteLine(row.ItemArray[1]);
        //    Console.WriteLine(row.ItemArray[2]);
        //}


        var list = (from DataRow dr in dt.Rows
                    select new Person
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        Email = dr["Email"].ToString(),
                    }
           ).ToList();

        return list;
    }


    public static bool Insert(Person person)
    {
        Console.WriteLine("Add FirstName");
        person.FirstName = Console.ReadLine();
        Console.WriteLine("Add LastName");
        person.LastName = Console.ReadLine();
        Console.WriteLine("Add Phone");
        person.Phone = Console.ReadLine();
        Console.WriteLine("Add Email");
        person.Email = Console.ReadLine();



        SqlConnection connection = new SqlConnection();
        connection.ConnectionString = _CONNECTION_STRING;


        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        //command.CommandText = $"INSERT INTO People VALUES ('{person.FirstName}','{person.LastName}','{person.Phone}','{person.Email}')";

        command.CommandText = "INSERT INTO People VALUES ( @FirstName, @LastName, @Phone, @Email)";
        command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = person.FirstName;
        command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = person.LastName;
        command.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = person.Phone;
        command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = person.Email;
        //command.Parameters.AddWithValue("@LastName", person.LastName);

        //string text = "delete from People Where Id = @Id" + txtId.Text; 1



        if (connection.State == ConnectionState.Closed) connection.Open();

        bool result = command.ExecuteNonQuery() > 0;

        connection.Close();
        return result;
    }

    public static void Main()
    {
        //var people = GetAllConnected();
        //foreach (Person person in people)
        //{
        //    Console.WriteLine($"\n");
        //    foreach (PropertyInfo prop in person.GetType().GetProperties())
        //    {
        //        Console.WriteLine($"{prop.Name,-10} : {prop.GetValue(person)}");
        //    }
        //    Console.WriteLine($"\n{new String('_', 50)}");
        //}

      

        
        start:
        Console.WriteLine("1: add contact    2:Get Contacts    3:Search Contacts     0:Exit");

        string data = Console.ReadLine();

        switch (data)
        {

            case "1":
                bool result = Insert(new Person { });
                if (!result) return;
                goto start;
            case "2":

                var people = GetAllDisConnected();

                foreach (Person person in people)
                {
                    Console.WriteLine($"\n");
                    foreach (PropertyInfo prop in person.GetType().GetProperties())
                    {
                        Console.WriteLine($"{prop.Name,-10} : {prop.GetValue(person)}");
                    }
                    Console.WriteLine($"\n{new String('_', 50)}");
                }
                Console.WriteLine("1: Delete     2:back to menu");

                string newdata = Console.ReadLine();
                switch (newdata)
                {
                    case "1":
                        Console.WriteLine("insert id for delete ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Delete(id);

                        goto start;

                        case "2":
                        goto start;
                }

                goto start;
            case "3":
                Console.Write("search :");
                string input = Console.ReadLine();
                var people1 = Search(input);

                foreach (Person person in people1)
                {
                    Console.WriteLine($"\n");
                    foreach (PropertyInfo prop in person.GetType().GetProperties())
                    {
                        Console.WriteLine($"{prop.Name,-10} : {prop.GetValue(person)}");
                    }
                    Console.WriteLine($"\n{new String('_', 50)}");
                }
                break;
            default:
                Console.WriteLine("SiuuuuBanAllah");
                break;
        }

    }
}



