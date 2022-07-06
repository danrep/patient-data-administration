using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientDataAdministration.DeduplicationEngine.Engines.EngineDataValidation
{
    public class EngineSecondaryValidation : IDisposable
    {
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~EngineSecondaryValidation()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public void Execute()
        {
            try
            {
                var batchSize = 1000;
                var populationBatches = 0;
                var bioSearch = new Codesistance.UniqueBioSearchSecugen.SearchEngine();
                List<Patient_PatientBiometricDataSecondary> currentBatchPopulation;

                using (var entities = new Entities())
                {
                    populationBatches = entities.Patient_PatientBiometricDataSecondary.Count() / batchSize;
                }

                for (int i = 0; i < populationBatches; i++)
                {
                    ActivityLogger.Log("INFO", $"Working on Batch {i + 1} of {populationBatches}");

                    using (var entities = new Entities())
                    {
                        currentBatchPopulation = entities.Patient_PatientBiometricDataSecondary
                        .Where(x => x.BioDataScore != 0)
                        .OrderBy(x => x.Id)
                        .Skip(i * batchSize)
                        .Take(batchSize)
                        .ToList();
                    }

                    if (!currentBatchPopulation.Any())
                        break;

                    var chunkedBatch = Transforms.ListChunk(currentBatchPopulation, (batchSize / 10)).ToList();

                    Parallel.ForEach(chunkedBatch, (currentBatch) =>
                    {
                        try
                        {
                            var entities = new Entities();

                            foreach (var batchItem in currentBatch)
                            {
                                var resolvedData = Resolvers.ResolveSecondaryBioData(batchItem);

                                int bioDataScore = 0;

                                foreach (var resolvedDatum in resolvedData)
                                {
                                    try
                                    {
                                        if (bioSearch.TestData(Convert.FromBase64String(resolvedDatum.FingerPrintData)))
                                            bioDataScore += 10;
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }

                                if (batchItem.BioDataScore != bioDataScore)
                                {
                                    batchItem.BioDataScore = bioDataScore;
                                    entities.Entry(batchItem).State = System.Data.Entity.EntityState.Modified;
                                    ActivityLogger.Log("INFO", $"Updated {batchItem.PepId} to {bioDataScore}");
                                }

                                resolvedData = null;
                            }

                            entities.SaveChanges();
                            entities.Database.Connection.Close();
                            entities.Dispose();
                        }
                        catch (Exception ex)
                        {
                            ActivityLogger.Log(ex);
                        }

                        currentBatch = null;
                    });

                    chunkedBatch = null;
                    currentBatchPopulation.Clear();

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

                bioSearch.DeInitialize();
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }
        }
    }
}
