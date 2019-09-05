using Microsoft.WindowsAzure.ServiceRuntime;
using System.Data.SqlClient;


namespace AppointmentDatabase
{
    public class Connection
    {
        public SqlConnection mConnection;
        public Connection()
        {
            string ConnectionString = "Server=tcp:appointmentsc.database.windows.net,1433;Initial Catalog=appointments;Persist Security Info=False;User ID=richa;Password=player#1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            //string ConnectionString = RoleEnvironment.GetConfigurationSettingValue("DBConnectionString");
            mConnection = new SqlConnection(ConnectionString);
            mConnection.Open();

            //bool x = RoleEnvironment.IsEmulated;
            
        }
    }
}
