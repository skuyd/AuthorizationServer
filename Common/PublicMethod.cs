using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

//using DXCommon.Data.SqlServer;
using System.Xml;

namespace DXCommon
{
    public class PublicMethod
    {
        #region 创建Id

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        public static string CreateGuidKey()
        {
            return Guid.NewGuid().ToString("N");
        }

        #endregion 创建Id

        #region 由Object取值

        /// <summary>
        /// 取得Int值,如果为Null 则返回０
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int GetInt(object obj)
        {
            if (obj != null)
            {
                int i;
                int.TryParse(obj.ToString(), out i);
                return i;
            }
            else
                return 0;
        }

        /// <summary>
        /// 取得Int值,如果为Null 则返回０
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int? GetNullInt(object obj)
        {
            if (obj != null)
            {
                int i;
                int.TryParse(obj.ToString(), out i);
                return i;
            }
            return null;
        }

        public static double GetDouble(object obj)
        {
            if (obj != null)
            {
                double d;
                double.TryParse(obj.ToString(), out d);
                return d;
            }
            else
                return 0;
        }

        public static double? GetNullDouble(object obj)
        {
            if (obj != null)
            {
                double d;
                double.TryParse(obj.ToString(), out d);
                return d;
            }
            return null;
        }

        public static float GetFloat(object obj)
        {
            if (obj != null)
            {
                float f;
                float.TryParse(obj.ToString(), out f);
                return f;
            }
            else
                return 0;
        }

        public static float? GetNullFloat(object obj)
        {
            if (obj != null)
            {
                float f;
                float.TryParse(obj.ToString(), out f);
                return f;
            }
            return null;
        }

        /// <summary>
        /// 取得Int值,如果不成功则返回指定exceptionvalue值
        /// </summary>
        /// <param name="obj">要计算的值</param>
        /// <param name="exceptionvalue">异常时的返回值</param>
        /// <returns></returns>
        public static int GetInt(object obj, int exceptionvalue)
        {
            if (obj == null)
                return exceptionvalue;
            if (string.IsNullOrEmpty(obj.ToString()))
                return exceptionvalue;
            int i = exceptionvalue;
            try { i = Convert.ToInt32(obj); }
            catch { i = exceptionvalue; }
            return i;
        }

        /// <summary>
        /// 取得Decima值,如果不成功则返回指定exceptionvalue值
        /// </summary>
        /// <param name="obj">要计算的值</param>
        /// <param name="exceptionvalue">异常时的返回值</param>
        /// <returns></returns>
        public static decimal GetDecimal(object obj, int exceptionvalue)
        {
            if (obj == null)
                return exceptionvalue;
            if (string.IsNullOrEmpty(obj.ToString()))
                return exceptionvalue;
            decimal i = exceptionvalue;
            try { i = Convert.ToDecimal(obj); }
            catch { i = exceptionvalue; }
            return i;
        }

        /// <summary>
        /// 取得byte值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte Getbyte(object obj)
        {
            if (obj.ToString() != "")
                return byte.Parse(obj.ToString());
            else
                return 0;
        }

        /// <summary>
        /// 获得Long值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long GetLong(object obj)
        {
            if (obj != null && obj.ToString() != "")
                return long.Parse(obj.ToString());
            else
                return 0;
        }

        /// <summary>
        /// 取得Long值,如果不成功则返回指定exceptionvalue值
        /// </summary>
        /// <param name="obj">要计算的值</param>
        /// <param name="exceptionvalue">异常时的返回值</param>
        /// <returns></returns>
        public static long GetLong(object obj, long exceptionvalue)
        {
            if (obj == null)
                return exceptionvalue;
            if (string.IsNullOrEmpty(obj.ToString()))
                return exceptionvalue;
            long i = exceptionvalue;
            try { i = Convert.ToInt64(obj); }
            catch { i = exceptionvalue; }
            return i;
        }

        /// <summary>
        /// 取得Decimal值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal GetDecimal(object obj)
        {
            if (obj != null && obj.ToString() != "")
                return decimal.Parse(obj.ToString());
            else
                return 0;
        }

        /// <summary>
        /// 取得Decimal值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal? GetNullDecimal(object obj)
        {
            if (obj != null && obj.ToString() != "")
                return decimal.Parse(obj.ToString());
            else
                return null;
        }

        /// <summary>
        /// 取得Guid值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Guid GetGuid(object obj)
        {
            if (obj != null && obj.ToString() != "")
                return new Guid(obj.ToString());
            else
                return Guid.Empty;
        }

        /// <summary>
        /// 取得DateTime值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(object obj)
        {
            if (obj != null && obj.ToString() != "")
                return DateTime.Parse(obj.ToString());
            else
                return new DateTime(2011, 1, 1);
        }

        /// <summary>
        /// 取得DateTime值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime? GetNullDateTime(object obj)
        {
            if (obj != null && obj.ToString() != "")
                return DateTime.Parse(obj.ToString());
            else
                return new DateTime?();
        }
        /// <summary>
        /// 取得DateTime值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static DateTime? GetNullDateTime(object obj, DateTime? defaultValue)
        {
            if (obj != null && obj.ToString() != "")
                return DateTime.Parse(obj.ToString());
            else
                return defaultValue;
        }

        /// <summary>
        /// 取得bool值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetBool(object obj)
        {
            if (obj != null)
            {
                bool flag;
                bool.TryParse(obj.ToString(), out flag);
                return flag;
            }
            else
                return false;
        }

        /// <summary>
        /// 取得byte[]
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Byte[] GetByte(object obj)
        {
            if (obj.ToString() != "")
            {
                return (Byte[])obj;
            }
            else
                return null;
        }

        /// <summary>
        /// 将字符串直接转换为简单类型的对象（值）
        /// </summary>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public static object ConvertSimpleType(string value, Type destinationType)
        {
            object returnValue;
            if ((value == null) || destinationType.IsInstanceOfType(value)
                || value == string.Empty)
            {
                return value;
            }

            TypeConverter converter = TypeDescriptor.GetConverter(destinationType);
            bool flag = converter.CanConvertFrom(value.GetType());
            if (!flag)
            {
                converter = TypeDescriptor.GetConverter(value.GetType());
            }

            if (!flag && !converter.CanConvertTo(destinationType))
            {
                throw new InvalidOperationException("无法转换成类型：" + value + "==>" + destinationType);
            }
            try
            {
                returnValue = flag
                    ? converter.ConvertFrom(null, null, value)
                    : converter.ConvertTo(null, null, value, destinationType);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("类型转换出错：" + value + "==>" + destinationType, e);
            }
            return returnValue;
        }

        /// <summary>
        /// 取得string值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetString(object obj)
        {
            if (obj != null && obj != DBNull.Value)
            {
                if (obj.GetType() == typeof(DateTime))
                {
                    var value = Convert.ToDateTime(obj).ToString("yyyy-MM-dd HH:mm:ss");
                    return value;
                }
                else
                {
                    return obj.ToString();
                }
            }
            else
                return "";
        }

        #endregion 由Object取值

        /// <summary>
        /// 返回查询表单之间传值的字符串,异常返回undefined
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetQueryString(string name)
        {
            try
            {
                if (HttpContext.Current.Request.QueryString[name] != null)
                {
                    return HttpContext.Current.Request.QueryString[name].ToString(); ;
                }
                else if (HttpContext.Current.Request.Form[name] != null)
                {
                    return HttpContext.Current.Request.Form[name].ToString();
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 关闭当前窗口返回值
        /// </summary>
        public static void CloseWindowReturnValues(string value)
        {
            #region

            System.Text.StringBuilder Str = new System.Text.StringBuilder();
            Str.Append("<Script language='JavaScript'type=\"text/javascript\">");
            Str.Append("var str='" + value + "';");
            Str.Append("top.returnValue=str;");
            Str.Append("top.close();</Script>");

            HttpContext.Current.Response.Write(Str.ToString());
            HttpContext.Current.Response.End();
            #endregion
        }

        #region 获取指定表中指定字段的最大值
        ///// <summary>
        ///// 获取指定表中指定字段的最大值
        ///// </summary>
        ///// <param name="tableName">表名称</param>
        ///// <param name="field">字段</param>
        ///// <returns>Return Type:Int</returns>
        //public static int GetMaxID(string tableName, string field)
        //{
        //    string s = "select Max(@field) from @tablename";
        //    SqlParameter[] para = { new SqlParameter("@field", field), new SqlParameter("@tablename", tableName) };
        //    object obj = SqlHelper.ExecuteScalar(SqlEasy.connString, CommandType.Text, s, para);
        //    int i = Convert.ToInt32(obj == DBNull.Value ? "0" : obj);
        //    return i;
        //}
        #endregion

        #region 获取客户端IP地址

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }

        #endregion

        #region DataTable To List

        /// <summary>
        /// DataTable To List
        /// </summary>
        /// <typeparam name="TType">object type</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static List<T> DataTableToObjectList<T>(DataTable dt) where T : new()
        {
            DataColumnCollection columns = dt.Columns;
            int columncount = columns.Count;
            List<T> result = new List<T>();    //声明一个要返回的对象泛型
            Type type = typeof(T);

            PropertyInfo[] propertys = type.GetProperties(BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);   //获取参数对象属性集合
            foreach (DataRow r in dt.Rows)
            {
                T t = new T();
                for (int i = 0; i < propertys.Length; i++)
                {
                    DataColumn column = columns[propertys[i].Name];
                    if (column != null && r[column] != null)
                    {
                        if (propertys[i].PropertyType == typeof(int))
                            propertys[i].SetValue(t, PublicMethod.GetInt(r[column]), null);
                        if (propertys[i].PropertyType == typeof(string))
                            propertys[i].SetValue(t, r[column].ToString(), null);
                        if (propertys[i].PropertyType == typeof(DateTime))
                            propertys[i].SetValue(t, PublicMethod.GetDateTime(r[column]), null);
                    }
                }
                result.Add(t);
            }
            return result;
        }

        /// <summary>
        /// 泛型集合转换成DATATABLE
        /// </summary>
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static DataTable ToDataTable(IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        #endregion

        /// <summary>
        /// 获取指定字符串中的指定字符的个数
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="value">要查找的字符串</param>
        /// <returns></returns>
        public static int GetCharLength(string source, string value)
        {
            Regex reg = new Regex(value);
            return reg.Matches(source).Count;
        }

        /// <summary>
        /// 给指定字符串前面增加指定值
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="value">要增加的字符串</param>
        /// <returns></returns>
        public static string CharBeforeAppend(string source, string value)
        {
            return source.Insert(0, value);
        }

        /// <summary>
        /// 给指定字符串前面增加指定个数的指定值
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="value">要增加的字符串</param>
        /// <param name="length">要增加的个数</param>
        /// <returns></returns>
        public static string CharBeforeAppend(string source, string value, int length)
        {
            for (int i = 0; i < length; i++)
            {
                source = source.Insert(0, value);
            }
            return source;
        }

        /// <summary>
        /// 合并指定表并返回
        /// </summary>
        /// <param name="dt">原始表</param>
        /// <param name="DataTables">可变表参</param>
        /// <returns></returns>
        public static DataTable MergeDataTable(DataTable dt, params DataTable[] DataTables)
        {
            if (DataTables.Length == 0)
                return dt;
            foreach (DataTable table in DataTables)
                dt.Merge(table);
            return dt;
        }

        //add by yangks-----2014.10.13------------------

        #region 随机生成指定位数的字符串---YKS

        /// <summary>
        /// 随机生成指定位数的密码
        /// </summary>
        /// <param name="u"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public static string CreatePass(int no)
        {
            Random rd = new Random();
            string pass = "";
            for (int i = 0; i < no; i++)
            {
                //int num = rd.Next(1, 4);
                //if (num == 1)//生成数字
                //{
                //    pass += rd.Next(0, 10).ToString();
                //}
                //else if (num == 2)//生成大写字母
                //{
                //    pass += ((char)rd.Next(65, 91)).ToString();
                //}
                //else if (num == 3)//生成小写字母
                //{
                pass += ((char)rd.Next(48, 127)).ToString();
                //}
            }
            return pass;
        }

        /// <summary>
        /// 随机生成指定位数的字符串
        /// </summary>
        /// <param name="u"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public static string CreateStr(int no)
        {
            Random rd = new Random();
            string str = "";
            for (int i = 0; i < no; i++)
            {
                int num = rd.Next(1, 4);
                if (num == 1)//生成数字
                {
                    str += rd.Next(0, 10).ToString();
                }
                else if (num == 2)//生成大写字母
                {
                    str += ((char)rd.Next(65, 91)).ToString();
                }
                else if (num == 3)//生成小写字母
                {
                    str += ((char)rd.Next(48, 127)).ToString();
                }
            }
            return str;
        }

        #endregion

        #region 邮件发送---YKS

        public static string SendMail(string to, string subject, string body, string fujian)
        {
            //TODO:
            //return "send ok";
            string from = System.Configuration.ConfigurationManager.AppSettings["mailsite"];
            string fromname = System.Configuration.ConfigurationManager.AppSettings["mailfrom"];
            string username = System.Configuration.ConfigurationManager.AppSettings["mailsite"];

            string password = System.Configuration.ConfigurationManager.AppSettings["mailpsw"];
            string server = System.Configuration.ConfigurationManager.AppSettings["smtpserver"];

            try
            {
                //邮件发送类
                MailMessage mail = new MailMessage();
                //发送人
                mail.From = new MailAddress(from);
                //收件人
                mail.To.Add(to);

                MailMessage Mm = new MailMessage(from, to);
                //标题
                mail.Subject = subject;
                //内容编码
                mail.BodyEncoding = Encoding.Default;
                //发送优先级
                mail.Priority = MailPriority.High;
                //邮件内容
                mail.Body = body;
                //是否HTML形式发送
                mail.IsBodyHtml = true;
                //附件
                if (fujian.Length > 0)
                {
                    mail.Attachments.Add(new Attachment(fujian));
                }
                //邮件服务器和端口
                SmtpClient smtp = new SmtpClient(server, 25);
                smtp.UseDefaultCredentials = true;
                //指定发送方式
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //指定登录名和密码
                smtp.Credentials = new System.Net.NetworkCredential(username, password);
                //超时时间
                smtp.Timeout = 10000;
                smtp.Send(mail);
                return "send ok";
            }
            catch (Exception exp)
            {
                return exp.Message;
            }
        }

        #endregion

        #region 密码/字符串强度检查(打分制)---YKS

        public static bool checkpsw(string str)
        {
            int stongenum = -1;
            if (str.Length < 6) { return false; }
            else if (str.Length == 6)
            {
                stongenum += 2;
            }
            else if (str.Length > 6 && str.Length <= 8)
            {
                stongenum += 3;
            }
            else
            {
                stongenum += 4;
            }
            Regex reg = new Regex(@"[A-Z]+");
            if (reg.IsMatch(str)) stongenum += 2;
            Regex reg1 = new Regex(@"[0-9]+");
            if (reg1.IsMatch(str)) stongenum += 1;
            Regex reg2 = new Regex(@"[a-z]+");
            if (reg2.IsMatch(str)) stongenum += 2;
            Regex reg3 = new Regex(@"[`~!@#\$%\^\&\*\(\)_\+<>\?:\{\},\.\\\/;'\[\]]+");
            if (reg3.IsMatch(str)) stongenum += 3;

            if (stongenum >= 5)
                return true;
            else
                return false;
        }

        #endregion

        #region 判断是否是数字---YKS

        public static bool IsNumeric(string Value)
        {
            try
            {
                float i = float.Parse(Value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 修改web.config节点的值---YKS 2014.12.09

        public static void EditAppValue(string KeyNameStr, string SetValueStr)
        {
            //修改web.config
            XmlDocument xDoc = new XmlDocument();
            try
            {
                //打开web.config
                xDoc.Load(System.Web.HttpContext.Current.Request.MapPath("../Web.config"));
                //string key;
                XmlNode app;
                app = xDoc.SelectSingleNode("/configuration/appSettings/add[@key='" + KeyNameStr + "']");
                app.Attributes["value"].Value = SetValueStr;
                //关闭
                xDoc.Save(System.Web.HttpContext.Current.Request.MapPath("../web.config"));
                System.Web.HttpContext.Current.Response.Write("<script>alert('配置数据修改完成！');</script>");
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('" + ex.Message.ToString() + "');</script>");
            }
            finally
            {
                xDoc = null;
            }
        }

        #endregion

        //杨定，用于地址栏传中文的处理
        #region BASE64 编码/解码

        /// <summary>
        /// BASE64编码
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>编码后的字符串</returns>
        public static string Base64_Encode(string source)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(source));
        }

        /// <summary>
        /// BASE64解码
        /// </summary>
        /// <param name="data">编码后的字符串</param>
        /// <returns>源字符串</returns>
        public static string Base64_Decode(string data)
        {
            byte[] bytes = Convert.FromBase64String(data);
            return Encoding.UTF8.GetString(bytes);
        }

        #endregion BASE64 编码/解码

        /// <summary>
        /// 应用程序集目录
        /// </summary>
        public static string AppDirectory
        {
            get
            {
                return System.Web.HttpContext.Current.Request.PhysicalApplicationPath;
            }
        }

        #region  列表转成用逗号隔开的字符串  ----YKS 2016.01.15

        /// <summary>
        /// 列表转成用逗号隔开的字符串
        /// </summary>
        /// <param name="strList"></param>
        /// <returns></returns>
        public static string ListConvertToString(IList<string> strList)
        {
            if (strList.Count > 0)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var s in strList)
                {
                    stringBuilder.Append(s + ",");
                }
                string str = stringBuilder.ToString();
                return str.Substring(0, str.Length - 1);
            }
            else
            {
                return "";
            }
        }

        #endregion

        #region  列表转成用逗号隔开的字符串ForSql(每个字符上添加了单引号)  ----YKS 2016.01.15

        /// <summary>
        /// 列表转成用逗号隔开的字符串
        /// </summary>
        /// <param name="strList"></param>
        /// <returns></returns>
        public static string ListConvertToStringForSql(IList<string> strList)
        {
            if (strList.Count > 0)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var s in strList)
                {
                    stringBuilder.Append("'" + s + "',");
                }
                string str = stringBuilder.ToString();
                return str.Substring(0, str.Length - 1);
            }
            else
            {
                return "";
            }
        }

        #endregion

        /// <summary>
        /// 浅拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Copy<T>(object source) where T : new()
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            var target = new T();
            var typesSource = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var typesTarget = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var propertyInfo in typesSource)
            {
                var tar = typesTarget.FirstOrDefault(t => t.Name == propertyInfo.Name);
                if (tar != null)
                {
                    tar.SetValue(target, propertyInfo.GetValue(source));
                }
            }
            return target;
        }

        #region  写入异常文件  add by Yks 2016.01.21

        /// <summary>
        /// 将异常打印到LOG文件
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="logAddress">日志文件地址</param>
        public static void WriteLog(Exception ex, string logAddress = "")
        {
            //如果日志文件为空，则默认在Debug目录下新建 YYYY-mm-dd_Log.log文件
            if (logAddress == "")
            {
                logAddress = Environment.CurrentDirectory + '\\' +
                    DateTime.Now.Year + '-' +
                    DateTime.Now.Month + '-' +
                    DateTime.Now.Day + "_Log.log";
            }

            //把异常信息输出到文件
            StreamWriter sw = new StreamWriter(logAddress, true);
            sw.WriteLine("当前时间：" + DateTime.Now.ToString());
            sw.WriteLine("异常信息：" + ex.Message);
            sw.WriteLine("异常对象：" + ex.Source);
            sw.WriteLine("调用堆栈：\n" + ex.StackTrace.Trim());
            sw.WriteLine("触发方法：" + ex.TargetSite);
            sw.WriteLine("----------------------------------------------------------");
            sw.WriteLine();
            sw.Close();
        }


        #endregion


        #region
        /// <summary>
        /// 检查Sql输入语句的安全性和正确性
        /// </summary>
        /// <param name="sql"></param>
        public static void CheckSql(string sql)
        {
            var fitle = new[] { "delete", "drop", "alter", "truncate ", "update ", "insert ", "create", "*", "--" };

            var needfitle = new[] { "select", "from" };

            foreach (var f in fitle)
            {
                if (sql.ToLower().IndexOf(f, StringComparison.InvariantCultureIgnoreCase) > 0)
                {
                    throw new Exception("sql语句有非法字符:" + f);
                }
            }

            foreach (var f in needfitle)
            {
                if (sql.ToLower().IndexOf(f, StringComparison.InvariantCultureIgnoreCase) < 0)
                {
                    throw new Exception("sql语句必须包含字符否则会无法查询:" + f);
                }
            }

        }


        #endregion


        #region 根据扩展名获取easyuiCss

        /// <summary>
        /// 根据扩展名后缀获取easyui Css Class
        /// </summary>
        /// <param name="extName"></param>
        /// <returns></returns>
        public static string GetFileClassByExtendName(string extName)
        {

            extName = extName.Replace(".","").Trim();

            string[] picext = { "png", "jpg", "amp", "bpm", "jpeg" };
            string[] zipext = { "zip", "rar", "7z" };
            string[] wordext = { "doc", "docx" };
            string[] excelext = { "xls", "xlsx" };
            string[] flash = { "gif", "swf"};
            string[] visioext = { "vsd", "cad"};

            if (picext.Contains(extName)) {
                return "icon-picture";
            }

            if (zipext.Contains(extName))
            {
                return "icon-page_white_zip";
            }
            if (wordext.Contains(extName))
            {
                return "icon-page_white_word";
            }
            if (excelext.Contains(extName))
            {
                return "icon icon-page_white_excel";
            }
            if (flash.Contains(extName))
            {
                return "icon-page_white_flash";
            }
            if (visioext.Contains(extName))
            {
                return "icon-shape_align_center";
            }

            return "icon-page_white_office";
        }
        #endregion


    }
}