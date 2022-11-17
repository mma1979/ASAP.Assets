using ASAP.Assets.Core;
using ASAP.Assets.Resources;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;

namespace ASAP.Assets.Service
{
    public class GoogleSheetsService
    {
        public List<Asset> ExportFromSheet()
        {
            var assets = ReadSheet();

            var connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (var conn = new SqlConnection(connStr))
            {
                foreach (var asset in assets)
                {
                    using (var cmd = new SqlCommand(SqlStatements.InsertStatement, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@number", asset.AssetNo);
                        cmd.Parameters.AddWithValue("@name", asset.AssetName);
                        cmd.Parameters.AddWithValue("@model", asset.ModelNO);
                        cmd.Parameters.AddWithValue("@vendor", asset.Vendor);
                        cmd.Parameters.AddWithValue("@description", asset.Description);

                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.ExecuteNonQuery();

                    }
                }

                conn.Close();
            }

            return assets;
        }
        private List<Asset> ReadSheet()
        {
            HttpClient client = new HttpClient();

            var response = client.GetStringAsync(new Uri("https://sheetdb.io/api/v1/l6yd31tvd3tsu")).Result;
            if (string.IsNullOrEmpty(response)) return default;


            var list = JsonConvert.DeserializeObject<List<Asset>>(response);
            return list;
        }


    }
}
