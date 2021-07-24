using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.IO;

namespace DAL_BAL
{
    public class API_NhanDien
    {
        static HttpClient client = new HttpClient();
        public string getOutput()
        {
            //Ghi cái api get ở đây
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("http://127.0.0.1:8000/realtime"));
            WebReq.Method = "POST";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
            Console.WriteLine(WebResp.StatusCode);
            Console.WriteLine(WebResp.Server);

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            return jsonString;
        }
    }
}
