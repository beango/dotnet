using System.Text;
using System.Data;
using System.Web.Script.Serialization;

namespace beango.util
{
    public class JsonHelper
    {
        public static T JsonDivertToObj<T>(string json)
        {
            JavaScriptSerializer script = new JavaScriptSerializer();
            T dic = script.Deserialize<T>(json);
            return dic;
        }

        public static string ObjDivertToJson(object obj)
        {
            JavaScriptSerializer script = new JavaScriptSerializer();
            string json = script.Serialize(obj);
            return json;
        }

        public static string DataTabDrivertJson(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                sb.Append("[");
            }
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("{Text:\"" + dr[0] + "\"},");
            }
            if (sb.ToString().LastIndexOf(',') == sb.Length - 1)
            {
                sb.Remove(sb.Length - 1, 1);
                sb.Append("]");
            }
            return sb.ToString();
        }
    }
}
