using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDataWriter
{
    public class TransactionsDAO
    {
        private string ConnectionString { get; set; }

        public TransactionsDAO(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public bool WriteTenThousandRows()
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
                {
                    con.Open();
                    var transaction = con.BeginTransaction();
                    ExecuteTenThousandInserts(con);
                    transaction.Commit();
                    con.Close();
                    con.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
        }

        private void ExecuteTenThousandInserts(SQLiteConnection con)
        {
            var totalRows = CountTotalRows(con);
            var limit = totalRows + 10000;
            for (int i = totalRows; i <= limit; i++)
            {
                var command = CreateCommand("INSERT INTO transactions VALUES (@id, @datahora, @transactionName)");

                command.Parameters.AddWithValue("id", i);
                command.Parameters.AddWithValue("datahora", DateTime.Now);
                command.Parameters.AddWithValue("transactionName", $"Inserted row {i}");
                command.Connection = con;

                command.ExecuteNonQuery();
                command.Dispose();
            }

        }

        private int CountTotalRows(SQLiteConnection con)
        {
            int totalRows = 0;
            var command = CreateCommand("select Count(*) from transactions");
            command.Connection = con;
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                totalRows = Convert.ToInt32(reader["Count(*)"]);
            }
            return totalRows;
        }

        private SQLiteCommand CreateCommand(string commandString)
        {
            var command = new SQLiteCommand();
            command.CommandText = commandString;
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }

    }
}
