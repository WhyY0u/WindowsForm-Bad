using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldSkils2
{
    class Base
    {
        public static readonly string connectionString = "server=localhost;user=root;password=k23092003;database=Session1;";
        public static User Login(String name, String password)
        {

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string sqlQuery = "SELECT * FROM user WHERE UserName = @UserName AND Password = @Password;";
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {

                        command.Parameters.AddWithValue("@UserName", name);
                        command.Parameters.AddWithValue("@Password", password);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.HasRows) return new User(reader["FullName"].ToString(), reader["Gender"].ToString(), reader["BrithDate"].ToString(), Convert.ToInt32(reader["FamilyCount"]), Convert.ToInt32(reader["Id"]));
                                else return null;
                            }
                        }
                    }
                }


                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            return null;
        }
        public static bool Register(string FullName, string login, string password, string date, string Gender, int Fc)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string sqlQuery = "INSERT INTO user (UserName, Password, FullName, Gender, BrithDate, FamilyCount) VALUES (@UserName, @Password, @FullName, @Gender, @BrithDate, @FamilyCount);";
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", login);
                        command.Parameters.AddWithValue("@FullName", FullName);
                        command.Parameters.AddWithValue("@Gender", Gender);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@BrithDate", date);
                        command.Parameters.AddWithValue("@FamilyCount", Fc);
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected);

                        if (rowsAffected <= 0)
                        {
                            throw new Exception("Не удалось добавить пользователя в базу данных.");
                            return false;
                        }
                        else
                        {
                            return true;

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Ошибка при регистрации пользователя: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return false;

        }

        public static bool CheckLogin(String login)
        {

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string sqlQuery = "SELECT * FROM user WHERE UserName = @UserName;";
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {

                        command.Parameters.AddWithValue("@UserName", login);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.HasRows) return false;
                            }
                            return true;
                        }
                    }
                }


                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            return false;
        }
    }
    
    class User
    {
       public String FullName, Gender, Birth;
       public int id, Fc;

        public User(String FullName, String Gender, String Birth, int Fc, int id)
        {
            this.FullName = FullName;
            this.Gender = Gender;
            this.Birth = Birth;
            this.Fc = Fc;
            this.id = id;
        }


    }
}
