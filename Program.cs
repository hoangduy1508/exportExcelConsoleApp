using exportExcelConsoleApp.DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
namespace exportExcelConsoleApp
{
    class Program
    {
        private static IConfiguration _iconfiguration;
        static void Main(string[] args)
        {
            GetAppSettingsFile();
            PrintCountries();
        }
        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _iconfiguration = builder.Build();
        }
        static void PrintCountries()
        {
            Console.WriteLine("Input Address city");
            var city = Console.ReadLine().ToString();
            var AddressDAL = new AddressDAL(_iconfiguration);
            var listAddressModel = AddressDAL.GetList(city);
            Console.WriteLine("Your city adress is:");

            listAddressModel.ForEach(item =>
            {
                Console.WriteLine($"Your address is: {item.AddressLine1}");
            });
            Console.WriteLine("Press any key to stop.");
            Console.ReadKey();
        }
    }
}