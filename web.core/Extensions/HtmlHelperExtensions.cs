using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace web.core.Extensions
{
    //====================================================================== 
    // create by:hd
    // create date:2014-12-17 11:42
    // filename :Q-PC
    // description : 
    // 
    //======================================================================
    public static class HtmlHelperExtensions
    {
        #region AppSettings Config

        /// <summary>
        /// 根节点
        /// </summary>
        private const string BaseRootPath = "~/";

        /// <summary>
        /// 版本号
        /// </summary>
        private static readonly string Version = ConfigurationManager.AppSettings["Version"];
        #endregion

        /// <summary>
        /// 获取版本号
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static MvcHtmlString GetVersionNo(this HtmlHelper html)
        {
            return new MvcHtmlString(Version);
        }

        /// <summary>
        /// 生成CSS的链接
        /// </summary>
        /// <param name="html"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static MvcHtmlString StyleSheet(this HtmlHelper html, string fileName)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            return StyleSheetForFullPath(html,
                urlHelper.Content(BaseRootPath + fileName));
        }

        /// <summary>
        /// 生成CSS的链接
        /// </summary>
        /// <param name="html"></param>
        /// <param name="path">全路径</param>
        /// <returns></returns>
        public static MvcHtmlString StyleSheetForFullPath(this HtmlHelper html, string path)
        {

            var tagBuilder = new TagBuilder("link");
            tagBuilder.MergeAttribute("href", path.GetVersionPar(Version));
            tagBuilder.MergeAttribute("rel", "stylesheet");
            tagBuilder.MergeAttribute("type", "text/css");
            return MvcHtmlString.Create(HttpUtility.HtmlDecode(tagBuilder.ToString()));
        }

        /// <summary>
        /// 生成Scrip的链接
        /// </summary>
        /// <param name="html"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static MvcHtmlString JavaScript(this HtmlHelper html, string fileName)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            return JavaScriptForFullPath(html,
                urlHelper.Content(BaseRootPath + fileName));
        }


        /// <summary>
        /// 生成Scrip的链接
        /// </summary>
        /// <param name="html"></param>
        /// <param name="path">全路径</param>
        /// <returns></returns>
        public static MvcHtmlString JavaScriptForFullPath(this HtmlHelper html, string path)
        {

            var tagBuilder = new TagBuilder("script");
            tagBuilder.MergeAttribute("src", path.GetVersionPar(Version));
            tagBuilder.MergeAttribute("type", "text/javascript");
            return MvcHtmlString.Create(HttpUtility.HtmlDecode(tagBuilder.ToString()));
        }

    }
}
