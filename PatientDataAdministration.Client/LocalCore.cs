using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PatientDataAdministration.Data.InterchangeModels;

namespace PatientDataAdministration.Client
{
    public static class LocalCore
    {
        private static readonly LocalPDAEntities PdaEntities = new LocalPDAEntities();
        private static int _userCredentialId = 0;

        public static DialogResult TreatError(Exception exception, int userCredentialId, bool silent = false)
        {
            LocalCore._userCredentialId = userCredentialId;
            return silent ? MessageBox.Show(exception.Message + @"\n" + exception.InnerException?.Message) : DialogResult.OK;
        }

        private static void TreatError(Exception exception)
        {
            try
            {
                PdaEntities.System_ErrorLog.Add(new System_ErrorLog()
                {
                    IsDeleted = false, 
                    ErrorDate = DateTime.Now, 
                    ErrorMessage = exception.Message,
                    ErrorString = exception.ToString(),
                    SyncStatus = false,
                    UserId = _userCredentialId
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string BaseUrl
            =>
                PdaEntities.System_Setting.FirstOrDefault(x => x.SettingKey == (int) EnumLibrary.SettingKey.RemoteApi)?
                    .SettingValue;

        public static async Task<ResponseData> Get(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseData>(json);
                    }
                    else
                        return new ResponseData
                        {
                            Message = "Bad Response",
                            Status = false
                        };
                }
            }
            catch (Exception e)
            {
                TreatError(e);
                return new ResponseData
                {
                    Message = e.Message,
                    Status = false
                };
            }
        }

        public static ResponseData Post(string url, string payload)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(BaseUrl + url);

                request.Method = "POST";
                request.Credentials = CredentialCache.DefaultCredentials;
                ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";

                var byteArray = Encoding.UTF8.GetBytes(payload);
                request.ContentType = "application/json; charset=utf-8";
                request.ContentLength = byteArray.Length;
                var dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                var response = request.GetResponse();
                dataStream = response.GetResponseStream();
                var reader = new StreamReader(dataStream);
                var responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                
                return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseData>(responseFromServer);
            }
            catch (Exception e)
            {
                TreatError(e);
                return new ResponseData
                {
                    Message = e.Message,
                    Status = false
                };
            }
        }
    }
}
