using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;

namespace PatientDataAdministration.ClientUpdater
{
    class Program 
    {
        static int _retries = 0;
        private static System_Update _systemUpdate = new System_Update();
        private static string _storeLocation;
        private static string _url = "";

        static void Main(string[] args)
        {
            _storeLocation = string.Empty;

            if (args.Length > 0)
                _url = args[0];

            _storeLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DownloadedUpdate");

            if (string.IsNullOrEmpty(_url))
                _url = "http://pbs.apin.org.ng";

            if (_url == "U")
            {
                var updateInfo = $@"{_storeLocation}\data.txt";
                if (!File.Exists(updateInfo))
                    return;

                var updateData = File.ReadAllText(updateInfo);
                var updateDataResolved = Newtonsoft.Json.JsonConvert.DeserializeObject<System_Update>(updateData);

                Console.WriteLine(
                    $@"Hello there. This is an automated process. Please do not interrupt or interfere. APIN PDA will resume shortly.");
                Console.WriteLine($@"Applying Update Version {updateDataResolved.VersionNumber}.");

                if (updateDataResolved.IsDbRefreshRequired)
                {
                    try
                    {
                        var dbName = "LocalPDA";
                        var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True";
                        using (var connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            var cmd = connection.CreateCommand();
                            cmd.CommandText = $"exec sp_detach_db {dbName}";
                            cmd.ExecuteNonQuery();
                        }

                        var outputFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                        var mdfFilename = dbName + ".mdf";
                        var dbFileName = Path.Combine(outputFolder, mdfFilename);
                        var logFileName = Path.Combine(outputFolder, $"{dbName}_log.ldf");

                        if (File.Exists(logFileName))
                        {
                            File.Delete(logFileName);
                            File.Delete(dbFileName);
                        }
                    }
                    catch
                    {
                        //
                    }
                }

                var files = Directory.EnumerateFiles(_storeLocation).Where(x => !x.Contains(".sql")).ToList();
                var i = 1.0;

                foreach (var file in files)
                {
                    try
                    {
                        var fileInfo = new FileInfo(file);
                        Console.WriteLine($@"Installing {fileInfo.Name} {fileInfo.Length} bytes ...");
                        var destination = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, fileInfo.Name);

                        if (File.Exists(destination))
                            File.Delete(destination);

                        File.Move(file, destination);
                    }
                    catch
                    {
                        //
                    }

                    Console.WriteLine($@"{(i / files.Count) * 100:00}%");
                    i++;
                }

                Console.WriteLine(@"100%");
                Console.WriteLine(@"APIN PDA will be started shortly.");
                Process.Start("PatientDataAdministration.Client.exe");

                Environment.Exit(0);
            }
            else
            {
                while (!ConfirmStoreLocation())
                {
                    CheckPersistence();
                }

                _retries = 0;
                while (!CheckUpdateDetails(_url))
                {
                    CheckPersistence();
                }

                _retries = 0;
                while (!ExecutePull())
                {
                    CheckPersistence();
                }

                File.WriteAllText($@"{_storeLocation}/data.txt",
                    Newtonsoft.Json.JsonConvert.SerializeObject(_systemUpdate));
            }
        }

        static void CheckPersistence()
        {
            if (_retries < 5)
            {
                _retries++;
                return;
            }

            Environment.Exit(0);
        }

        static bool ConfirmStoreLocation()
        {
            try
            {
                if (!Directory.Exists(_storeLocation))
                    Directory.CreateDirectory(_storeLocation);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        static bool CheckUpdateDetails(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync(url + $@"/ClientCommunication/Misc/GetUpdateData").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var json = response.Content.ReadAsStringAsync().Result;
                        var responseData = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseData>(json);

                        if (!responseData.Status)
                        {
                            Console.WriteLine(responseData.Message);
                            return false;
                        }
                        else
                        {
                            if (responseData.Data == null)
                            {
                                Console.WriteLine("No Updates Found");
                                Environment.Exit(0);
                            }
                            else
                            {
                                _systemUpdate =
                                    Newtonsoft.Json.JsonConvert.DeserializeObject<System_Update>(
                                        Newtonsoft.Json.JsonConvert.SerializeObject(responseData.Data));

                                Console.WriteLine(
                                    $"APIN PDA Client Update Utility. Do not close this application until there is an error or it's task is completed successfully.");
                                Console.WriteLine($"Found New Update >> {_systemUpdate.VersionNumber}");
                                return true;
                            }

                            return false;
                        }
                    }
                    else
                        return false;
                }
            }
            catch (Exception e)
            {
                WriteException(e);
                return false;
            }
        }

        static void WriteException(Exception exception)
        {
            Console.WriteLine($"\n\n***");
            Console.WriteLine($@"Error: {exception.Message}");
            
            if (exception.InnerException != null)
                Console.WriteLine($@"Error Detail: {exception.InnerException.Message}");
        }

        static bool ExecutePull()
        {
            try
            {
                var remoteLocation = $@"{_systemUpdate.ServerLocation}/{_systemUpdate.FolderLocation}/{_systemUpdate.VersionNumber}";
                var ftpRequest =
                    (FtpWebRequest)WebRequest.Create(remoteLocation);
                ftpRequest.Credentials = new NetworkCredential(_systemUpdate.ServerUsername, _systemUpdate.ServerPassword);
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                ftpRequest.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback += ValidateServerCertificate;

                var response = (FtpWebResponse)ftpRequest.GetResponse();
                var streamReader = new StreamReader(response.GetResponseStream());
                var files = new List<string>();

                var line = streamReader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    files.Add(line);
                    line = streamReader.ReadLine();
                }
                streamReader.Close();

                Console.WriteLine($@"Downloading Update.");

                using (var ftpClient = new WebClient())
                {
                    ftpClient.Credentials = new NetworkCredential(_systemUpdate.ServerUsername, _systemUpdate.ServerPassword);
                    
                    ServicePointManager.ServerCertificateValidationCallback += ValidateServerCertificate;
                    ftpClient.DownloadProgressChanged += DownloadProgressChanged;
                    ftpClient.DownloadDataCompleted += DownloadDataCompleted;

                    foreach (var file in files)
                    {

                        while (ftpClient.IsBusy) { }

                        Console.WriteLine($"\nCurrent File: {file}.");

                        var path = $@"{remoteLocation}/{file}";
                        var trnsfrpth = $@"{_storeLocation}\\{file}";
                        
                        ftpClient.DownloadFileAsync(new Uri(path), trnsfrpth);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                WriteException(e);
                return false;
            }
        }

        static void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var bytesIn = double.Parse(e.BytesReceived.ToString());
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write("Downloaded " + bytesIn + " bytes so far");
        }

        static void DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            Console.Write("Success");
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
