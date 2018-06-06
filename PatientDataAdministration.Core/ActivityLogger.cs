using System;
using System.IO;

namespace PatientDataAdministration.Core
{
    public static class ActivityLogger
    {
        public static void Log(Exception exception)
        {
            Log("ERROR>>" + exception.Source, "[" + exception + "]" + exception.Message);
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
                var location = System.Web.Hosting.HostingEnvironment.MapPath("~/logs");

                var dirInfo = new DirectoryInfo(location);
                if (!dirInfo.Exists)
                {
                    Directory.CreateDirectory(location);
                }

                location = location + "/";

                if (!File.Exists(location + "PDA_Web_Logs.txt"))
                {
                    using (var sw = File.CreateText(location + "PDA_Web_Logs.txt"))
                    {
                        sw.WriteLine("[" + messageType + "] " + DateTime.Now + ": " + message);
                        sw.Close();
                    }
                }
                else
                    using (var sw = File.AppendText(location + "PDA_Web_Logs.txt"))
                    {
                        var fI = new FileInfo(location + "PDA_Web_Logs.txt");
                        if (fI.Length <= Setting.LogRollOver)
                        {
                            sw.WriteLine("[" + messageType + "] " + DateTime.Now + ": " + message);
                            sw.Close();
                        }
                        else
                        {
                            sw.Close();
                            File.Move(location + "PDA_Web_Logs.txt", location + "PDA_Web_Logs_" + DateTime.Now.ToString("yyyymmddhhMMsstt"));

                            using (var sw3 = File.CreateText(location + "PDA_Web_Logs.txt"))
                            {
                                sw3.WriteLine("[" + messageType + "] " + DateTime.Now + ": " + message);
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
    }
}
