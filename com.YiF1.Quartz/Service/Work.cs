using System;
using System.Threading;
using Quartz;

namespace com.YiF1.Quartz
{
    /// <summary>
    /// 短信定时发送任务
    /// </summary>
    public class SmsIntervalSendWork : IJob
    {
        public SmsIntervalSendWork()
        {

        }
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                SendSms();
            }
            catch (Exception ex)
            {
                JobExecutionException exception = new JobExecutionException(ex);
                exception.Source = context.JobDetail.Key.ToString();
                exception.UnscheduleFiringTrigger = true;
                Util.LogHelper.Error(ex);
            }
        }

        public static void SendSms()
        {
             ThreadPool.QueueUserWorkItem(new YeionSmsService().SendMultiSms);
        }
    }

}
