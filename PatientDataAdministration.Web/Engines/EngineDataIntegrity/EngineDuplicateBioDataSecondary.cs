﻿using System;
using System.Collections.Generic;
using System.Linq;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.EnumLibrary;

namespace PatientDataAdministration.Web.Engines.EngineDataIntegrity
{
    public class EngineDuplicateBioDataSecondary
    {
        public static List<Patient_PatientBiometricSecondaryIntegrityCase> BioDataIntegrityCases { get; set; }
        public static bool IsProcessing { get; set; }

        public static void ProcessDataIntegrityBiometric()
        {
            IsProcessing = true;

            //Load Pending Cases
            RefreshBioDataIntegrityCases();

            IsProcessing = false;
        }

        private static void RefreshBioDataIntegrityCases()
        {
            try
            {
                using (var entities = new Entities())
                {
                    BioDataIntegrityCases = entities.Patient_PatientBiometricSecondaryIntegrityCase
                        .Where(x => !x.IsDeleted && x.CaseStatus == (int) CaseStatus.Open).ToList();
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }
    }
}