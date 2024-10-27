using SQLite;

namespace SQLITEDemo
{
    public static class Constants
    {
        private const string DBFileName = "SQLite.db1";

        public const SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite |
                                             SQLiteOpenFlags.Create |
                                             SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                return Path.Combine(FileSystem.AppDataDirectory, DBFileName);
            }
        }
    }
}