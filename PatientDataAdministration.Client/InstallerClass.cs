using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;

namespace PatientDataAdministration.Client
{
    [RunInstaller(true)]
    public partial class InstallerClass : System.Configuration.Install.Installer
    {
        public InstallerClass()
        {
            InitializeComponent();
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);


            {
                var appFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                var myDirectoryInfo = new DirectoryInfo(appFolder);

                var myDirectorySecurity = Directory.GetAccessControl(appFolder);

                myDirectorySecurity.AddAccessRule(new FileSystemAccessRule("everyone",
                    FileSystemRights.Read | FileSystemRights.Write | FileSystemRights.Modify,
                    InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.InheritOnly,
                    AccessControlType.Allow));

                myDirectorySecurity.AddAccessRule(
                    new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                        FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,
                        PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                myDirectoryInfo.SetAccessControl(myDirectorySecurity);
            }
        }
    }
}
