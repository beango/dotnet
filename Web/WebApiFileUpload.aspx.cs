using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using beango.util;

namespace Web
{
    public partial class WebApiFileUpload : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            btnUpload.Click += btnUpload_Click;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFiles();
            }
        }
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnUpload_Click(object sender, EventArgs e)
        {
            var fullname = Path.GetFullPath(FileUpload1.PostedFile.FileName);
            string rst = FileHelper.HttpUploadFile("http://localhost:8346/FileUpload/UploadFile", FileUpload1.FileBytes, fullname, FileUpload1.PostedFile.ContentType,
                new NameValueCollection { { "exceltype", "测试目录" } });
            Response.Write(rst);

            BindFiles();
        }

        private void BindFiles()
        {
            var fs = HttpUtil.Request("http://localhost:8346/FileUpload/FileList");
            //Response.Write(fs);
            Repeater1.DataSource = JsonHelper.JsonDeserializa<string[]>(fs);
            Repeater1.DataBind();
        }
    }
}