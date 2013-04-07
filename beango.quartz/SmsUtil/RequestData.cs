using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using beango.util;

namespace beango.quartz
{
    public class RequestData
    {
        public HttpWebRequest request { get; set; }

        public byte[] msgBytes { get; set; }

        public List<dynamic> msgList { get; set; }

        public StringBuilder summary { get; set; }

        public String result { get; set; }

        public Action<RequestData> onComplete { get; set; }

        public MutipleThreadResetEvent manualEvent { get; set; }

        private string _requestEncoding = "UTF-8";
        public string requestEncoding
        {
            get { return _requestEncoding; }
            set { _requestEncoding = value; }
        }
    }
}
