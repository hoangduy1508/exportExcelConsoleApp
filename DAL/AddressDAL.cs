using exportExcelConsoleApp.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace exportExcelConsoleApp.DAL
{
    class AddressDAL
    {
        private string _connectionString;
        public AddressDAL(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("Default");
        }
        public List<AddressModel> GetList(string cityName)
        {
            var listAddressModel = new List<AddressModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_get_address", con);
                    cmd.Parameters.Add("@city", SqlDbType.VarChar, -1).Value = cityName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        listAddressModel.Add(new AddressModel
                        {
                            AddressId = Convert.ToInt32(rdr[0]),
                            AddressLine1 = rdr[1].ToString(),
                            City = rdr[2].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listAddressModel;
        }
    
}
}
