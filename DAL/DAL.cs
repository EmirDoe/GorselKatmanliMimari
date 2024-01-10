using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace DAL
{
    public class DAL
    {
        // connection string
        private readonly string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb;Persist Security Info=False;";

        public DataTable ExecuteQuery(string query)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                DataTable dataTable = new DataTable();
                try
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Query Error: " + ex.Message);
                }

                return dataTable;
            }
        }

        public int ExecuteNonQuery(string query)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                int affectedRows = 0;
                try
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        affectedRows = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Query Error: " + ex.Message);
                }

                return affectedRows;
            }
        }

    }
}
