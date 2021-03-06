﻿using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace beango.util
{
    public class FileUploadHelper
    {
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="url">上传url</param>
        /// <param name="buffer">上传数据</param>
        /// <param name="fullname">上传文件名</param>
        /// <param name="contentType">文档类型:FileUpload1.PostedFile.ContentType</param>
        /// <param name="nvc">参数</param>
        /// <returns></returns>
        public static string HttpUploadFile(string url, byte[] buffer, string fullname, string contentType, NameValueCollection nvc)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = CredentialCache.DefaultCredentials;

            Stream rs = wr.GetRequestStream();

            const string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            const string headerTemplate = "Content-Disposition: form-data; name=\"file\"; filename=\"{0}\"\r\nContent-Type: {1}\r\n\r\n";
            string header = string.Format(headerTemplate, fullname, contentType);
            byte[] headerbytes = Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            rs.Write(buffer, 0, buffer.Length);

            byte[] trailer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                if (stream2 != null)
                {
                    StreamReader reader2 = new StreamReader(stream2);
                    var rst = reader2.ReadToEnd();
                    return rst;
                }
            }
            catch (Exception ex)
            {
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
                LogHelper.Error(ex);
                throw;
            }
            finally
            {
                wr = null;
            }
            return "";
        }
    }
}
