using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using beango.dal;
using beango.model;
using beango.util;

namespace beango.quartz
{
    public class YeionSmsService
    {

        #region 发送多条
        /// <summary>
        /// 短信批量发送
        /// </summary>
        /// <param name="state"></param>
        public void SendMultiSms(object state)
        {
            RequestData param = state as RequestData;
            try
            {
                List<dynamic> msgList = param.msgList;
                if (msgList == null || msgList.Count == 0)
                    return;
                #region 组装需要发送的消息

                string msgContent = "JsonHelper.ObjDivertToJson(md)";

                #endregion

                string url = "http://localhost:8001/Handler/test.ashx";
                StringBuilder _summary = new StringBuilder();
                _summary.AppendFormat("短信发送【{0}】；", url);

                IDictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("Account", "account");
                parameters.Add("Password", "pwd");
                parameters.Add("jsondata", msgContent);
                parameters.Add("Exno", "0");


                param.summary = _summary;
                param.onComplete = onSendComplete;

                new HttpSyncUtil().Request(url, parameters, param);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                if (null != param.manualEvent)
                    param.manualEvent.SetOne();
            }
        }
        #endregion

        /// <summary>
        /// 请求完成后要执行的操作
        /// </summary>
        /// <param name="code">Http状态码</param>
        /// <param name="result">返回结果</param>
        public static void onSendComplete(RequestData resultData)
        {
            try
            {
                var dictResult = resultData.result.Trim();
                resultData.summary.Append("【处理结果】：" + resultData.result.Trim());

                string msid = @resultData.msgList.Aggregate("", (current, yeionItem) => current + (yeionItem.id.ToString() + ","));
                if (!string.IsNullOrEmpty(msid))
                {
                    msid = msid.Substring(0, msid.Length - 1);
                    new MessageLogDAL().UpdateSendFail(msid, resultData.summary.ToString());
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            finally
            {
                if (null != resultData.manualEvent)
                    resultData.manualEvent.SetOne();
            }
        }
    }
}