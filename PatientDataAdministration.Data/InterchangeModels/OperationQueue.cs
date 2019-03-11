using System;

namespace PatientDataAdministration.Data.InterchangeModels
{
    public class OperationQueue
    {
        private object _param;

        public DateTime TimeStamp { get; private set; }

        public object Param
        {
            get { return _param; }
            set
            {
                _param = value;
                TimeStamp = DateTime.Now;
            }
        }
    }
}
