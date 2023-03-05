using Microsoft.Data.SqlClient;

namespace AuthProject.Models
{
    public class City
    {
        public int CityId { get; set; }

        public string CityName { get; set; } = null!;

        public string State { get; set; } = null!;

        public static List<City> GetAll()
        {
            List<City> list = new List<City>();
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DotNetProject;Integrated Security=True;";
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM City", cn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new City
                            {
                                CityId = reader.GetInt32(0),
                                CityName = reader.GetString(1),
                                State = reader.GetString(2)
                            }); 
                        }
                    }
                    return list;
                }
                catch (Exception)
                {

                    throw;
                }
                finally { cn.Close(); }
            }
        }
    }
}
