using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Quartz;
using beango.dal;
using beango.util;

namespace beango.quartz
{
    /// <summary>
    /// 定时作业，
    /// DisallowConcurrentExecution禁止并行作业
    /// </summary>
    [DisallowConcurrentExecution]
    public class SmsIntervalSendWork : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                //do ...
            }
            catch (Exception ex)
            {
                
            }
        }

    }

}
