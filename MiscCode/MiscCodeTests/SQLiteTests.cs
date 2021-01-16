using System;
using System.Threading.Tasks;
using MiscCodeTests.SQLite;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class SQLiteTests
    {
        [Test]
        public async Task CreateWriteAndRead()
        {
            var sql = new SQLiteAsyncConnection("../../TestDb.db");


            await sql.CreateTableAsync<db_KeyValue>();

            await sql.InsertOrReplaceAsync(new db_KeyValue {Key = "KEY1", Value = "value 1"});


            var all = await sql.QueryAsync<db_KeyValue>("SELECT * FROM db_KeyValue");
            foreach (var e in all)
            {
                Console.WriteLine(e.Key + "=" + e.Value);
            }
        }

        
    }
    public class db_KeyValue
    {
        [PrimaryKey]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}