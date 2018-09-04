using System;
using System.Collections.Generic;

namespace Codesistance.UniqueBioSearchSecugen
{
    public struct Template
    {
        public string Filename;
        public uint Index;
        public byte[] TemplatesBuffer;

        public Template(string filename, uint index, byte[] templateBuffer)
        {
            Filename = filename;
            Index = index;
            TemplatesBuffer = templateBuffer;
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
                var template = new Template(patientDatum.PepId, i,
                    Convert.FromBase64String(patientDatum.FingerPrintData));
                Add(template);
                i++;
            }

            return Size > 0;
        }

        public Template GetTemplate(int index)
        {
            return (Template)_templates[index];
        }

        private void Add(Template template)
        {
            _templates.Add(template);
        }
    }

    public class PatientData
    {
        public string PepId { get; set; }
        public string FingerPrintData { get; set; }
        public FingerPrintPosition FingerPosition { get; set; }
    }

    public enum FingerPrintPosition
    {
        Left = 1, 
        Right
    }
} 
