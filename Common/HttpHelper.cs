using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DXCommon
{
    /// <summary>
    /// 处理http请求
    /// </summary>
    public class HttpHelper
    {

        /// <summary>
        /// 请求方式
        /// </summary>
        public enum EMethod
        {
            /// <summary>
            /// POST
            /// </summary>
            POST,
            /// <summary>
            /// GET
            /// </summary>
            GET
        }


        private string _url;
        private EMethod _method;
        private const string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";


        /// <summary>
        /// 请求的地址。要包含“http”或者“https”
        /// </summary>
        public string URL
        {
            get { return _url; }
        }

        /// <summary>
        /// 请求方式
        /// </summary>
        public EMethod Method
        {
            get { return _method; }
        }

        /// <summary>
        /// 请求的参数
        /// </summary>
        public IDictionary<string, object> Parameters { get; set; }


        /// <summary>
        /// 请求的文件
        /// </summary>
        public string[] ArrayFiles { get; set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        public int? timeout { get; set; }
        /// <summary>
        /// 请求的客户端浏览器信息，可以为空
        /// </summary>
        public string userAgent { get; set; }
        /// <summary>
        /// 发送HTTP请求时所用的编码。默认为UTF8
        /// </summary>
        public Encoding RequestEncoding { get; set; }
        /// <summary>
        /// 随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空
        /// </summary>
        public CookieCollection cookies { get; set; }


        /// <summary>
        /// HttpRequest
        /// </summary>
        public HttpWebRequest Request
        {
            get;
            private set;
        }


        /// <summary>
        /// HttpWebResponse
        /// </summary>
        public HttpWebResponse Response
        {
            get;
            private set;
        }
        /// <summary>
        /// 实例化
        /// </summary>
        public HttpHelper(string url, EMethod method = EMethod.GET)
        {
            _url = url;
            _method = method;
            Parameters = new Dictionary<string, object>();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            if (RequestEncoding == null)
            {
                RequestEncoding = Encoding.UTF8;
            }
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <returns></returns>
        public Stream SendHttpRequstStream(string jsonParas = null)
        {
            Init();
            if (string.IsNullOrEmpty(URL))
            {
                throw new ArgumentNullException("URL");
            }

            //如果是发送HTTPS请求  
            if (URL.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                // We're using SSL3 here and not TLS. Without this line, nothing works.
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                Request = WebRequest.Create(URL) as HttpWebRequest;
                Request.ProtocolVersion = HttpVersion.Version10;
                // Const.WriteJobTxt("<===>" + URL, "Https");
            }
            else
            {
                Request = WebRequest.Create(URL) as HttpWebRequest;
            }
            string boundary = "----------------------------" +
           DateTime.Now.Ticks.ToString("x");

            Request.Method = Method.ToString();
            Request.ContentType = "multipart/form-data; boundary=" + boundary;
            //enctype="multipart/form-data     只有使用了这个才能完整的传递文件数据
            Request.Credentials = System.Net.CredentialCache.DefaultCredentials;
            long length = 0;
            Stream memStream = new System.IO.MemoryStream();

            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            memStream.Write(boundarybytes, 0, boundarybytes.Length);
            length += boundarybytes.Length;

            if (!string.IsNullOrEmpty(userAgent))
            {
                Request.UserAgent = userAgent;
            }
            else
            {
                Request.UserAgent = DefaultUserAgent;
            }

            if (timeout.HasValue)
            {
                Request.Timeout = timeout.Value;
            }

            if (cookies != null)
            {
                Request.CookieContainer = new CookieContainer();
                var uri = new Uri(URL);
                Request.CookieContainer.Add(uri, cookies);
            }

            //如果有本地文件上传
            if (ArrayFiles != null && ArrayFiles.Length > 0)
            {
                memStream.Write(boundarybytes, 0, boundarybytes.Length);
                length += boundarybytes.Length;

                string headerTemplate = "Content-Disposition: form-data; name=\"{0}\" filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";

                for (int i = 0; i < ArrayFiles.Length; i++)
                {
                    string header = string.Format(headerTemplate, "file" + i, ArrayFiles[i]);

                    byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);

                    memStream.Write(headerbytes, 0, headerbytes.Length);
                    length += headerbytes.Length;

                    FileStream fileStream = new FileStream(ArrayFiles[i], FileMode.Open, FileAccess.Read);
                    byte[] buffer = new byte[1024];

                    int bytesRead = 0;

                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        memStream.Write(buffer, 0, bytesRead);
                        length += bytesRead;
                    }


                    memStream.Write(boundarybytes, 0, boundarybytes.Length);
                    length += boundarybytes.Length;

                    fileStream.Close();
                }
            }

            #region Parameters
            //如果需要POST数据  
            if (Parameters != null && Parameters.Count > 0)
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in Parameters.Keys)
                {
                    string httpRow = "--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n";
                    buffer.AppendFormat(httpRow, key, Parameters[key]);
                }


                byte[] data = RequestEncoding.GetBytes(buffer.ToString());
                if (memStream != null && memStream.Length > 2)
                {
                    memStream.Position = 0;
                    byte[] tempBuffer = new byte[memStream.Length];
                    memStream.Read(tempBuffer, 0, tempBuffer.Length);
                    memStream.Close();

                    var dataResultBuffer = new byte[tempBuffer.Length + boundarybytes.Length + data.Length];

                    data.CopyTo(dataResultBuffer, 0);
                    boundarybytes.CopyTo(dataResultBuffer, data.Length);
                    tempBuffer.CopyTo(dataResultBuffer, data.Length + boundarybytes.Length);
                    Request.ContentLength = dataResultBuffer.Length;

                    using (Stream stream = Request.GetRequestStream())
                    {
                        stream.Write(dataResultBuffer, 0, dataResultBuffer.Length);
                    }
                }
                else
                {
                    Request.ContentLength = data.Length;
                    using (Stream stream = Request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);

                    }
                }
            }
            #endregion

            //如果需要POST数据  
            if (!string.IsNullOrEmpty(jsonParas))
            {
                //StringBuilder buffer = new StringBuilder();

                //string httpRow = "--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n";
                //buffer.AppendFormat(httpRow, jsonParas);



                byte[] data = RequestEncoding.GetBytes(jsonParas);
                Request.Method = "POST";
                Request.ContentType = "application/x-www-form-urlencoded";
                Request.ContentLength = data.Length;

                using (Stream reqStream = Request.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Flush();
                    reqStream.Close();
                }
            }


            Response = Request.GetResponse() as HttpWebResponse;
            return Response.GetResponseStream(); 
        }
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <returns></returns>
        public string SendHttpRequst(string jsonParas = null)
        {
            Init();
            if (string.IsNullOrEmpty(URL))
            {
                throw new ArgumentNullException("URL");
            }

            //如果是发送HTTPS请求  
            if (URL.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                // We're using SSL3 here and not TLS. Without this line, nothing works.
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                Request = WebRequest.Create(URL) as HttpWebRequest;
                Request.ProtocolVersion = HttpVersion.Version10;
                // Const.WriteJobTxt("<===>" + URL, "Https");
            }
            else
            {
                Request = WebRequest.Create(URL) as HttpWebRequest;
            }
            string boundary = "----------------------------" +
           DateTime.Now.Ticks.ToString("x");

            Request.Method = Method.ToString();
            Request.ContentType = "multipart/form-data; boundary=" + boundary;
            //enctype="multipart/form-data     只有使用了这个才能完整的传递文件数据
            Request.Credentials = System.Net.CredentialCache.DefaultCredentials;
            long length = 0;
            Stream memStream = new System.IO.MemoryStream();

            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            memStream.Write(boundarybytes, 0, boundarybytes.Length);
            length += boundarybytes.Length;

            if (!string.IsNullOrEmpty(userAgent))
            {
                Request.UserAgent = userAgent;
            }
            else
            {
                Request.UserAgent = DefaultUserAgent;
            }

            if (timeout.HasValue)
            {
                Request.Timeout = timeout.Value;
            }

            if (cookies != null)
            {
                Request.CookieContainer = new CookieContainer();
                var uri = new Uri(URL);
                Request.CookieContainer.Add(uri, cookies);
            }

            //如果有本地文件上传
            if (ArrayFiles != null && ArrayFiles.Length > 0)
            {
                memStream.Write(boundarybytes, 0, boundarybytes.Length);
                length += boundarybytes.Length;

                string headerTemplate = "Content-Disposition: form-data; name=\"{0}\" filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";

                for (int i = 0; i < ArrayFiles.Length; i++)
                {
                    string header = string.Format(headerTemplate, "file" + i, ArrayFiles[i]);

                    byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);

                    memStream.Write(headerbytes, 0, headerbytes.Length);
                    length += headerbytes.Length;

                    FileStream fileStream = new FileStream(ArrayFiles[i], FileMode.Open, FileAccess.Read);
                    byte[] buffer = new byte[1024];

                    int bytesRead = 0;

                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        memStream.Write(buffer, 0, bytesRead);
                        length += bytesRead;
                    }


                    memStream.Write(boundarybytes, 0, boundarybytes.Length);
                    length += boundarybytes.Length;

                    fileStream.Close();
                }
            }

            #region Parameters
            //如果需要POST数据  
            if (Parameters != null && Parameters.Count > 0)
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in Parameters.Keys)
                {
                    string httpRow = "--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n";
                    buffer.AppendFormat(httpRow, key, Parameters[key]);
                }


                byte[] data = RequestEncoding.GetBytes(buffer.ToString());
                if (memStream != null && memStream.Length > 2)
                {
                    memStream.Position = 0;
                    byte[] tempBuffer = new byte[memStream.Length];
                    memStream.Read(tempBuffer, 0, tempBuffer.Length);
                    memStream.Close();

                    var dataResultBuffer = new byte[tempBuffer.Length + boundarybytes.Length + data.Length];

                    data.CopyTo(dataResultBuffer, 0);
                    boundarybytes.CopyTo(dataResultBuffer, data.Length);
                    tempBuffer.CopyTo(dataResultBuffer, data.Length + boundarybytes.Length);
                    Request.ContentLength = dataResultBuffer.Length;

                    using (Stream stream = Request.GetRequestStream())
                    {
                        stream.Write(dataResultBuffer, 0, dataResultBuffer.Length);
                    }
                }
                else
                {
                    Request.ContentLength = data.Length;
                    using (Stream stream = Request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);

                    }
                }
            }
            #endregion

            //如果需要POST数据  
            if (!string.IsNullOrEmpty(jsonParas))
            {
                //StringBuilder buffer = new StringBuilder();

                //string httpRow = "--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n";
                //buffer.AppendFormat(httpRow, jsonParas);



                byte[] data = RequestEncoding.GetBytes(jsonParas);
                Request.Method = "POST";
                Request.ContentType = "application/x-www-form-urlencoded";
                Request.ContentLength = data.Length;

                using (Stream reqStream = Request.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Flush();
                    reqStream.Close();
                }
            }

            var strResult = string.Empty;
            try
            {
                Response = Request.GetResponse() as HttpWebResponse;
                var srReader = new StreamReader(Response.GetResponseStream(), Encoding.UTF8);
                strResult = srReader.ReadToEnd();
                ////因为这个地方API返回格式不统一，暂时不用这个处理。
                //var user = JSONhelper.ConvertToObject<ServerUserInfoMsg>(strResult);
                //if (!user.Success)
                //{
                //    HttpContext.Current.Response.Redirect(@"\WeChatApp\API\WeChatRedirect.aspx?action=UserLogin");
                //    HttpContext.Current.Response.End();
                //}
                return strResult;
            }
            catch (Exception ex)
            {
                var msg = new JsonMessage { Data = "", Message = ex.Message, Success = false };
                return msg.ToString();
            }
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }
    }
}