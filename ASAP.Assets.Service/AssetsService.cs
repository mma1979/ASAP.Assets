using ASAP.Assets.Core;
using ASAP.Assets.Resources;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ASAP.Assets.Service
{
    public class AssetsService : IDisposable
    {
        private SqlConnection connection;

        public AssetsService()
        {
            connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public List<Asset> GetAll()
        {
            var table = new DataTable();
            using (var adapter = new SqlDataAdapter(SqlStatements.SelectAllStatement, connection))
            {

                adapter.Fill(table);
            }

            var data = table.AsEnumerable()
                .Select(r => new Asset
                {
                    Id = r.Field<int>(nameof(Asset.Id)),
                    AssetNo = r.Field<int>(nameof(Asset.AssetNo)),
                    AssetName = r.Field<string>(nameof(Asset.AssetName)),
                    ModelNO = r.Field<string>(nameof(Asset.ModelNO)),
                    Vendor = r.Field<string>(nameof(Asset.Vendor)),
                    Description = r.Field<string>(nameof(Asset.Description)),

                }).ToList();

            return data;
        }

        public Asset GetById(int id)
        {
            var table = new DataTable();
            using (var cmd = new SqlCommand(SqlStatements.SelectAllStatement, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (var adapter = new SqlDataAdapter(cmd))
                {

                    adapter.Fill(table);
                }
            }

            var data = table.AsEnumerable()
                .Select(r => new Asset
                {
                    Id = r.Field<int>(nameof(Asset.Id)),
                    AssetNo = r.Field<int>(nameof(Asset.AssetNo)),
                    AssetName = r.Field<string>(nameof(Asset.AssetName)),
                    ModelNO = r.Field<string>(nameof(Asset.ModelNO)),
                    Vendor = r.Field<string>(nameof(Asset.Vendor)),
                    Description = r.Field<string>(nameof(Asset.Description)),

                }).FirstOrDefault();

            return data;
        }

        public bool Insert(Asset asset)
        {
            int saved = 0;
            using (var cmd = new SqlCommand(SqlStatements.InsertStatement, connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@number", asset.AssetNo);
                cmd.Parameters.AddWithValue("@name", asset.AssetName);
                cmd.Parameters.AddWithValue("@model", asset.ModelNO);
                cmd.Parameters.AddWithValue("@vendor", asset.Vendor);
                cmd.Parameters.AddWithValue("@description", asset.Description);

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                saved = cmd.ExecuteNonQuery();

            }

            return saved > 0;
        }

        public bool Update(Asset asset)
        {
            int updated = 0;
            using (var cmd = new SqlCommand(SqlStatements.UpdateStatement, connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", asset.Id);
                cmd.Parameters.AddWithValue("@number", asset.AssetNo);
                cmd.Parameters.AddWithValue("@name", asset.AssetName);
                cmd.Parameters.AddWithValue("@model", asset.ModelNO);
                cmd.Parameters.AddWithValue("@vendor", asset.Vendor);
                cmd.Parameters.AddWithValue("@description", asset.Description);

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                updated = cmd.ExecuteNonQuery();

            }

            return updated > 0;
        }

        public bool Delete(int id)
        {
            int deleted = 0;
            using (var cmd = new SqlCommand(SqlStatements.DeleteStatement, connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                deleted = cmd.ExecuteNonQuery();

            }

            return deleted > 0;
        }

        public void Dispose()
        {
            connection?.Dispose();
        }
    }
}
