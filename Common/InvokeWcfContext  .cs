
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Linq;

namespace DXCommon
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class InvokeWcfContext
    {

        #region Wcf服务工厂  

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hostName">host Name 即可</param>
        /// <returns></returns>
        public static T CreateWCFServiceByHostName<T>(string hostName)
        {
            return CreateWCFServiceByHostName<T>(hostName, "wsHttpBinding");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hostName">host Name 即可</param>
        /// <param name="bing"></param>
        /// <returns></returns>
        public static T CreateWCFServiceByHostName<T>(string hostName, string bing)
        {
            if (!hostName.EndsWith("/"))
            {
                hostName = string.Format("{0}/", hostName);
            }
            var url = string.Format("{0}{1}", hostName, WcfUrl.Instance.GetUrlValue<T>());
            return CreateWCFServiceByURL<T>(url, bing);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">完整的路径</param>
        /// <returns></returns>
        public static T CreateWCFServiceByURL<T>(string url)
        {
            return CreateWCFServiceByURL<T>(url, "wsHttpBinding");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">host Name 即可</param>
        /// <param name="bing"></param>
        /// <returns></returns>
        public static T CreateWCFServiceByURL<T>(string url, string bing)
        {
            if (string.IsNullOrEmpty(url))
                throw new NotSupportedException("this url isn`t Null or Empty!");

            EndpointAddress address = new EndpointAddress(url);
            Binding binding = CreateBinding(bing);
            ChannelFactory<T> factory = new ChannelFactory<T>(binding, address);
            return factory.CreateChannel();
        }
        #endregion

        #region 创建传输协议  
        /// <summary>  
        /// 创建传输协议  
        /// </summary>  
        /// <param name="binding">传输协议名称</param>  
        /// <returns></returns>  
        private static Binding CreateBinding(string binding)
        {
            Binding bindinginstance = null;
            if (binding.ToLower() == "basichttpbinding")
            {
                BasicHttpBinding ws = new BasicHttpBinding();
                ws.MaxBufferSize = 2147483647;
                ws.MaxBufferPoolSize = 2147483647;
                ws.MaxReceivedMessageSize = 2147483647;
                ws.ReaderQuotas.MaxStringContentLength = 2147483647;
                ws.CloseTimeout = new TimeSpan(0, 10, 0);
                ws.OpenTimeout = new TimeSpan(0, 10, 0);
                ws.ReceiveTimeout = new TimeSpan(0, 10, 0);
                ws.SendTimeout = new TimeSpan(0, 10, 0);

                bindinginstance = ws;
            }
            else if (binding.ToLower() == "netnamedpipebinding")
            {
                NetNamedPipeBinding ws = new NetNamedPipeBinding();
                ws.MaxReceivedMessageSize = 65535000;
                bindinginstance = ws;
            }
            else if (binding.ToLower() == "netpeertcpbinding")
            {
                NetPeerTcpBinding ws = new NetPeerTcpBinding();
                ws.MaxReceivedMessageSize = 65535000;
                bindinginstance = ws;
            }
            else if (binding.ToLower() == "nettcpbinding")
            {
                NetTcpBinding ws = new NetTcpBinding();
                ws.MaxReceivedMessageSize = 65535000;
                ws.Security.Mode = SecurityMode.None;
                bindinginstance = ws;
            }
            else if (binding.ToLower() == "wsdualhttpbinding")
            {
                WSDualHttpBinding ws = new WSDualHttpBinding();
                ws.MaxReceivedMessageSize = 65535000;

                bindinginstance = ws;
            }
            else if (binding.ToLower() == "webhttpbinding")
            {
                //WebHttpBinding ws = new WebHttpBinding();  
                //ws.MaxReceivedMessageSize = 65535000;  
                //bindinginstance = ws;  
            }
            else if (binding.ToLower() == "wsfederationhttpbinding")
            {
                WSFederationHttpBinding ws = new WSFederationHttpBinding();
                ws.MaxReceivedMessageSize = 65535000;
                bindinginstance = ws;
            }
            else if (binding.ToLower() == "wshttpbinding")
            {
                WSHttpBinding ws = new WSHttpBinding(SecurityMode.None);
                ws.MaxReceivedMessageSize = 65535000;
                ws.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
                ws.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
                bindinginstance = ws;
            }
            return bindinginstance;

        }
        #endregion

        private class WcfUrl
        {
            private const string setFilePath = @"Config\WcfUrl.xml";

            private static object _locker = new object();
            private WcfUrl()
            {
                _settings = LoadSettings();
            }
            private string FilePath
            {
                get
                {
                    return Path.Combine(PublicMethod.AppDirectory, setFilePath);
                }
            }
            private Dictionary<string, string> _settings;

            private Dictionary<string, string> LoadSettings()
            {
                if (!File.Exists(FilePath))
                {
                    return new Dictionary<string, string>();
                }
                var ds = new DataSet();
                ds.ReadXml(FilePath);
                var dt = ds.Tables[0];
                var list = (from DataRow dr in dt.Rows
                            select dr).ToDictionary(dr => dr["interfacename"].ToString(), dr => dr["url"].ToString());
                return list;
            }

            private static WcfUrl _instance;


            public static WcfUrl Instance
            {
                get
                {
                    if (_instance != null)
                        return _instance;
                    lock (_locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new WcfUrl();
                        }
                    }
                    return _instance;
                }
            }

            /// <summary>
            /// 获取接口的相对路径
            /// </summary> 
            /// <returns></returns>
            public string GetUrlValue<T>()
            {
                var k = typeof(T).Name;
                if (_settings.ContainsKey(k))
                {
                    return _settings[k];
                }
                return null;
            }
        }
    }
}
