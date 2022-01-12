using Microsoft.Extensions.Configuration;
using System;

namespace HenHoCore.Ulti
{
    public class EnviConfig
    {
        public static string ConnectionString { get; private set; }
        public static string SecretKey = "7jqx9ffrd97e57h4iteeajdkm4073qqxk5";
        public static int ExpirationInMinutes = 14400;
        public static string DailyToken = "7jqx9ffrd97e57h4iteeajdkm4073qqxk5";

        public static void Config(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
