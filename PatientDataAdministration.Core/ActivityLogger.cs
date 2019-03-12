using System;
using System.IO;

namespace PatientDataAdministration.Core
{
    public static class ActivityLogger
    {
        public static string LogFilePath => System.Web.Hosting.HostingEnvironment.MapPath("~/logs") + "/";

        public static string LogFileName { get; set; }

        public static void Log(Exception exception)
        {
            Log("ERROR>>" + exception.Source, "[" + exception + "]" + exception.Message);

            EntityValidationErrorLog(exception);

            if (exception.InnerException == null)
                return;

            Log("ERROR>>" + exception.InnerException.Source,
                "[" + exception.InnerException + "]" + exception.InnerException.Message);

            EntityValidationErrorLog(exception.InnerException);
        }

        public static void Log(string messageType, string message)
        {
            while (!LogEngine(messageType, message.Length >= 2000 ? message.Substring(0, 2000) : message))
            { }
        }

        private static bool LogEngine(string messageType, string message)
        {
            try
            {
                var location = LogFilePath;
                if (LogFilePath == "/")
                {
                    location = Path.Combine(
                                   Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location),
                                   "Logs") + "/";
                }

                var dirInfo = new DirectoryInfo(location);
                if (!dirInfo.Exists)
                {
                    Directory.CreateDirectory(location);
                }

                if (!File.Exists(location + LogFileName))
                {
                    using (var sw = File.CreateText(location + LogFileName))
                    {
                        sw.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(new Logger()
                        {
                            TimeStamp = DateTime.Now,
                            MessageType = messageType,
                            Message = message
                        }));
                        sw.Close();
                    }
                }
                else
                    using (var sw = File.AppendText(location + LogFileName))
                    {
                        var fI = new FileInfo(location + LogFileName);
                        if (fI.Length <= Setting.LogRollOver)
                        {
                            sw.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(new Logger()
                            {
                                TimeStamp = DateTime.Now,
                                MessageType = messageType,
                                Message = message
                            }));
                            sw.Close();
                        }
                        else
                        {
                            sw.Close();
                            File.Move(location + LogFileName,
                                location + LogFileName.Replace(".txt", "").Trim() + "_" +
                                DateTime.Now.ToString("yyyymmddhhMMsstt") + ".txt");

                            using (var sw3 = File.CreateText(location + LogFileName))
                            {
                                sw3.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(new Logger()
                                {
                                    TimeStamp = DateTime.Now,
                                    MessageType = messageType,
                                    Message = message
                                }));
                                sw3.Close();
                            }
                        }
                    }
            }
            catch (IOException)
            {
                return false;
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        private static void EntityValidationErrorLog(Exception e)
        {
            try
            {
                var trackingGuid = Guid.NewGuid().ToString().ToUpper();

                var entitiyException = (System.Data.Entity.Validation.DbEntityValidationException)e;
                foreach (var eve in entitiyException.EntityValidationErrors)
                {
                    Log($"DB_ERROR_NODE: {trackingGuid}", Newtonsoft.Json.JsonConvert.SerializeObject(eve));

                    foreach (var ve in eve.ValidationErrors)
                        Log($"DB_ERROR_ITEM: {trackingGuid}", Newtonsoft.Json.JsonConvert.SerializeObject(ve));
                }
            }
            catch
            {

            }
        }
    }

    public class Logger
    {
        public DateTime TimeStamp { get; set; }
        public string MessageType { get; set; }
        public string Message { get; set; }
        public string Date => TimeStamp.ToString("yy/MM/dd hh:mm:ss tt");
    }
}
