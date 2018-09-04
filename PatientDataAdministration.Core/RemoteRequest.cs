using PatientDataAdministration.Data.InterchangeModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PatientDataAdministration.Core
{
    public class RemoteRequest
    {
        public static async Task<ResponseData> RequestGet(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

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
                ActivityLogger.Log(e);
                return new ResponseData
                {
                    Message = e.Message,
                    Status = false
                };
            }
        }

        public static ResponseData RequestPost(string url, string payload,
            NameValueCollection headerNameValueCollection = null)
        {
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(url);

                request.Method = "POST";
                request.Credentials = CredentialCache.DefaultCredentials;
                ((HttpWebRequest) request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";

                var byteArray = Encoding.UTF8.GetBytes(payload);

                request.ContentType = "application/json; charset=utf-8";
                request.ContentLength = byteArray.Length;

                if (headerNameValueCollection != null)
                    request.Headers.Add(headerNameValueCollection);

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
                ActivityLogger.Log(e);
                return new ResponseData
                {
                    Message = e.Message,
                    Status = false
                };
            }
        }
    }
}
