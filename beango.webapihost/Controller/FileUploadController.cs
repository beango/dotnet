using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using beango.util;

namespace beango.webapihost.Controller
{
    public class FileUploadController : ApiController
    {
        [HttpGet]
        public string Hello()
        {
            return "Hello from windows service!";
        }

        [HttpPost]
        public Hashtable UploadFile()
        {
            Hashtable hash = new Hashtable();
            try
            {
                // 检查是否是 multipart/form-data 
                if (!Request.Content.IsMimeMultipartContent("form-data"))
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                //文件保存目录路径 
                string SaveTempPath = AppDomain.CurrentDomain.BaseDirectory + "\\upload";
                if (!Directory.Exists(SaveTempPath))
                {
                    Directory.CreateDirectory(SaveTempPath);
                }
                // 设置上传目录 
                var streamprovider = new MultipartFormDataStreamProvider(SaveTempPath);

                var provider = Request.Content.ReadAsMultipartAsync(streamprovider).Result;

                if (provider.FileData == null || provider.FileData.Count == 0)
                {
                    hash["ok"] = 0;
                    hash["msg"] = "上传失败。";
                    return hash;
                }

                var file = provider.FileData[0];//provider.FormData 
                string orfilename = file.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
                FileInfo fileinfo = new FileInfo(file.LocalFileName);

                if (fileinfo.Length <= 0)
                {
                    hash["ok"] = 0;
                    hash["msg"] = "请选择上传文件。";
                }
                else
                {
                    string fileExt = orfilename.Substring(orfilename.LastIndexOf('.'));
                    string exceltype = provider.FormData["exceltype"];

                    String newFileName = Guid.NewGuid().ToString();
                    if (!Directory.Exists(Path.Combine(SaveTempPath, exceltype)))
                    {
                        Directory.CreateDirectory(Path.Combine(SaveTempPath, exceltype));
                    }
                    fileinfo.CopyTo(Path.Combine(SaveTempPath, exceltype, newFileName + fileExt), true);
                    fileinfo.Delete();
                    hash["ok"] = 1;
                    hash["msg"] = exceltype + "/" + newFileName + fileExt;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                hash["ok"] = 0;
                hash["msg"] = "上传出错";
            }
            return hash;
        }

        [HttpGet]
        public string[] FileList()
        {
            return Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\upload\\" + "测试目录");
        }

        [HttpGet]
        public HttpResponseMessage DownFile(string filename)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\upload\\" + filename))
                {
                    result = new HttpResponseMessage(HttpStatusCode.NotFound);
                    return result;
                }

                var stream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\upload\\" + filename, FileMode.Open, FileAccess.Read);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = filename;
                return result;
            }
            catch (Exception)
            {
                result = new HttpResponseMessage(HttpStatusCode.NotFound);
                return result;
            }
        }
    }
}
