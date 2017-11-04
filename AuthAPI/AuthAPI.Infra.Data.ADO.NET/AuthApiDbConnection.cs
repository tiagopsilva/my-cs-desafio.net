using System;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace AuthAPI.Infra.Data.ADO.NET
{
    public class AuthApiDbConnection : IDisposable
    {
        private FbConnection _connection;

        public AuthApiDbConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["AuthAPIConnectionString"].ConnectionString;
            _connection = new FbConnection(connectionString);
            Open();
        }

        public void Open()
        {
            if (_connection.State == ConnectionState.Closed || _connection.State == ConnectionState.Broken)
                _connection.Open();
        }

        public void Close()
        {
            if (_connection.State != ConnectionState.Closed || _connection.State != ConnectionState.Broken)
                _connection.Close();
        }

        public FbCommand CreateCommand(string sql, params FbParameter[] parameters)
        {
            var cmd = new FbCommand(sql);
            try
            {
                cmd.Connection = _connection;

                foreach (var parameter in parameters ?? new object[] { })
                    cmd.Parameters.Add(parameter);

                return cmd;
            }
            catch
            {
                cmd.Dispose();
                throw;
            }
        }

        public async Task<int> ExecuteCountAsync(string sql, params FbParameter[] parameters)
        {
            using (var cmd = CreateCommand(sql))
            {
                foreach (var parameter in parameters ?? new object[] { })
                    cmd.Parameters.Add(parameter);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    reader.Read();
                    return reader.GetInt32(0);
                }
            }
        }

        public Task<int> ExecuteCommandAsync(string sql, params FbParameter[] parameters)
        {
            using (var cmd = CreateCommand(sql))
            {
                foreach (var parameter in parameters ?? new object[] { })
                    cmd.Parameters.Add(parameter);

                return cmd.ExecuteNonQueryAsync();
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _connection.Close();
                    _connection = null;
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}