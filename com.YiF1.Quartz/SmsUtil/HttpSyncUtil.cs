using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace com.YiF1.Quartz
{
    public class HttpSyncUtil
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="url"> </param>
        /// <param name="parameters"></param>
        /// <param name="param"> </param>
        public void GetRequest(string url, IDictionary<string, string> parameters, RequestData param) 
        {
            try
            {
                //如果需要POST数据  
                if (!(parameters == null || parameters.Count == 0))
                {
                    StringBuilder buffer = new StringBuilder();
                    int i = 0;
                    foreach (string key in parameters.Keys)
                    {
                        if (i > 0)
                        {
                            buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                        }
                        else
                        {
                            buffer.AppendFormat("?{0}={1}", key, parameters[key]);
                        }
                        i++;
                    }
                    param.summary.AppendFormat("参数：【{0}】；", buffer.ToString());
                    url += buffer.ToString();
                    var request = WebRequest.Create(url) as HttpWebRequest;
                    if (request == null)
                        return;
                    request.Method = "GET";
                    request.ContentType = "application/x-www-form-urlencoded";
                    try
                    {
                        param.request = request;
                        request.BeginGetResponse(WebResponseCallback, param);
                    }
                    catch (Exception ex)
                    {
                        Util.LogHelper.Error(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Util.LogHelper.Error(ex);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="url"> </param>
        /// <param name="parameters"></param>
        /// <param name="param"> </param>
        public void Request(string url, IDictionary<string, string> parameters, RequestData param)
        {
            try
            {
                var request = WebRequest.Create(url) as HttpWebRequest;
                if (request == null)
                    return;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                //如果需要POST数据  
                if (!(parameters == null || parameters.Count == 0))
                {
                    StringBuilder buffer = new StringBuilder();
                    int i = 0;
                    foreach (string key in parameters.Keys)
                    {
                        if (i > 0)
                        {
                            buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                        }
                        else
                        {
                            buffer.AppendFormat("{0}={1}", key, parameters[key]);
                        }
                        i++;
                    }
                    param.summary.AppendFormat("参数：【{0}】；", buffer.ToString());
                    byte[] data = Encoding.GetEncoding(param.requestEncoding).GetBytes(buffer.ToString());

                    try
                    {
                        param.request = request;
                        param.msgBytes = data;
                        request.BeginGetRequestStream(SendCallBack, param);
                    }
                    catch (Exception ex)
                    {
                        Util.LogHelper.Error(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Util.LogHelper.Error(ex);
            }
        }

        /// <summary>
        /// 发送消息后的回调方法（发送消息体）
        /// </summary>
        /// <param name="result"></param>
        private void SendCallBack(IAsyncResult result)
        {
            try
            {
                RequestData data = result.AsyncState as RequestData;
                HttpWebRequest request = data.request;

                using (BinaryWriter bw = new BinaryWriter(request.EndGetRequestStream(result)))
                {
                    bw.Write(data.msgBytes);
                }

                request.BeginGetResponse(WebResponseCallback, data);
                data.msgBytes = null;
            }
            catch (Exception ex)
            {
                Util.LogHelper.Error(ex);
            }
        }

        /// <summary>
        /// 请求的主体部分（由此完成请求并返回请求结果）
        /// </summary>
        /// <param name="asyncResult"> </param>
        /// <returns>请求返回结果</returns>
        private void WebResponseCallback(IAsyncResult asyncResult)
        {
            try
            {
                RequestData param = asyncResult.AsyncState as RequestData;
                HttpWebResponse response = null;
                HttpWebRequest request = param.request;
                try
                {
                    response = request.EndGetResponse(asyncResult) as HttpWebResponse;
                }
                catch (WebException ex)
                {
                    response = ex.Response as HttpWebResponse;
                }

                if (response == null)
                {
                    param.result = "请求远程返回为空";
                    param.summary.Append("结果：【请求远程返回为空】；");
                }
                else
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        param.result = reader.ReadToEnd();
                        param.summary.AppendFormat("结果：【{0}】；", param.result.Trim());
                    }
                }
                if (param.onComplete != null)
                    param.onComplete(param);
            }
            catch (Exception ex)
            {
                Util.LogHelper.Error(ex);
            }
        }
    }
}
