using System;
using System.Data.SQLite;

namespace GameServer.DataAccess
{
    public class SqLiteBaseRepository
    {
        public static string DbFile
        {
            get { return Environment.CurrentDirectory + "\\GameDB.sqlite"; }
        }

        public static SQLiteConnection SQLiteDBConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }
    }
    
}