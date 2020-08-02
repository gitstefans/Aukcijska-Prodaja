using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass
{
    public class UserCollection : ObservableCollection<User>
    {
        
        public static UserCollection GetAllUsers()
        {
            UserCollection users = new UserCollection();
            User user = null;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT userID, UserName, Password, IsAdmin FROM Users WHERE is_deleted = 0", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = User.GetUserFromResultSet(reader);

                        users.Add(user);

                        
                    }
                }

            }
            return users;
        }
    }
}
