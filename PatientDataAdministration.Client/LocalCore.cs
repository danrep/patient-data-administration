using System;
using System.Data.SqlClient;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PatientDataAdministration.Client.LocalSettingStorage;
using PatientDataAdministration.Data.InterchangeModels;

namespace PatientDataAdministration.Client
{
    public static class LocalCore
    {
        private static LocalPDAEntities _pdaEntities = new LocalPDAEntities();
        private static int _userCredentialId = 0;

        public static DialogResult TreatError(Exception exception, int userCredentialId, bool canDisplayError = false)
        {
            LocalCore._userCredentialId = userCredentialId;
            TreatError(exception);
            return canDisplayError
                ? MessageBox.Show(exception.Message + @" " + exception.InnerException?.Message)
                : DialogResult.OK;
        }

        private static void TreatError(Exception exception)
        {
            _pdaEntities.System_ErrorLog.Add(new System_ErrorLog()
            {
                IsDeleted = false,
                ErrorDate = DateTime.Now,
                ErrorMessage = exception.Message,
                ErrorString = exception.ToString(),
                SyncStatus = false,
                UserId = _userCredentialId
            });

            if (exception.InnerException != null)
                _pdaEntities.System_ErrorLog.Add(new System_ErrorLog()
                {
                    IsDeleted = false,
                    ErrorDate = DateTime.Now,
                    ErrorMessage = exception.InnerException.Message,
                    ErrorString = exception.InnerException.ToString(),
                    SyncStatus = false,
                    UserId = _userCredentialId
                });

            _pdaEntities.SaveChanges();
        }

        #region Remote Processors

        public static async Task<ResponseData> Get(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    _pdaEntities = new LocalPDAEntities();

                    client.BaseAddress =
                        new Uri(
                            _pdaEntities.System_Setting.FirstOrDefault(
                                x => x.SettingKey == (int) EnumLibrary.SyncMode.RemoteApi)?.SettingValue ?? "");

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("APIN_AUTH_TOKEN", LocalCache.Get<string>("ClientId"));

                    var response = client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseData>(json);
                    }
                    else
                        return new ResponseData
                        {
                            Message = "Bad Response",
                            Status = false
                        };
                }
            }
            catch (Exception e)
            {
                TreatError(e);
                return new ResponseData
                {
                    Message = e.Message,
                    Status = false
                };
            }
        }

        public static ResponseData Post(string url, string payload)
        {
            try
            {
                _pdaEntities = new LocalPDAEntities();
                var request = (HttpWebRequest) WebRequest.Create(
                    (_pdaEntities.System_Setting
                         .FirstOrDefault(x => x.SettingKey == (int) EnumLibrary.SyncMode.RemoteApi)?.SettingValue ??
                     "") + url);

                request.Method = "POST";
                request.Credentials = CredentialCache.DefaultCredentials;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";

                var byteArray = Encoding.UTF8.GetBytes(payload);

                request.ContentType = "application/json; charset=utf-8";
                request.ContentLength = byteArray.Length;
                request.Headers.Add("APIN_AUTH_TOKEN", LocalCache.Get<string>("ClientId"));

                var dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                var response = request.GetResponse();
                dataStream = response.GetResponseStream();
                var reader = new StreamReader(dataStream);
                var responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();

                return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseData>(responseFromServer);
            }
            catch (Exception e)
            {
                TreatError(e);
                return new ResponseData
                {
                    Message = e.Message,
                    Status = false
                };
            }
        }

        public static bool PostLocal(string url, string payload)
        {
            try
            {
                _pdaEntities = new LocalPDAEntities();
                var request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.Credentials = CredentialCache.DefaultCredentials;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";

                var byteArray = Encoding.UTF8.GetBytes(payload);

                request.ContentType = "application/json; charset=utf-8";
                request.ContentLength = byteArray.Length;

                var dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                var response = request.GetResponse();
                dataStream = response.GetResponseStream();

                var reader = new StreamReader(dataStream);
                var responseFromServer = reader.ReadToEnd();

                reader.Close();
                dataStream.Close();
                response.Close();

                return responseFromServer.Contains("200");
            }
            catch (Exception e)
            {
                TreatError(e);
                return false;
            }
        }

        #endregion

        #region Database Management

        public static SqlConnection GetLocalDb(string dbName, bool deleteIfExists = false)
        {
            try
            {
                var outputFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                var mdfFilename = dbName + ".mdf";
                var dbFileName = Path.Combine(outputFolder, mdfFilename);
                var logFileName = Path.Combine(outputFolder, $"{dbName}_log.ldf");

                // If the file exists, and we want to delete old data, remove it here and create a new database.
                if (File.Exists(dbFileName) && deleteIfExists)
                {
                    if (File.Exists(logFileName))
                    {
                        File.Delete(logFileName);
                        File.Delete(dbFileName);
                    }

                    CreatePrimaryDatabase(dbName, dbFileName);
                }

                // If the database does not already exist, create it.
                if (!File.Exists(dbFileName))
                {
                    DropDatabase(dbFileName);
                    CreatePrimaryDatabase(dbName, dbFileName);
                }

                // Open newly created, or old database.
                var connection = new SqlConnection(Properties.Settings.Default.LocalPDAConnectionString);
                connection.Open();
                return connection;
            }
            catch
            {
                throw;
            }
        }

        private static void CreatePrimaryDatabase(string dbName, string dbFileName)
        {
            try
            {
                RunDatabaseSript(dbName, Properties.Resources.DatabaseInstallScript);

                RunDatabaseSript(dbName, Properties.Resources.DatabaseMigrateScript);

                if (File.Exists(dbFileName))
                    return;
                else
                    return;
            }
            catch
            {
                throw;
            }
        }

        public static void RunDatabaseSript(string dbName, string scriptText)
        {
            try
            {
                const string connectionString =
                    @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True";

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    DetachDatabase(dbName);

                    var migrateCmd = connection.CreateCommand();
                    migrateCmd.CommandText = ScriptClean(scriptText);
                    migrateCmd.ExecuteNonQuery();
                }
            }
            catch
            {
                //
            }
        }

        private static string ScriptClean(string sqlScript)
        {
            sqlScript = sqlScript.Replace("GO", "");
            sqlScript = Regex.Replace(sqlScript, "([/*][*]).*([*][/])", "");
            sqlScript = Regex.Replace(sqlScript, "\\s{2,}", " ");
            sqlScript = sqlScript.Replace("{dbLocation}",
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));

            return sqlScript;
        }

        private static void DetachDatabase(string dbName)
        {
            try
            {
                var connectionString =
                    @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True";
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = $"exec sp_detach_db '{dbName}'";
                    cmd.ExecuteNonQuery();

                    return;
                }
            }
            catch
            {
                return;
            }
        }

        private static void DropDatabase(string dbName)
        {
            try
            {
                var connectionString =
                    @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True";
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = $"drop database [{dbName}]";
                    cmd.ExecuteNonQuery();

                    return;
                }
            }
            catch
            {
                return;
            }
        }

        #endregion

        public static GeoCoordinate GetLocationProperty()
        {
            var watcher = new GeoCoordinateWatcher();
            watcher.TryStart(true, TimeSpan.FromMilliseconds(1));
            watcher.Start();

            var coord = watcher;
            return watcher.Position.Location.IsUnknown == false ? coord.Position.Location : null;
        }
    }
}
