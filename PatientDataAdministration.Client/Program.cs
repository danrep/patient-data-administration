using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PatientDataAdministration.Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            UpdateSelf();
            UpdateRegistry();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Splash());
        }

        private static void UpdateSelf()
        {
            try
            {
                var location = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DownloadedUpdate");

                if (!Directory.Exists(location))
                    return;

                // Update Pendings
                var storeLocations = new DirectoryInfo(location).EnumerateFiles().Where(x => x.Name.Contains(".exe") || x.Name.Contains(".dll"));

                foreach (var storeLocation in storeLocations)
                {
                    if (!File.Exists(storeLocation.FullName))
                        continue;

                    var destinationLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                        storeLocation.Name);

                    if (File.Exists(destinationLocation))
                        File.Delete(destinationLocation);

                    File.Move(storeLocation.FullName, destinationLocation);
                }
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, 0);
            }
        }

        private static void UpdateRegistry()
        {
            try
            {
                var key = Registry.CurrentUser.OpenSubKey("Software", true);
                key = key.OpenSubKey("Microsoft\\Windows NT\\CurrentVersion\\AppCompatFlags\\Layers", true);

                key.SetValue(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PatientDataAdministration.Client.exe"), "~ WIN8RTM");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
