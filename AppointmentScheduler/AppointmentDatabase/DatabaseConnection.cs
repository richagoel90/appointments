using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace AppointmentDatabase
{   public class DatabaseConnection : Connection
    {
        int result=0;
        public DbUsers UserLogin(DbUsers user)
        {
            using (SqlCommand cmd1 = new SqlCommand("LOGIN_USER", mConnection))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@USERNAME", user.UserName));
                cmd1.Parameters.Add(new SqlParameter("@PASSWORD", user.Password));
                using (SqlDataReader reader = cmd1.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        user.FirstName = (string)reader["FirstName"];
                        user.LastName = (string)reader["LastName"];
                        user.EmailID = (string)reader["EmailID"];
                        user.PhoneNumber = (long)reader["PhoneNumber"];
                        return user;
                    }
                }

            }
            return user;
        }
        public int UserRegistration(DbUsers user)
        {
            using (SqlCommand cmd1 = new SqlCommand("VALIDATE_USER", mConnection))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@EMAILID", user.EmailID));
                using (SqlDataReader reader = cmd1.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return 400;
                    }
       
                }
            }
            //string column = "(UserName, Password, FirstName, LastName, EmailID, PhoneNumber)";
            //String query1 = user.UserName + "','" + user.Password + "','" + user.FirstName + "','" + user.LastName + "','" + user.EmailID + "'," + user.PhoneNumber;
            //string query = "Insert into Users" + column + " values('" + query1 + ")";
            using (SqlCommand cmd = new SqlCommand("INSERT_USER", mConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@USERNAME", user.UserName));
                cmd.Parameters.Add(new SqlParameter("@PASSWORD", user.Password));
                cmd.Parameters.Add(new SqlParameter("@FIRSTNAME",user.FirstName));
                cmd.Parameters.Add(new SqlParameter("@LASTNAME", user.LastName));
                cmd.Parameters.Add(new SqlParameter("@EMAILID", user.EmailID));
                cmd.Parameters.Add(new SqlParameter("@PHONENUMBER", user.PhoneNumber));
                result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    return -1;
                }
            }
            return result;
        }
    }
}
