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

                ActivityLogger.Log("MSG_AUDIT", Newtonsoft.Json.JsonConvert.SerializeObject(result));

                switch (result.Keyword?.ToUpper())
                {
                    case "HELP":
                        break;
                    case null:
                        //This is for messages that have no keyword
                        break;
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