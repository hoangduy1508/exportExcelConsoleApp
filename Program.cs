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
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "authors.xlsx";

            Console.WriteLine("Input Address city");
            var city = Console.ReadLine().ToString();
            var AddressDAL = new AddressDAL(_iconfiguration);
            var listAddressModel = AddressDAL.GetList(city);
            Console.WriteLine("Your city adress is:");

            listAddressModel.ForEach(item =>
            {
                Console.WriteLine($"Your address is: {item.AddressLine1}");
            });
            using (var wb = new ClosedXML.Excel.XLWorkbook())
            {


                var ws = wb.AddWorksheet("Duy");
                var currentRow = 1;
                ws.Cell(currentRow, 1).SetValue("Id");
                ws.Cell(currentRow, 2).SetValue("Address");
                ws.Cell(currentRow, 3).SetValue("City");

                foreach( var item in listAddressModel) 
                    {
                        currentRow++;
                        ws.Cell(currentRow, 1).SetValue(item.AddressId);
                        ws.Cell(currentRow, 2).SetValue(item.AddressLine1);
                        ws.Cell(currentRow, 3).SetValue(item.City);

                    }
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string savePath = Path.Combine(desktopPath, "test.xlsx");
                wb.SaveAs(savePath, false);


            }
                
           Console.WriteLine("Press any key to stop.");
            Console.ReadKey();
        }


    }
}