using System.Data.SQLite;
using System.IO;
using NUnit.Framework;

namespace ChessMangler.TestHarness.Data
{
    public static class SetUp
    {
        public const string _testDir = @"..\..\..\TestHarness\Sandbox";
        public const string _testDB = @"..\..\..\TestHarness\Test Files\ChessMangler.db";

        public static string _sandboxDB = _testDir + @"\ChessMangler.db";

        public static SQLiteConnection _testConnection;

        public static void StartUp()
        {
            SetUp.DeleteFiles();

            //Pull down a fresh copy of our test database
            SetUp.CopyDB();

            //Connect to this fresh copy.
            _testConnection = new SQLiteConnection();
            _testConnection.ConnectionString = "Data Source=" + _sandboxDB + ";";
            _testConnection.Open();
        }

        public static void CopyDB()
        {
            //Copy Test File into sandbox directory
            File.Copy(SetUp._testDB, SetUp._sandboxDB);

            bool bExists = File.Exists(SetUp._sandboxDB);
            Assert.AreEqual(true, bExists, "unable to verify sandbox db file.  not at: " + SetUp._testDB);
        }

        public static void DeleteFiles()
        {
            //Delete Test DB File if its there when we start..
            if (File.Exists(_sandboxDB))
            {
                File.Delete(_sandboxDB);
            }
        }
    }
}




    //using (SQLiteConnection testReadConnection = new SQLiteConnection())
    //{
    //    testReadConnection.ConnectionString = "Data Source=" + SetUp._sandboxDB + ";";
    //    testReadConnection.Open();

    //    SQLiteCommand cmd = testReadConnection.CreateCommand();
    //    cmd.CommandText = "Select blah blah";

    //    SQLiteDataReader reader = cmd.ExecuteReader();
    //    Assert.IsTrue(reader.HasRows, "Expected to find at least 1 record in: " + cmd.CommandText);

    //    object retVal = reader.GetValue(0);
    //    Assert.AreEqual(1, retVal , "Expected 1 record ");

    //    testReadConnection.Close();
    //}

