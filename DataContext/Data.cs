using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace hr_app.api.DataContext
{
    public class MySqlManager
    {
        private readonly string _connectionString;

        public MySqlManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> ExecuteQueryAsync(string query, IDictionary<string, object> ?parameters = null)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            using MySqlCommand command = new MySqlCommand(query, connection);

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
            }

            await connection.OpenAsync();

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<DataTable> GetDataTableAsync(string query, IDictionary<string, object> ?parameters = null)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using MySqlConnection connection = new MySqlConnection(_connectionString);
                using MySqlCommand command = new MySqlCommand(query, connection);

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }
                }

                await connection.OpenAsync();

                using DbDataReader reader = await command.ExecuteReaderAsync();
                dataTable.Load(reader);

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dataTable;
        }

        public async Task<int> LoginUserAsync(string username, string password)
        {
            int employeeId = -1;
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new MySqlCommand("loginUser", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password_p", password);
                        cmd.Parameters.Add("@employee_id", MySqlDbType.Int32);
                        cmd.Parameters["@employee_id"].Direction = ParameterDirection.Output;

                        await cmd.ExecuteNonQueryAsync();

                        employeeId = Convert.ToInt32(cmd.Parameters["@employee_id"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while logging in user {username}: {ex.Message}");
            }
            return employeeId;
        }




    }
}
