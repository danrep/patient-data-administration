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
            Update();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Splash());
        }

        private static void Update()
        {
            try
            {
                // Update Pendings
                var storeLocations = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "DownloadedUpdate")).EnumerateFiles().Where(x => x.Name.Contains(".exe") || x.Name.Contains(".dll"));

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
    }
}
