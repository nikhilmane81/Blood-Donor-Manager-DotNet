using Microsoft.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace AuthProject.Models
{
    public class Donor
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Gender { get; set; } = null!;
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Please choose the City")]
        public int CityId { get; set; }

        [Display(Name = "Contact No")]
        public string Phone { get; set; } = null!;

        public string BloodGroup { get; set; } = null!;

        [Display(Name= "Date Of Birth")]
        public string Dob { get; set; } = null!;

        public string CityName { get; set; } = null!;
        public static List<Donor> GetAll()
        {
            List<Donor> list = new List<Donor>();
            using(SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DotNetProject;Integrated Security=True;";
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("select donor.id, donor.name, donor.gender, donor.email, city.cityName, donor.phone, donor.bloodgroup, donor.dob \r\nfrom donor join city\r\non donor.cityid = city.cityid", cn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Donor
                            {

                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Gender = reader.GetString(2),
                                Email = reader.GetString(3),
                                CityName= reader.GetString(4),
                                Phone = reader.GetString(5),
                                BloodGroup = reader.GetString(6),
                                Dob= (reader.GetString(7))
                            });
                        }
                    }
                    return list;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public static void CreateD(Donor donor)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DotNetProject;Integrated Security=True;";
            try
            {
                cn.Open();

                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.CommandText = "insert into Donor values (@name, @gender, @email, @cityid, @phone, @bloodgroup, @dob)";
                //cmdInsert.Parameters.AddWithValue("@Id", donor.Id);
                cmdInsert.Parameters.AddWithValue("@name", donor.Name);
                
                cmdInsert.Parameters.AddWithValue("@gender", donor.Gender);
                cmdInsert.Parameters.AddWithValue("@email", donor.Email);
                cmdInsert.Parameters.AddWithValue("@cityid", donor.CityId);
                cmdInsert.Parameters.AddWithValue("@phone", donor.Phone);
                cmdInsert.Parameters.AddWithValue("@bloodgroup", donor.BloodGroup);
                cmdInsert.Parameters.AddWithValue("@dob", donor.Dob);
                cmdInsert.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally { cn.Close(); }
        }

        public static Donor GetD(int id)
        {
            Donor donor = new Donor();
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DotNetProject;Integrated Security=True;";
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("select * from donor WHERE Id = @Id", cn);
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            donor.Id = reader.GetInt32(0);
                            donor.Name = reader.GetString(1);
                            
                            donor.Gender = reader.GetString(2);
                            donor.Email = reader.GetString(3);
                            donor.CityId = reader.GetInt32(4);
                            donor.Phone = reader.GetString(5);
                            donor.BloodGroup = reader.GetString(6);
                            donor.Dob = reader.GetString(7);
                        }
                        return donor;
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally { cn.Close(); }
            }
        }


        public static void EditD(int id, Donor donor)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DotNetProject;Integrated Security=True;";
            try
            {
                cn.Open();

                SqlCommand cmdUpdate = new SqlCommand();
                cmdUpdate.Connection = cn;
                cmdUpdate.CommandType = CommandType.Text;
                cmdUpdate.CommandText = "UPDATE donor SET name = @name,gender = @gender, email= @email, cityid = @cityid, phone= @phone, bloodgroup= @bloodgroup, dob = @dob WHERE Id = @id";
                cmdUpdate.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                cmdUpdate.Parameters.AddWithValue("@name", donor.Name);
                
                cmdUpdate.Parameters.AddWithValue("@gender", donor.Gender);
                cmdUpdate.Parameters.AddWithValue("@email", donor.Email);
                cmdUpdate.Parameters.AddWithValue("@cityid", donor.CityId);
                cmdUpdate.Parameters.AddWithValue("@phone", donor.Phone);
                cmdUpdate.Parameters.AddWithValue("@bloodgroup", donor.BloodGroup);
                cmdUpdate.Parameters.AddWithValue("@dob", donor.Dob);
                cmdUpdate.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { cn.Close(); }
        }

        public static void DeleteD(int id)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DotNetProject;Integrated Security=True;";
            try
            {
                cn.Open();

                SqlCommand cmdDelete = new SqlCommand();
                cmdDelete.Connection = cn;
                cmdDelete.CommandType = CommandType.Text;
                cmdDelete.CommandText = "Delete from donor where id = @id;";
                cmdDelete.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { cn.Close(); }
        }
    }
}
