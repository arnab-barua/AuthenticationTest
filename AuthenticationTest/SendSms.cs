using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace AuthenticationTest
{
    public class SendSms
    {
        public string from = "8804445653970";
        public string to;
        public string text;

        public SendSms(string to, string message)
        {
            this.to = to;
            this.text = message;

            Task.Run(() => SMSSender());
        }

        public async Task SMSSender()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://107.20.199.106/");
            string jsonStringifiedMessage = new JavaScriptSerializer().Serialize(this);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "restapi/sms/1/text/single");
            request.Content = new StringContent(jsonStringifiedMessage, Encoding.UTF8, "application/json");
            request.Headers.Add("accept", "application/json");
            //request.Headers.Add("authorization", "Basic TWltdGVsOm1pbUA3ODQ2TTg5");
            request.Headers.Add("authorization", "Basic c29oYWlsQHYtbGlua25ldHdvcmsuY29tOnBhc3N3b3JkMjAxOA==");

            await client.SendAsync(request);
        }
    }
}