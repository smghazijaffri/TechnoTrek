using Microsoft.Data.SqlClient;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace SharedClass.Components.Data
{
    public class Connection
    {
        public readonly string connectionString;

        public Connection()
        {
            //string ipAddress = GetIPv4Addresses(); //Auto IPv4
            //connectionString = $"Data Source={ipAddress},1433;Initial Catalog=Computer;User Id=Admin;Password=12345;Encrypt=False;"; //Auto IPv4
            connectionString = $"Data Source=192.168.0.106,1433;Initial Catalog=Computer;User Id=Admin;Password=12345;Encrypt=False;"; //Manual IPv4
            //connectionString = $"Data Source=DESKTOP-M62686B\\SQLEXPRESS;Initial Catalog=Computer;User Id=Admin;Password=12345;Encrypt=False;"; //Sufiyan Local
        }

        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(connectionString);
        }

        private static string GetIPv4Addresses() //Auto IPv4
        {
            string ipAddress = "";
            // Get all network interfaces on the machine
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                // Filter out loopback and non-operational interfaces
                if (networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback
                    && networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    // Get the IP properties of the current interface
                    IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();

                    // Filter out IPv6 addresses and get the IPv4 addresses
                    IPAddress[] ipv4Addresses = ipProperties
                        .UnicastAddresses
                        .Where(addr => addr.Address.AddressFamily == AddressFamily.InterNetwork)
                        .Select(addr => addr.Address)
                        .ToArray();

                    foreach (IPAddress ipv4Address in ipv4Addresses)
                    {
                        ipAddress = ipv4Address.ToString();
                    }
                }
            }
            return ipAddress;
        }
    }
}