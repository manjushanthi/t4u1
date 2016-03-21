using DpmDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace T4U.CRT.Generation.Model
{
    public class DefaultMemberResolver : IDefaultMemberResolver
    {
        static HashSet<string> defaultDimCodes;
        SQLiteConnector connector;

        public DefaultMemberResolver(SQLiteConnector dataConnector)
        {
            this.connector = dataConnector;
        }

        public bool isDefault(string mappingCode)
        {
            if (defaultDimCodes == null || defaultDimCodes.Count == 0)
                loadDefaultCodes();

            return defaultDimCodes.Contains(mappingCode);
        }

        private void loadDefaultCodes()
        {
            defaultDimCodes = new HashSet<string>();

            if (connector == null)
                return;

            string query = @"select d.DimensionXBRLCode||'('||m.MemberXBRLCode||')' as def
from mDimension d
    inner join mMember m on m.DomainID = d.DomainID
where m.IsDefaultMember = 1";

            DataTable dt = connector.executeQuery(query);
            foreach (DataRow dr in dt.Rows)
                defaultDimCodes.Add(dr["def"].ToString());
        }
    }
}
