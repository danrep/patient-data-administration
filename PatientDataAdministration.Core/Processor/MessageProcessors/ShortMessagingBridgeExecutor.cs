using System;
using PatientDataAdministration.Core.DataTranslation;

namespace PatientDataAdministration.Core.Processor.MessageProcessors
{
    public class ShortMessagingBridgeExecutor
    {
        public static void ProcessMessage(Result result)
        {
            try
            {
                LogMessage(result);

                var messageParts = result.Text.Split(' ');
                ActivityLogger.Log("MSG_AUDIT", Newtonsoft.Json.JsonConvert.SerializeObject(result));

                switch (messageParts[0].ToLower())
                {
                    
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        private static void LogMessage(Result result)
        {
            
        }
    }
}