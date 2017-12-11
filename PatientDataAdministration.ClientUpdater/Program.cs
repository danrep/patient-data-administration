using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;

namespace PatientDataAdministration.ClientUpdater
{
    class Program
    {
        static int _retries = 0;
        private static System_Update systemUpdate = new System_Update();

        static void Main(string[] args)
        {
            if (args.Length == 0)
                return;

            Console.WriteLine("PDA Client Update is Running");
            Console.WriteLine("You dont need to do anything. I will let you know when I am done.");

            while (!CheckConnection(args[0]))
            {
                CheckPersistence();
            }

            while (!ExecutePull())
            {
                CheckPersistence();
            }
        }

        static void CheckPersistence()
        {
            if (_retries < 5)
            {
                Console.WriteLine("Retrying Last Activity");
                _retries++;
                return;
            }

            Console.WriteLine("Maximum amout of Retries Reached.");
            Console.WriteLine("The Update will be retried Later.");
            Environment.Exit(0);
        }

        static bool CheckConnection(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var json = response.Content.ReadAsStringAsync().Result;
                        var responseData = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseData>(json);

                        if (!responseData.Status)
                        {
                            Console.WriteLine(responseData.Message);
                            return false;
                        }

                        systemUpdate =
                            Newtonsoft.Json.JsonConvert.DeserializeObject<System_Update>(
                                Newtonsoft.Json.JsonConvert.SerializeObject(responseData.Data));

                        if (systemUpdate == null)
                        {
                            Console.WriteLine("No Updates Found");
                            Environment.Exit(0);
                        }

                        Console.WriteLine("Found New Update");
                        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(responseData.Data));
                        return true;
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
                var request =
                    (FtpWebRequest) WebRequest.Create(systemUpdate.ServerLocation + "//" + systemUpdate.FolderLocation);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.KeepAlive = true;
                request.UsePassive = false;
                request.UseBinary = true;

                request.Credentials = new NetworkCredential(systemUpdate.ServerUsername, systemUpdate.ServerPassword);

                var response = (FtpWebResponse)request.GetResponse();

                var responseStream = response.GetResponseStream();
                var reader = new StreamReader(responseStream);

                using (var writer = new FileStream(systemUpdate.VersionNumber, FileMode.Create))
                {
                    var length = response.ContentLength;
                    var bufferSize = 2048;
                    int readCount;
                    var buffer = new byte[2048];

                    readCount = responseStream.Read(buffer, 0, bufferSize);
                    while (readCount > 0)
                    {
                        writer.Write(buffer, 0, readCount);
                        readCount = responseStream.Read(buffer, 0, bufferSize);
                    }
                }

                reader.Close();
                response.Close();
                return true;
            }
            catch (Exception e)
            {
                WriteException(e);
                return false;
            }
        }
    }
}
