using System;
using System.Collections.Generic;
using System.Text;

namespace com.YiF1.Quartz
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
            List<string> msgList = state as List<string>;
            if (msgList == null || msgList.Count == 0)
                return;
            #region 组装需要发送的消息
            
            string msgContent = "JsonHelper.ObjDivertToJson(md)";

            #endregion

            string url = "http://www.baidu.com";
            StringBuilder _summary = new StringBuilder();
            _summary.AppendFormat("短信发送【{0}】；", url);

            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Account", "account");
            parameters.Add("Password", "pwd");
            parameters.Add("jsondata", msgContent);
            parameters.Add("Exno", "0");

            RequestData param = new RequestData()
            {
                msgList = msgList,
                summary = _summary,
                onComplete = onSendComplete
            };
            new HttpSyncUtil().Request(url, parameters, param);
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
                var dictResult =resultData.result.Trim();
            }
            catch (Exception ex)
            {
                Util.LogHelper.Error(ex);
            }
        }
    }
}
