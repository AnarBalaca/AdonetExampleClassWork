using AdonetExample.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdonetExample.Services
{
    public interface IPersonService
    {
        public static List<Person> GetAllConnected(string _CONNECTION_STRING)
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
    }
}
