using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ModelClass
{
    public class User : INotifyPropertyChanged
    {
        public int userID;
        public string userName;
        public string password;
        public bool isAdmin;

        private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public int UserID
        {
            get { return userID; }
            set
            {
                if (userID == value)
                {
                    return;
                }
                userID = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UserID"));
            }
        }
        public string UserName
        {
            get { return userName; }
            set
            {
                if (userName == value)
                {
                    return;
                }
                userName = value;


                //3
                List<string> errors = new List<string>();
                bool valid = true;

                if (value == null || value == "")
                {
                    errors.Add("User name cant be empty");
                    SetErrors("UserName", errors);
                    valid = false;
                }
                if (!Regex.Match(value, @"^[a-zA-Z]+$").Success)
                {
                    errors.Add("User name can only contain letters");
                    SetErrors("UserName", errors);
                    valid = false;
                }
                if (valid)
                {
                    ClearErrors("UserName");
                } //3 zavrsava 


                OnPropertyChanged(new PropertyChangedEventArgs("UserName"));
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                if (password == value)
                {
                    return;
                }
                password = value;

                //3 

                List<string> errors = new List<string>();
                bool valid = true;

                if (value == null || value == "")
                {
                    errors.Add("User password cant be empty");
                    SetErrors("Password", errors);
                    valid = false;
                }
                if (!Regex.Match(value, @"^[a-zA-Z]+$").Success)
                {
                    errors.Add("Password can only contain letters");
                    SetErrors("Password", errors);
                    valid = false;
                }
                if (valid)
                {
                    ClearErrors("Password");
                } //3


                OnPropertyChanged(new PropertyChangedEventArgs("Password"));
            }
        }
        public bool IsAdmin
        {
            get { return isAdmin; }
            set
            {
                if (isAdmin == value)
                {
                    return;
                }
                isAdmin = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsAdmin"));
            }
        }
        public bool HasErrors
        {
            get
            {
                return (errors.Count > 0);
            }
        }

        public User(string UserName, string Password, bool IsAdmin)
        {
            this.UserName = UserName;
            this.Password = Password;
            this.IsAdmin = IsAdmin;

        }
        public User(int Id, string UserName, string Password, bool IsAdmin)
        {
            this.UserID = Id;
            this.UserName = UserName;
            this.Password = Password;
            this.IsAdmin = IsAdmin;

        }
        public User()
        {
            UserName = "";
            Password = "";

        }
        public static User GetUserFromResultSet(SqlDataReader reader)
        {
            User user = new User((int)reader["UserID"], (string)reader["UserName"], (string)reader["Password"], (bool)reader["IsAdmin"]);
            return user;
        }
        public void UpdateUser()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("UPDATE Users SET UserName=@UserName, Password=@Password, IsAdmin=@IsAdmin WHERE UserID=@UserID", conn);

                SqlParameter userNameParam = new SqlParameter("@UserName", SqlDbType.NVarChar);
                userNameParam.Value = this.UserName;

                SqlParameter passwordParam = new SqlParameter("@Password", SqlDbType.NVarChar);
                passwordParam.Value = this.Password;

                SqlParameter isAdminParam = new SqlParameter("IsAdmin", SqlDbType.Bit);
                isAdminParam.Value = this.IsAdmin;


                SqlParameter IDParam = new SqlParameter("@UserID", SqlDbType.Int, 11);
                IDParam.Value = this.UserID;

                command.Parameters.Add(userNameParam);
                command.Parameters.Add(passwordParam);
                command.Parameters.Add(isAdminParam);

                command.Parameters.Add(IDParam);

                int rows = command.ExecuteNonQuery();

            }
        }
        public void DeleteUser()
        {
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("UPDATE Users SET is_deleted=1 WHERE id=@Id", conn);

                SqlParameter myParam = new SqlParameter("@UserID", SqlDbType.Int, 11);
                myParam.Value = this.UserID;

                command.Parameters.Add(myParam);

                int rows = command.ExecuteNonQuery();

            }
        }
        public void Insert()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("INSERT INTO Users(UserName, Password, IsAdmin, Is_Deleted)" +
                    " VALUES(@UserName, @Password, 0, 0); SELECT IDENT_CURRENT('Users');", conn);

                SqlParameter userNameParam = new SqlParameter("@UserName", SqlDbType.NVarChar);
                userNameParam.Value = this.UserName;

                SqlParameter passwordParam = new SqlParameter("@Password", SqlDbType.NVarChar);
                passwordParam.Value = this.Password;

                SqlParameter isAdminParam = new SqlParameter("@IsAdmin", SqlDbType.Bit);
                isAdminParam.Value = this.IsAdmin;

                command.Parameters.Add(userNameParam);
                command.Parameters.Add(passwordParam);
                command.Parameters.Add(isAdminParam);

                var id = command.ExecuteScalar();

                if (id != null)
                {
                    this.UserID = Convert.ToInt32(id);
                }

            }
        }
        public void Save()
        {
            if (UserID == 0)
            {
                Insert();

            }
            else
            {
                UpdateUser();
            }
        }
        private void SetErrors(string propertyName, List<string> propertyErrors)
        {
            //3

            errors.Remove(propertyName);
            errors.Add(propertyName, propertyErrors);
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void ClearErrors(string propertyName)
        {
            errors.Remove(propertyName);
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)   //3
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return (errors.Values);
            }
            else
            {
                return null;
            }
        }

    }
}
