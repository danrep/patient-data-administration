using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PatientDataAdministration.Data.InterchangeModels;

namespace PatientDataAdministration.Client
{
    public static class LocalCore
    {
        private static LocalPDAEntities _pdaEntities = new LocalPDAEntities();
        private static int _userCredentialId = 0;

        public static DialogResult TreatError(Exception exception, int userCredentialId, bool isSilent = false)
        {
            LocalCore._userCredentialId = userCredentialId;
            return isSilent ? MessageBox.Show(exception.Message + @" " + exception.InnerException?.Message) : DialogResult.OK;
        }

        private static void TreatError(Exception exception)
        {
            try
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
            }
            catch (Exception)
            {
                throw;
            }
        }

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
                                x => x.SettingKey == (int) EnumLibrary.SettingKey.RemoteApi)?.SettingValue ?? "");

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
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
                var request = (HttpWebRequest)WebRequest.Create((_pdaEntities.System_Setting.FirstOrDefault(
                            x => x.SettingKey == (int)EnumLibrary.SettingKey.RemoteApi)?.SettingValue ?? "") + url);

                request.Method = "POST";
                request.Credentials = CredentialCache.DefaultCredentials;
                ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";

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
                    CreateDatabase(dbName, dbFileName);
                }

                // If the database does not already exist, create it.
                if (!File.Exists(dbFileName))
                {
                    CreateDatabase(dbName, dbFileName);
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

        private static void CreateDatabase(string dbName, string dbFileName)
        {
            try
            {
                var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True";
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var installCmd = connection.CreateCommand();
                    installCmd.CommandText = ScriptClean(Properties.Resources.DatabaseInstallScript);
                    installCmd.ExecuteNonQuery();

                    DetachDatabase(dbName);

                    var migrateCmd = connection.CreateCommand();
                    migrateCmd.CommandText = ScriptClean(Properties.Resources.DatabaseMigrateScript);
                    migrateCmd.ExecuteNonQuery();
                }

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
                var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True";
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
    }
}
