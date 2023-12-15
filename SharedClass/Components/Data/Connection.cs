using Microsoft.Data.SqlClient;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace SharedClass.Components.Data
{
    public class Connection
    {
        private readonly string connectionString;

        public Connection()
        {
            //string ipAddress = GetIPv4Addresses();
            connectionString = $"Data Source=192.168.100.97,1433;Initial Catalog=Example;User Id=Ghazi;Password=fC5y2qRU;Encrypt=False;";
            //connectionString = $"Data Source={ipAddress},1433;Initial Catalog=Example;User Id=Ghazi;Password=fC5y2qRU;Encrypt=False;";
        }
        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(connectionString);
        }
        //private static string GetIPv4Addresses()
        //{
        //    string ipAddress = "";
        //    // Get all network interfaces on the machine
        //    NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

        //    foreach (NetworkInterface networkInterface in networkInterfaces)
        //    {
        //        // Filter out loopback and non-operational interfaces
        //        if (networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback
        //            && networkInterface.OperationalStatus == OperationalStatus.Up)
        //        {
        //            // Get the IP properties of the current interface
        //            IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();

        //            // Filter out IPv6 addresses and get the IPv4 addresses
        //            IPAddress[] ipv4Addresses = ipProperties
        //                .UnicastAddresses
        //                .Where(addr => addr.Address.AddressFamily == AddressFamily.InterNetwork)
        //                .Select(addr => addr.Address)
        //                .ToArray();

        //            Console.WriteLine($"Interface: {networkInterface.Description}");
        //            Console.WriteLine("IPv4 Addresses:");

        //            foreach (IPAddress ipv4Address in ipv4Addresses)
        //            {
        //                ipAddress = ipv4Address.ToString();
        //            }
        //        }

        //    }
        //    return ipAddress;
        //}
    }
}