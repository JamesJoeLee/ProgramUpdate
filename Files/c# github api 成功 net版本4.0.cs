using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
//using System.Windows.Forms;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace HelloWorldApplication
{
    class HelloWorld
    {
        static void Main(string[] args)
        {

            Console.WriteLine("test");


            string url = "https://api.github.com/repos/stomakun/NeteaseReverseLadder/releases/latest";

            //将接口传入，这个HttpUitls的类，有兴趣可以研究下，也可以直接用就可以，不用管如何实现。
            string getJson = HttpUitls.Get(url);

            // MessageBox.Show(getJson);
            Console.WriteLine(getJson);


            Console.Read();

        }
    }

    public class HttpUitls
    {
        public static string Get(string Url)
        {
            //System.GC.Collect();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Proxy = null;
            request.KeepAlive = false;
            request.Method = "GET";
            request.ContentType = "application/json; charset=UTF-8";
            request.AutomaticDecompression = DecompressionMethods.GZip;

            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; QQWubi 133; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; CIBA; InfoPath.2)";
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


            

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            myResponseStream.Close();

            if (response != null)
            {
                response.Close();
            }
            if (request != null)
            {
                request.Abort();
            }

            return retString;
        }

        public static string Post(string Url, string Data, string Referer)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.Referer = Referer;
            byte[] bytes = Encoding.UTF8.GetBytes(Data);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;
            Stream myResponseStream = request.GetRequestStream();
            myResponseStream.Write(bytes, 0, bytes.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            myResponseStream.Close();

            if (response != null)
            {
                response.Close();
            }
            if (request != null)
            {
                request.Abort();
            }
            return retString;
        }

    }
}