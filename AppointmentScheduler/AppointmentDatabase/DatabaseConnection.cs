using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace AppointmentDatabase
{
    public class DatabaseConnection : Connection
    {
        public DbUsers UserLogin(DbUsers user)
        {
            using (SqlCommand cmd1 = new SqlCommand("LOGIN_USER", mConnection))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@USERNAME", user.UserName));
                //cmd1.Parameters.Add(new SqlParameter("@PASSWORD", user.Password));
                using (SqlDataReader reader = cmd1.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        user.UserId = (int)reader["UserId"];
                        user.FirstName = (string)reader["FirstName"];
                        user.LastName = (string)reader["LastName"];
                        user.EmailID = (string)reader["EmailID"];
                        user.PhoneNumber = (long)reader["PhoneNumber"];
                        user.Password = (string)reader["Password"];
                        return user;
                    }
                }

            }
            return user;
        }
        public ReturnCode.result UserRegistration(DbUsers user)
        {
            using (SqlCommand cmd1 = new SqlCommand("VALIDATE_USER", mConnection))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@EMAILID", user.EmailID));
                cmd1.Parameters.Add(new SqlParameter("@USERNAME", user.UserName));
                using (SqlDataReader reader = cmd1.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return ReturnCode.result.userexist;
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
                cmd.Parameters.Add(new SqlParameter("@FIRSTNAME", user.FirstName));
                cmd.Parameters.Add(new SqlParameter("@LASTNAME", user.LastName));
                cmd.Parameters.Add(new SqlParameter("@EMAILID", user.EmailID));
                cmd.Parameters.Add(new SqlParameter("@PHONENUMBER", user.PhoneNumber));
                int out1 = cmd.ExecuteNonQuery();
                if (out1 == 1)
                {
                    return ReturnCode.result.success;                   
                }
                else
                {
                    return ReturnCode.result.fail;
                }
            }
        }
        public ReturnCode.result changePassword(DbUsers user)
        {
            using(SqlCommand cmd=new SqlCommand("CHANGE_PASSWORD", mConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@USERNAME", user.UserName));
                cmd.Parameters.Add(new SqlParameter("@PASSWORD", user.Password));
                int out1 = cmd.ExecuteNonQuery();
                if (out1==1)
                {
                    return ReturnCode.result.success;
                }
                else
                {
                    return ReturnCode.result.fail;
                }

            }
        }

        public List<DbAppointmentInfo> CurrentAppointment(int UserId)
        {
            List <DbAppointmentInfo> TodayAppList = new List<DbAppointmentInfo>();
            using (SqlCommand cmd = new SqlCommand("CurrentDateAppointment", mConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("USER_ID",UserId));
                using(SqlDataReader reader=cmd.ExecuteReader())
                {
                    TodayAppList.Add(new DbAppointmentInfo
                    {
                        AppointmentID = (int)reader[0],
                        HostUser = (string)reader[1],
                        GuestUser = (string)reader[2],
                        DatenTime = (DateTime)reader[3],
                        Subject = (string)reader[4]
                    });
                }
            }
            return TodayAppList;

        }


        public List<String> getUsersList()
        {
            List<string> UserList = new List<string>();
            using (SqlCommand cmd=new SqlCommand("USERLIST", mConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {    
                    while (reader.Read())
                    {
                        UserList.Add((string)reader[0]);
                    }
                }
                    
            }
            return UserList;
        }

    }
}
