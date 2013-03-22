using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using beango.dal.PetaPoco;

namespace beango.dal
{
    public class MessageLogDAL
    {

        public void UpdateSendFail(string msid, string summary)
        {
            string sql = @"update MessageLog set ErrNum=ErrNum+1,SendSummary=@0 where id in(select * from f_split2(@1))";
            Database db = new Database("DBConn");

            db.Execute(sql, new object[] {summary, msid});
        }
    }
}
