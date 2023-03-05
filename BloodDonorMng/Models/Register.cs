using Microsoft.Data.SqlClient;
using System.Data;

namespace AuthProject.Models
{
    public class Register
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;




        public static void CreateR(Register reg)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DotNetProject;Integrated Security=True;";
            try
            {
                cn.Open();

                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.CommandText = "insert into Register values (@Email, @Password)";
                cmdInsert.Parameters.AddWithValue("@Email", reg.Email);
                cmdInsert.Parameters.AddWithValue("@Password", reg.Password);
                cmdInsert.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally { cn.Close(); }
        }
    }


}
