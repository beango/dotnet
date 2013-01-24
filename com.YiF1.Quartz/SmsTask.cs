using System;
using System.ServiceProcess;
using Quartz;
using Quartz.Impl;

namespace com.YiF1.Quartz
{
    partial class SmsTask : ServiceBase
    {
        public SmsTask()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = e.ExceptionObject as Exception;
                Util.LogHelper.Error("来自“MonitorOnServer”的全局异常。" + ex.Message + "详细信息如下："
                                    + Environment.NewLine + "［InnerException］" + ex.InnerException
                                    + Environment.NewLine + "［Source］" + ex.Source
                                    + Environment.NewLine + "［TargetSite］" + ex.TargetSite
                                    + Environment.NewLine + "［StackTrace］" + ex.StackTrace);
            }
            catch
            {

            }
        }

        private static ISchedulerFactory _schedFactory;
        private static IScheduler sched;

        protected override void OnStart(string[] args)
        {
            try
            {
                Util.LogHelper.Info("短信定时任务启动。");
                _schedFactory = new StdSchedulerFactory();
                sched = _schedFactory.GetScheduler();
                sched.Start();
            }
            catch (Exception ex)
            {
                Util.LogHelper.Error(ex);
            }
        }

        protected override void OnStop()
        {
            try
            {
                sched.Shutdown();
            }
            catch
            {
            }
            Util.LogHelper.Info("短信定时发送任务停止。");
        }
    }
}
