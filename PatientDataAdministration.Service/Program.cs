﻿using System.ServiceProcess;

namespace PatientDataAdministration.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new PatientDataAdministrationService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
