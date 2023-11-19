using BlazorBootstrap;
using Microsoft.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace SharedClass.Components.Data
{
    public class Connection
    {
        private readonly string connectionString;

        public Connection()
        {
            string ipAddress = GetLocalIPv4Address();
            int sqlServerPort = GetSqlServerPort();
            connectionString = $"Data Source={ipAddress},{sqlServerPort};Initial Catalog=Example;Integrated Security = True; Encrypt=True;Trust Server Certificate=True";
        }
        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(connectionString);
        }
        private static string GetLocalIPv4Address()
        {
            string ipAddress = "";
            foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    foreach (var addressInfo in netInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (addressInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipAddress = addressInfo.Address.ToString();
                            break;
                        }
                    }
                }
            }
            return ipAddress;
        }
        private static int GetSqlServerPort()
        {
            // Replace "SQLEXPRESS" with your actual SQL Server instance name
            string instanceName = "SQLEXPRESS";

            string registryPath = $@"SOFTWARE\Microsoft\Microsoft SQL Server\{instanceName}\MSSQLServer\SuperSocketNetLib\Tcp";

            using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(registryPath))
            {
                if (key != null)
                {
                    object value = key.GetValue("TcpPort");
                    if (value != null && int.TryParse(value.ToString(), out int port))
                    {
                        return port;
                    }
                }
            }

            // Default port if registry read fails
            return 1433;
        }
    }
}