using System.Net.NetworkInformation;
using Microsoft.Data.SqlClient;
using System.Net.Sockets;
using System.Net;

namespace SharedClass.Components.Data
{
    public class Connection
    {
        public readonly string connectionString;
        public static readonly string connectionStringPOS;

        static Connection()
        {
            string ipAddress = GetIPv4Addresses(); // Auto IPv4
            connectionStringPOS = $"Data Source={ipAddress},1433;Initial Catalog=EvsPointOfSale;User Id=Admin;Password=12345;Encrypt=False;";
        }
        public Connection()
        {
            string ipAddress = GetIPv4Addresses(); //Auto IPv4
            connectionString = $"Data Source= {ipAddress},1433;Initial Catalog=Computer;User Id=Admin;Password=12345;Encrypt=False;";
            //Auto IPv4
            //connectionString = $"Data Source=192.168.43.18,1433;Initial Catalog=Computer;User Id=Admin;Password=12345;Encrypt=False;"; //ye mera samsoong ka number hai
            //connectionString = $"Data Source=192.168.100.25,1433;Initial Catalog=Computer;User Id=Admin;Password=12345;Encrypt=False;"; //ye mera samsoong ka number hai
            //connectionString = $"Data Source=192.168.0.104,1433;Initial Catalog=Computer;User Id=Admin;Password=12345;Encrypt=False;"; //Manual IPv4
            //connectionString = $"Data Source=DESKTOP-M62686B\\SQLEXPRESS;Initial Catalog=Computer;User Id=Admin;Password=12345;Encrypt=False;"; //Sufiyan Local
        }

        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(connectionString);
        }

        public SqlConnection GetPOSSqlConnection()
        {
            return new SqlConnection(connectionStringPOS);
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