using System;
using System.Collections.Generic;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data.InterchangeModels;

namespace Codesistance.UniqueBioSearchSecugen
{
    public struct Template
    {
        public string Filename;
        public uint Index;
        public byte[] TemplatesBuffer;
        public object Data;

        public Template(string filename, uint index, byte[] templateBuffer, object data)
        {
            Filename = filename;
            Index = index;
            TemplatesBuffer = templateBuffer;
            Data = data;
        }
    }

    public class SearchModel
    {
        private List<Template> _templates;

        public int Size => _templates.Count;

        public SearchModel()
        {
            _templates = new List<Template>();
        }

        public bool Load(List<PatientData> patientData)
        {
            uint i = 0;

            foreach (var patientDatum in patientData)
            {
                try
                {
                    var template = new Template(patientDatum.PepId, i,
                        Convert.FromBase64String(patientDatum.FingerPrintData), patientDatum);

                    Add(template);
                    i++;
                }
                catch (Exception e)
                {
                    ActivityLogger.Log(e);
                    ActivityLogger.Log("WARN", $"Data Issue ==> {Newtonsoft.Json.JsonConvert.SerializeObject(patientDatum)}");
                }
            }

            return Size > 0;
        }

        public Template GetTemplate(int index)
        {
            return _templates[index];
        }

        private void Add(Template template)
        {
            _templates.Add(template);
        }

        public void Clear()
        {
            _templates = new List<Template>();
        }
    }

    public class MatchModel
    {
        public string Pivot { get; set; }
        public PatientData PivotData { get; set; }
        public List<SuspectedCandidate> SuspectedCandidates { get; set; }
    }

    public class SuspectedCandidate
    {
        public Template BioDataSuspect { get; set; }
        public int MatchScore { get; set; }
    }
} 
