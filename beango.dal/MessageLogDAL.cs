using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using beango.dal.PetaPoco;

namespace beango.dal
{
    public class MessageLogDAL
    {

        public List<dynamic> GetMessageLogList()
        {
            return new Database("DBConn").Fetch<dynamic>("select * from messagelog where SendFlag=0 and ErrNum<3 order by id asc");
        }

        /// <summary>
        /// 发送失败
        /// </summary>
        /// <param name="msid"></param>
        /// <param name="summary"></param>
        /// <returns></returns>
        public int UpdateSendFail(string msid, string summary)
        {
            var db = new Database("DBConn");
            string sql = @"update MessageLog set ErrNum=ErrNum+1,SendSummary=@0 where id in(select * from f_split(@1))";
            return db.Execute(sql, summary, msid);
        }
    }
}
