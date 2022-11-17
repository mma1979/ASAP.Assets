namespace ASAP.Assets.Resources
{
    public static class SqlStatements
    {
        public const string InsertStatement =
            "if not exists(select * from Assets where AssetNo=@number) Insert into Assets (AssetNo, AssetName, ModelNO, Vendor, Description) values (@number, @name, @model, @vendor,@description)";

        public const string SelectAllStatement = "Select * from Assets";
        public const string SelectByIdStatement = "Select * from Assets where Id=@id";
        public const string UpdateStatement = "Update Assets Set AssetNo=@number, AssetName=@name, ModelNO=@model, Vendor=@vendor, Description=@description where Id=@id";
        public const string DeleteStatement = "Delete from Assets  where Id=@id";
    }
}
