using PatientDataAdministration.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PatientDataAdministration.Web.Engines
{
    public class LongQuery: IDisposable
    {
        public bool SqlOperationNoResponse(string executionQuery)
        {
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Entities"].ConnectionString))
                {
                    var con = conn.ConnectionString;
                    conn.Open();
                    using (var command = new SqlCommand(executionQuery, conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return false;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
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