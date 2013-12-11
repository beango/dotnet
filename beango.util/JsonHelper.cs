using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Data;

namespace beango.util
{
    public class JsonHelper
    {
        public static T JsonDeserializa<T>(string strjson)
        {
            DataContractJsonSerializer zer = new DataContractJsonSerializer(typeof(T));
            if (strjson != null)
            {
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(strjson));
                T obj = (T)zer.ReadObject(ms);
                return obj;
            }
            return default(T);
        }

        public static string JsonSerializa<T>(T t)
        {
            DataContractJsonSerializer zer = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            zer.WriteObject(ms, t);
            string jsonstring = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonstring;
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
