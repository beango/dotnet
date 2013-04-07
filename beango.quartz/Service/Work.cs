using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Quartz;
using beango.dal;
using beango.dal.PetaPoco;
using beango.quartz;
using beango.model;
using beango.util;

namespace beango.quartz
{
    /// <summary>
    /// 短信定时发送任务
    /// </summary>
    public class SmsIntervalSendWork : IJob
    {
        private static object syncRoot = new object();
        public SmsIntervalSendWork()
        {

        }
        public void Execute(IJobExecutionContext context)
        {
            bool lockTaken = false;
            Monitor.TryEnter(syncRoot, 500, ref lockTaken);
            try
            {
                List<dynamic> smss = new MessageLogDAL().GetMessageLogList();
                
                if (null != smss && smss.Count > 0 && lockTaken)
                {
                    SendSms(smss);
                }
            }
            catch (Exception ex)
            {
                JobExecutionException exception = new JobExecutionException(ex);
                exception.Source = context.JobDetail.Key.ToString();
                exception.UnscheduleFiringTrigger = true;
                LogHelper.Error(exception);
            }
        }

        public static void SendSms(List<dynamic> msgList)
        {
            var countdown = new MutipleThreadResetEvent();
            try
            {
                const int NumPer = 100;//接口每次最多发送的消息条数
                var smsGroup = msgList.GroupBy(item => item.ClientID);

                foreach (var group in smsGroup)//每个Client分批次发送
                {
                    int index = 0;
                    while (index < group.Count())
                    {
                        var sendSMS = group.Skip(index).Take(NumPer).ToList();
                        if (sendSMS.Count > 0)
                        {
                            countdown.RegisterEvent();
                            RequestData param = new RequestData()
                                                    {
                                                        msgList = sendSMS,
                                                        manualEvent = countdown
                                                    };
                            ThreadPool.QueueUserWorkItem(new YeionSmsService().SendMultiSms, param);
                            index += sendSMS.Count;
                        }
                    }
                }
                countdown.WaitAll();
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
            }
            finally
            {
                try
                {
                    Monitor.Exit(syncRoot);
                }
                catch (Exception ex)
                {
                    LogHelper.Error(ex);
                }
            }
        }
    }

}
