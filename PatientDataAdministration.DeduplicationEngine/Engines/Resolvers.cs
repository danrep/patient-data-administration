using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.Data.SecondaryBioDataModels;
using PatientDataAdministration.EnumLibrary;
using System;
using System.Collections.Generic;

namespace PatientDataAdministration.DeduplicationEngine.Engines
{
    public class Resolvers
    {
        public static List<PatientData> ResolveSecondaryBioData(Patient_PatientBiometricDataSecondary secondaryData)
        {
            try
            {
                switch ((SecondaryBioDataSources)secondaryData.DataModel)
                {
                    case SecondaryBioDataSources.NmrsBioDataXml:
                        return Resolvers.ResolveNmrsBioDataXml(secondaryData.BioDataExtract,
                            secondaryData.PepId,
                            secondaryData.Id);

                    case SecondaryBioDataSources.NdrBioDataCsv:
                        return Resolvers.ResolveNdrBioDataCsv(secondaryData.BioDataExtract,
                            secondaryData.PepId,
                            secondaryData.Id);

                    default:
                        return new List<PatientData>();
                }

            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return new List<PatientData>();
            }
        }

        public static List<PatientData> ResolveSecondaryBioData(dynamic secondaryData)
        {
            try
            {
                switch ((SecondaryBioDataSources)secondaryData.DataModel)
                {
                    case SecondaryBioDataSources.NmrsBioDataXml:
                        return Resolvers.ResolveNmrsBioDataXml(secondaryData.BioDataExtract,
                            secondaryData.PepId,
                            secondaryData.Id);

                    case SecondaryBioDataSources.NdrBioDataCsv:
                        return Resolvers.ResolveNdrBioDataCsv(secondaryData.BioDataExtract,
                            secondaryData.PepId,
                            secondaryData.Id);

                    default:
                        return new List<PatientData>();
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return new List<PatientData>();
            }
        }

        private static List<PatientData> ResolveNmrsBioDataXml(string bioDataExtract, string pepId, long rowId)
        {
            try
            {
                var patientData = new List<PatientData>();

                var fingerPrints = Newtonsoft.Json.JsonConvert.DeserializeObject<FingerPrints>(bioDataExtract);

               if (!string.IsNullOrEmpty(fingerPrints.LeftHand?.LeftIndex))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.LeftIndex,
                        FingerPrintData = fingerPrints.LeftHand.LeftIndex,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.LeftHand?.LeftMiddle))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.LeftMiddle,
                        FingerPrintData = fingerPrints.LeftHand.LeftMiddle,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.LeftHand?.LeftSmall))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.LeftSmall,
                        FingerPrintData = fingerPrints.LeftHand.LeftSmall,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.LeftHand?.LeftThumb))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.LeftThumb,
                        FingerPrintData = fingerPrints.LeftHand.LeftThumb,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.LeftHand?.LeftWedding))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.LeftRing,
                        FingerPrintData = fingerPrints.LeftHand.LeftWedding,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.RightHand?.RightIndex))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.RightIndex,
                        FingerPrintData = fingerPrints.RightHand.RightIndex,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.RightHand?.RightMiddle))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.RightMiddle,
                        FingerPrintData = fingerPrints.RightHand.RightMiddle,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.RightHand?.RightSmall))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.RightSmall,
                        FingerPrintData = fingerPrints.RightHand.RightSmall,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.RightHand?.RightThumb))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.RightThumb,
                        FingerPrintData = fingerPrints.RightHand.RightThumb,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.RightHand?.RightWedding))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.RightRing,
                        FingerPrintData = fingerPrints.RightHand.RightWedding,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });

                fingerPrints = null;
                return patientData;
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return new List<PatientData>();
            }
        }

        private static List<PatientData> ResolveNdrBioDataCsv(string bioDataExtract, string pepId, long rowId)
        {
            try
            {
                var patientData = new List<PatientData>();

                var fingerPrints = Newtonsoft.Json.JsonConvert.DeserializeObject<NdrCsvFingerprints>(bioDataExtract);

                if (!string.IsNullOrEmpty(fingerPrints.LeftIndex))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.LeftIndex,
                        FingerPrintData = fingerPrints.LeftIndex,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.LeftMiddle))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.LeftMiddle,
                        FingerPrintData = fingerPrints.LeftMiddle,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.LeftSmall))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.LeftSmall,
                        FingerPrintData = fingerPrints.LeftSmall,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.LeftThumb))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.LeftThumb,
                        FingerPrintData = fingerPrints.LeftThumb,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.LeftWedding))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.LeftRing,
                        FingerPrintData = fingerPrints.LeftWedding,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.RightIndex))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.RightIndex,
                        FingerPrintData = fingerPrints.RightIndex,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.RightMiddle))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.RightMiddle,
                        FingerPrintData = fingerPrints.RightMiddle,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.RightSmall))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.RightSmall,
                        FingerPrintData = fingerPrints.RightSmall,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.RightThumb))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.RightThumb,
                        FingerPrintData = fingerPrints.RightThumb,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.RightWedding))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.RightRing,
                        FingerPrintData = fingerPrints.RightWedding,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });

                fingerPrints = null;
                return patientData;
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return new List<PatientData>();
            }
        }
    }
}
