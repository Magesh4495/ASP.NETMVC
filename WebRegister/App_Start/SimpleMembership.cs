using Entities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace WebRegister
{
    public static class SimpleMembership
    {
        public static void Initialize()
        {
            string ConnectionString = ConfigReader.GetDefaultConnectionString();
            string UserTableName = ConfigReader.GetConfigStringValue("UserTableName");
            string UserIdColumn = ConfigReader.GetConfigStringValue("UserIdColumn");
            string UserNameColumn = ConfigReader.GetConfigStringValue("UserNameColumn");
            bool AutoCreateTable = ConfigReader.GetConfigBoolValue("AutoCreateTable");
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DevDbName", UserTableName, UserIdColumn, UserNameColumn, AutoCreateTable);
        }
    }
}