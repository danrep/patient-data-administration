using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Http;
using PatientDataAdministration.Data.InterchangeModels;

namespace PatientDataAdministration.Client.Web.Controllers
{
    public class DataIngressAppointmentController: ApiController
    {
        public ResponseData Post([FromBody]List<IntegrationAppointmentDataIngress> payload)
        {
            try
            {
                var startTime = DateTime.Now;

                if (payload == null)
                    return ResponseData.SendFailMsg("No Data was Found in the Payload");

                var basePath = LocalSettingStorage.AppSetting.PathAppointmentDataIngress;

                if (!Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);

                long size = 0;
                foreach (var chunk in Core.Transforms.ListChunk(payload, 100))
                {
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pda";

                    File.WriteAllText(Path.Combine(basePath, fileName),
                        Newtonsoft.Json.JsonConvert.SerializeObject(chunk), Encoding.UTF8);

                    size += new FileInfo(Path.Combine(basePath, fileName)).Length;
                }

                return ResponseData.SendSuccessMsg(data: new
                {
                    FileSize = $"{size:#,##0} b",
                    Duration = $"{DateTime.Now.Subtract(startTime).TotalMilliseconds:#,##0} ms"
                });
            }
            catch (Exception e)
            {
                return ResponseData.SendExceptionMsg(e);
            }
        }
    }
}
