using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.YiF1.Quartz
{
    public class MsgData
    {
        public int resend { get; set; }

        public List<MsgItem> list { get; set; }

        public int size { get; set; }
    }

    public class MsgItem
    {
        public string msid { get; set; }
        public string content { get; set; }
        public string mobile { get; set; }
    }
}
